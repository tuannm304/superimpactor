Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Claunia.PropertyList
Imports Ionic.Zip
Imports Newtonsoft.Json


Public Class Install
    Private Class DeviceInfo
        Public udid As String = ""
        Public deviceName As String = ""
    End Class

    Private trd As Thread
    Public Shared appInfo As AppInfos
    Delegate Sub LoadingInvokeDelegate()
    Delegate Sub DeviceInvokeDelegate()
    Dim statusRs As String
    Dim appId As String
    Dim isExit As Boolean
    Dim lstAcc As Dictionary(Of String, String)
    Dim listDevice As List(Of DeviceInfo)
    Dim cloneID As String

    Function installFromFile(fileIpa As String, Optional cloneIds As String = "", Optional newName As String = "") As String
        'extract
        Try
            Directory.Delete(AppConst.m_localTmp, True)
        Catch
        End Try
        Try
            Directory.CreateDirectory(AppConst.m_localTmp)
        Catch
        End Try

        Using zip1 As ZipFile = ZipFile.Read(fileIpa)
            Dim en As ZipEntry
            ' here, we extract every entry, but we could extract conditionally,
            ' based on entry name, size, date, checkbox status, etc.   
            For Each en In zip1
                If en.FileName.EndsWith("Info.plist") Then
                    en.Extract(AppConst.m_localTmp, ExtractExistingFileAction.OverwriteSilently)
                End If
            Next
        End Using

        Dim files As String() = Directory.GetDirectories(AppConst.m_localTmp + "\Payload\", "*.app")
        If (LBound(files) <> 0) Then
            Return "IPA file is error"
        End If
        Dim filePl As FileInfo = New FileInfo(AppConst.m_localTmp + "\Payload\" + Path.GetFileName(files(0)) + "\Info.plist")
        Dim nodes As NSDictionary = PropertyListParser.Parse(filePl)
        appInfo = New AppInfos
        appInfo.Filename = fileIpa
        appInfo.Package = nodes.ObjectForKey("CFBundleIdentifier").ToString
        appInfo.Package = Regex.Replace(appInfo.Package, "[^a-zA-Z.]", "")

        If nodes.ContainsKey("CFBundleName") Then
            appInfo.Name = nodes.ObjectForKey("CFBundleName").ToString
        Else
            appInfo.Name = "noname"
        End If
        'remove appex
        '        Dim fnd As Boolean = False
        '        Common.DeleteFolderInFolder(AppConst.m_localTmp, "appex", fnd)
        '        If fnd Then
        '           Common.Zip(AppConst.m_localTmp, fileIpa)
        '        End If
        AppConst.logger.Info("installFromFile: pk=" & appInfo.Package)
        picLoading.Visible = True
        lblLoading.Visible = True
        lblLoading.Text = "Detecting device..."
        txtAppName.Text = newName
        cloneID = cloneIds
        If cloneID.Equals("") Then
            chkCloneApp.Checked = False
        Else
            chkCloneApp.Checked = False
        End If
        Me.ShowDialog()
        Return ""
    End Function

    Private Async Sub ThreadTask()
        Dim webClient As New System.Net.WebClient
        lblLoading.BeginInvoke(New DeviceInvokeDelegate(AddressOf DeviceInvokeMethod))
        Dim i As Integer = 0
        Do While Not isExit
            i = (i + 1) Mod 10

            Try
                If i = 0 Then
                    Await detectDevice()
                    lblLoading.BeginInvoke(New DeviceInvokeDelegate(AddressOf DeviceInvokeMethod))
                Else
                    picLoading.BeginInvoke(New LoadingInvokeDelegate(AddressOf LoadingInvokeMethod))
                End If
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Thread.Sleep(200)
        Loop
    End Sub

    Public Sub LoadingInvokeMethod()
        picLoading.Invalidate()
    End Sub


    Public Sub DeviceInvokeMethod()
        cmbDevice.Items.Clear()
        For i = 0 To listDevice.Count - 1
            cmbDevice.Items.Add(listDevice.Item(i).deviceName + " - " + listDevice.Item(i).udid)
        Next i
        'Console.WriteLine(sOutput)
        If cmbDevice.Items.Count = 0 Then
            cmdInstall.Enabled = False
            cmbDevice.Text = ""
            picLoading.Visible = True
            lblLoading.Visible = True
            lblLoading.Text = "Detecting device..."
        Else
            cmdInstall.Enabled = True
            If lblLoading.Text = "Detecting device..." Then
                picLoading.Visible = False
                lblLoading.Text = ""
            End If
        End If
        If cmbDevice.Items.Count > 0 Then
            For i = 0 To cmbDevice.Items.Count - 1
                If cmbDevice.Text = cmbDevice.Items.Item(i) Then
                    Exit Sub
                End If
            Next i
            cmbDevice.SelectedIndex = 0
        End If
    End Sub 'InvokeMethod

    Private Async Function detectDevice() As Task
        Dim sOutput As String = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceinfo.exe", "")
        Dim line As String() = sOutput.Split(vbLf)
        Dim deviceName As String
        Dim udid As String

        For i = 0 To line.Length - 1
            If line(i).StartsWith("UniqueDeviceID") Then
                udid = line(i).Substring("UniqueDeviceID:".Length).Trim()
            ElseIf line(i).StartsWith("DeviceName") Then
                deviceName = line(i).Substring("DeviceName:".Length).Trim()
            End If
        Next
        listDevice.Clear()
        If (udid <> "" And deviceName <> "") Then
            Dim device As DeviceInfo = New DeviceInfo
            device.udid = udid
            device.deviceName = deviceName
            listDevice.Add(device)
        End If

    End Function
    Private Sub Install_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listDevice = New List(Of DeviceInfo)()
        Dim trd As Thread = New Thread(AddressOf ThreadTask)
        trd.Start()
        'load account
        lstAcc = Database.GetAccounts
        For Each kvp As KeyValuePair(Of String, String) In lstAcc
            cmbAppleId.Items.Add(kvp.Key)
        Next

    End Sub

    Public Async Function Create(appleId As String, password As String, udid As String, deviceName As String, machineName As String, machineId As String, ipaOrgFile As String, Optional isClone As Boolean = False) As Task(Of String)
        Dim cloneID As String
        If isClone Then
            cloneID = Common.GetHash((DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds).Substring(25)
        Else
            cloneID = Me.cloneID
            If Not cloneID.Equals("") Then
                isClone = True
            End If
        End If

        Dim fakeAppId As String = "si." + Common.GetHash(Trim(appleId)) + "." + IIf(isClone, "clone" & cloneID & ".", "") + Regex.Replace(appInfo.Package, "[^a-zA-Z.]", "")

        Dim appName As String = Regex.Replace("SI - " + appInfo.Name, "[^a-zA-Z ]", "")

        AppConst.logger.Info("Install::create: " & deviceName & "," & machineName & "," & machineId & "," & ipaOrgFile & "    id=" & fakeAppId & "," & appName)

        '1. Login
        Dim accInfo As String
        Dim totalStep As Integer = 9
        lblLoading.Text = "[1/" & totalStep & "] Login..."
        Application.DoEvents()
        Dim nodes As NSDictionary = AppleService.login(appleId, password)
        If (Not nodes.ContainsKey("myacinfo")) Then
            AppConst.logger.Info("Install::create: cannot login itune: " & nodes.ToXmlPropertyList())
            Return "Cannot login itune..."
        End If
        accInfo = nodes.ObjectForKey("myacinfo").ToString()

        '2. Get team
        lblLoading.Text = "[2/" & totalStep & "] Get team..."
        Application.DoEvents()
        nodes = AppleService.listTeam(accInfo)
        Dim teamId As String = ""
        If (nodes.ContainsKey("teams")) Then
            Dim lstTeams As NSArray = DirectCast(nodes.ObjectForKey("teams"), NSArray)
            If (lstTeams.Count > 0) Then
                Dim team As NSDictionary = lstTeams.ElementAt(0)
                If (team.ContainsKey("teamId")) Then
                    teamId = team.ObjectForKey("teamId").ToString()
                End If
            End If
        End If

        If teamId = "" Then
            AppConst.logger.Info("Install::create: not have teamId: " & nodes.ToXmlPropertyList())
            Return "Not have teamId"
        End If

        '3. Device
        lblLoading.Text = "[3/" & totalStep & "] Get devices..."
        Application.DoEvents()
        nodes = AppleService.listDevices(teamId)
        Dim deviceFnd As Boolean
        deviceFnd = False
        If (nodes.ContainsKey("devices")) Then
            Dim lstDevs As NSArray = DirectCast(nodes.ObjectForKey("devices"), NSArray)
            If (lstDevs.Count > 0) Then
                For i = 0 To lstDevs.Count - 1
                    Dim dev As NSDictionary = lstDevs.ElementAt(i)
                    If (dev.ContainsKey("deviceNumber")) Then
                        If udid = dev.ObjectForKey("deviceNumber").ToString() Then
                            deviceFnd = True
                            Exit For
                        End If
                    End If
                Next i
            End If
        End If

        If Not deviceFnd Then
            lblLoading.Text = "[3+/" & totalStep & "] Add device..."
            Application.DoEvents()
            nodes = AppleService.addDevice(udid, deviceName, teamId)
            deviceFnd = False
            If (nodes.ContainsKey("devices")) Then
                Dim lstDevs As NSArray = DirectCast(nodes.ObjectForKey("devices"), NSArray)
                If (lstDevs.Count > 0) Then
                    For i = 0 To lstDevs.Count - 1
                        Dim dev As NSDictionary = lstDevs.ElementAt(i)
                        If (dev.ContainsKey("deviceNumber")) Then
                            If udid = dev.ObjectForKey("deviceNumber").ToString() Then
                                deviceFnd = True
                                Exit For
                            End If
                        End If
                    Next i
                End If
            ElseIf (nodes.ContainsKey("device")) Then
                Dim dev As NSDictionary = nodes.ObjectForKey("device")
                If (dev.ContainsKey("deviceNumber")) Then
                    If udid = dev.ObjectForKey("deviceNumber").ToString() Then
                        deviceFnd = True
                    End If
                End If
            End If
            If Not deviceFnd Then
                AppConst.logger.Info("Install::create: cannot add device " & nodes.ToXmlPropertyList())
                If nodes.ContainsKey("userString") Then
                    Return "Cannot add device: " & nodes.ObjectForKey("userString").ToString
                End If
                Return "Cannot add device"
            End If
        End If

        '4. get cert
        lblLoading.Text = "[4/" & totalStep & "] Get certs..."
        Application.DoEvents()
        nodes = AppleService.allDevelopmentCert(teamId)
        AppConst.logger.Info("Install::create: all certs: " & nodes.ToXmlPropertyList())
        Dim certData As String = ""
        Dim certSerial As String
        If (nodes.ContainsKey("certificates")) Then
            Dim lstcerts As NSArray = DirectCast(nodes.ObjectForKey("certificates"), NSArray)
            For i = 0 To lstcerts.Count - 1
                Dim cert As NSDictionary = lstcerts.ElementAt(i)
                If (cert.ContainsKey("certContent") And cert.ContainsKey("machineName")) Then
                    Dim certMachineName As String = cert.ObjectForKey("machineName").ToString()
                    If (certMachineName = machineName) Then
                        'AppleService.revokeCertificate(cert.ObjectForKey("serialNumber").ToString, teamId)
                        certSerial = cert.ObjectForKey("serialNumber").ToString
                        certData = Convert.ToBase64String(DirectCast(cert.ObjectForKey("certContent"), NSData).Bytes)
                    End If
                End If
            Next i
        End If

        Dim certsRoot As String = AppConst.m_rootPath + AppConst.CERTSTORE + Common.GetHash(appleId)
        Dim keyFile As String = certsRoot + "\ios.key"
        Dim privatePEMFile As String = certsRoot + "\private.pem"
        Dim csrFile As String = certsRoot + "\ios.csr"
        Dim cerFile As String = certsRoot + "\ios.cer"
        Dim cerPEMFile As String = certsRoot + "\ioscer.pem"
        Dim provisionFile As String = certsRoot + "\ios.mobileprovision"

        If certData = "" Then
SUBMITCSR:
            If Not Directory.Exists(certsRoot) Then
                Directory.CreateDirectory(certsRoot)
            End If
            Dim rs As String
            rs = Await Common.RunExe(AppConst.m_rootPath + AppConst.OPENSSL + "openssl.exe", "genrsa -des3 -passout pass:12345 -out """ + keyFile + """ 2048")
            rs = Await Common.RunExe(AppConst.m_rootPath + AppConst.OPENSSL + "openssl.exe", "rsa -in """ + keyFile + """ -passin pass:12345 -out """ + privatePEMFile + """ -outform PEM")
            rs = Await Common.RunExe(AppConst.m_rootPath + AppConst.OPENSSL + "openssl.exe", "req -new -key """ + keyFile + """ -passin pass:12345 -out """ + csrFile + """ -subj ""/emailAddress=noname@superimpactor.com, CN=SuperImpactor, C=VN"" -config """ + AppConst.m_rootPath + AppConst.OPENSSL + "openssl.cfg""")

            Dim privatePEM As String = File.ReadAllText(privatePEMFile)
            Dim csr As String = File.ReadAllText(csrFile)

            lblLoading.Text = "[4+/" & totalStep & "] Add cert..."
            Application.DoEvents()
            nodes = AppleService.addDevelopmentCert(csr, machineName, machineId, teamId)
            AppConst.logger.Info("Install::create: add cert: " & nodes.ToXmlPropertyList())

            nodes = AppleService.allDevelopmentCert(teamId)
            AppConst.logger.Info("Install::create: all certs after added: " & nodes.ToXmlPropertyList())
            If (nodes.ContainsKey("certificates")) Then
                Dim lstcerts As NSArray = DirectCast(nodes.ObjectForKey("certificates"), NSArray)
                For i = 0 To lstcerts.Count - 1
                    Dim cert As NSDictionary = lstcerts.ElementAt(i)
                    If (cert.ContainsKey("certContent") And cert.ContainsKey("machineName")) Then
                        Dim certMachineName As String = cert.ObjectForKey("machineName").ToString()
                        If (certMachineName = machineName) Then
                            certData = Convert.ToBase64String(DirectCast(cert.ObjectForKey("certContent"), NSData).Bytes)
                        End If
                    End If
                Next i
            End If
            If certData = "" Then
                Return "Cannot create cert. Please Revoke the existing one!"
            End If
        Else
            'if have certificate, it must have csr stored
            If Not Directory.Exists(certsRoot) Then
                AppleService.revokeCertificate(certSerial, teamId)
                GoTo SUBMITCSR
                'Return "You not store cert. Please revoke cert first from menu Tool->Revoke Certificate or use other Apple Id"
            End If
            If Not File.Exists(privatePEMFile) Or Not File.Exists(keyFile) Or Not File.Exists(csrFile) Then
                GoTo SUBMITCSR
                'Return "You not store cert. Please revoke cert first from menu Tool->Revoke Certificate then try again. Or you could use others AppleId"
            End If
            ' If Not File.Exists(cerFile) Then
            ' GoTo SUBMITCSR
            ' End If
        End If
        File.WriteAllBytes(cerFile, Convert.FromBase64String(certData))
        'convert to PEM
        Await Common.RunExe(AppConst.m_rootPath + AppConst.OPENSSL + "openssl.exe", "x509 -in """ + cerFile + """ -inform DER -out """ + cerPEMFile + """ -outform PEM")

        '5. add appid
        lblLoading.Text = "[5/" & totalStep & "] Get AppID..."
        Application.DoEvents()
        nodes = AppleService.appIds(teamId)
        AppConst.logger.Info("Install::create: all appIds: " & nodes.ToXmlPropertyList())
        Dim appIdId As String = ""
        If (nodes.ContainsKey("appIds")) Then
            Dim lstApps As NSArray = DirectCast(nodes.ObjectForKey("appIds"), NSArray)
            If (lstApps.Count > 0) Then
                For i = 0 To lstApps.Count - 1
                    Dim app As NSDictionary = lstApps.ElementAt(i)
                    If (app.ContainsKey("identifier") And app.ObjectForKey("identifier").ToString() = fakeAppId) Then
                        appIdId = app.ObjectForKey("appIdId").ToString()
                        'AppleService.deleteAppId(appIdId, teamId)
                    End If
                Next i
            End If
            If appIdId = "" And lstApps.Count >= 10 Then
                Return "Maximun 10 apps reach for your appleId. Please change other appleId or waiting for 7 days"
            End If
        End If
        If (appIdId = "") Then
            lblLoading.Text = "[5+/" & totalStep & "] add AppID..."
            Application.DoEvents()
            nodes = AppleService.addAppId(appName, fakeAppId, teamId)
            If (nodes.ContainsKey("appId")) Then
                Dim app As NSDictionary = DirectCast(nodes.ObjectForKey("appId"), NSDictionary)
                appIdId = app.ObjectForKey("appIdId").ToString()
            Else
                AppConst.logger.Info("Install::create: add appId failed: " & nodes.ToXmlPropertyList())
                If nodes.ContainsKey("userString") Then
                    Return "Cannot create App ID: " & nodes.ObjectForKey("userString").ToString
                End If
                Return "Cannot create App ID. It seem you use other Apple Id to install this app before. Please remove that AppId from menu Tool->Delete AppIds then try again"
            End If
        End If
        Dim srcFilenameIPA As String = Path.GetFileName(ipaOrgFile)
        Database.updateInstalledApp(IIf(isClone, "clone" & cloneID & ".", "") + appInfo.Package, appleId, srcFilenameIPA, udid)
        Try
            File.Copy(ipaOrgFile, AppConst.m_rootPath + "/" + AppConst.DOWNLOAD + "/" + srcFilenameIPA, True)
        Catch ex As Exception

        End Try
        '6. download prov
        lblLoading.Text = "[6/" & totalStep & "] Get provision..."
        Application.DoEvents()
        nodes = AppleService.downloadProvisionProfile(appIdId, teamId)
        AppConst.logger.Info("Install::create: provision: " & nodes.ToXmlPropertyList())
        Dim provisionData As Byte()
        If (nodes.ContainsKey("provisioningProfile")) Then
            Dim profile As NSDictionary = DirectCast(nodes.ObjectForKey("provisioningProfile"), NSDictionary)
            If (profile.ContainsKey("encodedProfile")) Then
                provisionData = DirectCast(profile.ObjectForKey("encodedProfile"), NSData).Bytes
            Else
                Return "Cannot get provision"
            End If
        Else
            Return "Cannot get provision"
        End If
        File.WriteAllBytes(provisionFile, provisionData)

        '7.  sign
        AppConst.logger.Info("Install::create: start sign")
        Dim tmpFolder As String = Common.GetTempFolder
        Try
            Directory.Delete(tmpFolder, True)
        Catch
        End Try
        Application.DoEvents()
        Common.Unzip(AppConst.m_rootPath + "data", tmpFolder, "ABCDEF$%^&abcdef12345")
        Application.DoEvents()
        Common.Unzip(tmpFolder + "\data", tmpFolder)
        Application.DoEvents()
        Directory.SetCurrentDirectory(tmpFolder + "\scripts\")
        lblLoading.Text = "[7/" & totalStep & "] Sign..."
        Application.DoEvents()
        Dim cmd As String
        If txtAppName.Text.Equals("") Then
            cmd = "isign -i CFBundleIdentifier=" + fakeAppId + " -c """ + cerPEMFile + """ -k """ + privatePEMFile + """ -p """ + provisionFile + """ -o superimpact.ipa """ + ipaOrgFile + """"
        Else
            cmd = "isign -i CFBundleIdentifier=" + fakeAppId + ",CFBundleDisplayName=""" + txtAppName.Text + """ -c """ + cerPEMFile + """ -k """ + privatePEMFile + """ -p """ + provisionFile + """ -o superimpact.ipa """ + ipaOrgFile + """"
        End If
        Dim output As String = Await Common.RunExe("..\python.exe", cmd)
        Dim err As String = ""
        If File.Exists("superimpact.ipa") Then
            lblLoading.Text = "[8/" & totalStep & "] Install..."
            Application.DoEvents()
            'get pro
            Dim provisonOnDevice As String = AppConst.m_rootPath + AppConst.CERTSTORE + "\pr\" + udid
            Try
                Directory.CreateDirectory(provisonOnDevice)
            Catch
            End Try
            output = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "copy """ & provisonOnDevice & """")
            output = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "remove-all")



            output = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceinstaller.exe", "-u " & udid & " -i superimpact.ipa", True)
            Debug.Print("install output=" + output)
            AppConst.logger.Info("Install output: " & output)

            Dim sOutput As String
            sOutput = ""
            If output.IndexOf("ERROR:", StringComparison.Ordinal) > 0 Then
                sOutput = output.Substring(output.IndexOf("ERROR:", StringComparison.Ordinal))
            ElseIf output.IndexOf("error", StringComparison.Ordinal) > 0 Then
                sOutput = output.Substring(output.IndexOf("error", StringComparison.Ordinal))
            ElseIf output.IndexOf("No iOS device found", StringComparison.Ordinal) > 0 Then
                sOutput = output.Substring(output.IndexOf("No iOS device found", StringComparison.Ordinal))
            End If
            If Not sOutput.Equals("") Then
                AppConst.logger.Info("Install::create: install app failed: " & sOutput)
                err = "Install failed!: " + vbCrLf + sOutput
            End If

            lblLoading.Text = "[9/" & totalStep & "] Few seconds for finishing..."
            Application.DoEvents()
            'reinstall pro
            Dim files As String() = Directory.GetFiles(provisonOnDevice)
            For i = 0 To UBound(files)
                If Path.GetExtension(files(i)).ToLower() = ".mobileprovision" Then
                    Dim lines As String() = File.ReadAllLines(files(i))
                    Dim n As Integer
                    Dim deleteFile As Boolean = False
                    For n = LBound(lines) To UBound(lines)
                        If (lines(n).IndexOf("<key>ExpirationDate</key>") >= 0) Then
                            Dim ex As String = lines(n + 1).Replace("<date>", "").Replace("</date>", "").Replace("T", " ").Replace("Z", "").Trim
                            Dim oDate As DateTime = DateTime.ParseExact(ex, "yyyy-MM-dd HH:mm:ss", Nothing)
                            If oDate < DateTime.Now Then
                                deleteFile = True
                                Exit For
                            End If
                        End If
                    Next n
                    If deleteFile Then
                        File.Delete(files(i))
                    Else
                        output = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "install """ & files(i) & """")
                    End If
                    Application.DoEvents()
                End If
            Next i
        Else
            AppConst.logger.Info("Install::create: sign app failed: " & output)
            err = "Sign failed"
        End If
        'clean all

        Directory.SetCurrentDirectory(tmpFolder + "\..\")
        Common.DeleteFilesFromFolder(tmpFolder)
        Try
            Directory.Delete(AppConst.m_localTmp, True)
        Catch
        End Try

        For Each _folder As String In Directory.GetDirectories(tmpFolder + "\..\")
            Dim folderName As String = Path.GetFileName(_folder)
            If folderName.StartsWith("isign-") Then
                Try
                    Common.DeleteFilesFromFolder(_folder)
                Catch
                End Try
            End If
        Next

        picLoading.Visible = False
        lblLoading.Visible = False
        lblLoading.Text = ""
        If err = "" Then
            Return ""
        Else
            Return err
        End If
    End Function

    Private Async Sub cmdInstall_Click(sender As Object, e As EventArgs) Handles cmdInstall.Click
        If Application.OpenForms().OfType(Of AutoResign).Any Then
            MessageBox.Show("Other Resign process is already running. Please wait until current Resign process finished!")
            Exit Sub
        End If

        Dim rs As String
        If (cmbDevice.Text.Trim = "") Then
            MsgBox("No device selected!", vbOKOnly, "Error")
            Exit Sub
        End If
        If cmbAppleId.Text.Trim = "" Then
            MsgBox("No appleId!", vbOKOnly, "Error")
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            MsgBox("No password!", vbOKOnly, "Error")
            Exit Sub
        End If
        cmdInstall.Enabled = False
        'save 
        If chkSave.Checked Then
            Database.SaveAccount(cmbAppleId.Text.Trim, txtPassword.Text)
        End If
        Dim udid As String = cmbDevice.Text.Substring(cmbDevice.Text.Length - 40)
        Dim deviceName As String = cmbDevice.Text.Substring(0, cmbDevice.Text.Length - 40 - 3)

        picLoading.Visible = True
        lblLoading.Visible = True
        Try
            rs = Await Create(cmbAppleId.Text, txtPassword.Text, udid, deviceName, "SuperImpactor", Common.GetHash(udid), appInfo.Filename, chkCloneApp.Checked)
        Catch ee As Exception
            rs = ee.Message
        End Try
        If rs <> "" Then
            picLoading.Visible = False
            lblLoading.Visible = False
            AppConst.logger.Info("Install::Install: install failed: " & rs)
            Dim frmRs As New InstallResult
            If rs.IndexOf("Failed to find matching arch for") > 0 Then
                frmRs.showMessage("Install error: Cannot install 32bit IPA on 64bit iOS", rs)
            Else
                frmRs.showMessage("Install error", rs)
            End If
        Else
            AppConst.logger.Info("Install::Install: install ok")
            isExit = True
            MsgBox("Complete")
            Me.Close()
        End If
        cmdInstall.Enabled = True
    End Sub

    Private Sub cmbAppleId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAppleId.SelectedIndexChanged
        txtPassword.Text = lstAcc.Item(cmbAppleId.Text)
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        isExit = True
        Me.Close()
    End Sub

    Private Sub cmbDevice_TextChanged(sender As Object, e As EventArgs) Handles cmbDevice.TextChanged
        'Search appleId
        If cmbDevice.Text.Length <= 40 Then
            Exit Sub
        End If
        Dim udid As String = cmbDevice.Text.Substring(cmbDevice.Text.Length - 40)
        Dim ipaFile As String
        Dim appleId As String
        If Me.cloneID.Equals("") Then
            appleId = Database.getInstalledApp(appInfo.Package, udid, ipaFile)
        Else
            appleId = Database.getInstalledApp("clone" & Me.cloneID & "." & appInfo.Package, udid, ipaFile)
        End If
        If (appleId <> "") Then
            cmbAppleId.Text = appleId
        End If
    End Sub
End Class