Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions
Imports Claunia.PropertyList

Public Class AutoResign

    Delegate Sub ChangeTextsSafe(no As String, appInf As AppInfos)
    Delegate Sub AddLogSafe(log As String)
    Delegate Sub CompleteSafe(ByVal cancelled As Boolean)
    Private lstAppInfo As List(Of AppInfosResign)
    Private udid As String
    Private deviceName As String
    Private machineName As String
    Private machineId As String
    Private isRunning As Boolean = False

    Private Sub Complete(ByVal cancelled As Boolean)
        isRunning = False
        If (cancelled) Then
            Me.Close()
            Exit Sub
        End If
        Dim i As Integer = lstStatus.Items.Add("Complete")
        picLoading.Visible = False
        cmdCancel.Visible = False
    End Sub

    Private Sub AddLog(log As String)
        lstStatus.Items.Add(log)
        AppConst.logger.Debug("Resign all: " & log)
        'Dim ra As ListViewItem = lstStatus.Items.Item(i)
        'ra.EnsureVisible()
    End Sub

    Private Sub ChangeTexts(no As String, appInf As AppInfosResign)
        lblNo.Text = "Processing No.: " & no
        lblAppName.Text = "App name: " & appInf.Name
        lblFile.Text = "IPA file: " & appInf.Filename
    End Sub

    Public Sub ResignAsync(lstAppInfos As List(Of AppInfosResign), udid As String, deviceName As String, machineName As String, machineId As String)
        lstAppInfo = lstAppInfos
        Me.udid = udid
        Me.deviceName = deviceName
        Me.machineName = machineName
        Me.machineId = machineId
        BackgroundWorker1.WorkerSupportsCancellation = True
        Show()
        picLoading.Visible = True
        BackgroundWorker1.RunWorkerAsync() 'Start download
    End Sub

    Public Async Function Resign(appInfo As AppInfos, appleId As String, password As String, udid As String, deviceName As String, machineName As String, machineId As String, Optional cloneId As String = "") As Task
        Dim fakeAppId As String = "si." + Common.GetHash(Trim(appleId)) + "." + IIf(cloneId.Equals(""), "", "clone" & cloneId & ".") + Regex.Replace(appInfo.Package, "[^a-zA-Z.]", "")
        Dim appName As String = Regex.Replace("SI - " + appInfo.Name, "[^a-zA-Z ]", "")
        Dim ipaOrgFile As String = AppConst.m_rootPath + AppConst.DOWNLOAD + appInfo.Filename
        AppConst.logger.Info("Install::create: " & deviceName & "," & machineName & "," & machineId & "," & ipaOrgFile & "    id=" & fakeAppId & "," & appName)
        Dim addLogDelegate As New AddLogSafe(AddressOf AddLog)

        '1. Login
        Dim accInfo As String
        Dim totalStep As Integer = 9
        Try
            Me.Invoke(addLogDelegate, vbTab & "[1/" & totalStep & "] Login... " & appleId)
        Catch
        End Try

        Application.DoEvents()
        Dim nodes As NSDictionary = AppleService.login(appleId, password)
        If (Not nodes.ContainsKey("myacinfo")) Then
            AppConst.logger.Info("Install::create: cannot login itune: " & nodes.ToXmlPropertyList())
            Try
                Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot login itune...")
            Catch
            End Try
            Return
        End If
        accInfo = nodes.ObjectForKey("myacinfo").ToString()

        '2. Get team
        Try
            Me.Invoke(addLogDelegate, vbTab & "[2/" & totalStep & "] Get team...")
        Catch
        End Try
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
            Try
                Me.Invoke(addLogDelegate, vbTab & "ERROR: Not have teamId")
            Catch
            End Try
            Return
        End If

        '3. Device
        Try
            Me.Invoke(addLogDelegate, vbTab & "[3/" & totalStep & "] Get devices...")
        Catch
        End Try
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
            Try
                Me.Invoke(addLogDelegate, vbTab & "[3+/" & totalStep & "] Add device...")
            Catch
            End Try
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
                    Try
                        Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot add device: " & nodes.ObjectForKey("userString").ToString)
                    Catch
                    End Try
                    Return
                End If
                Try
                    Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot add device")
                Catch
                End Try
                Return
            End If
        End If

        '4. get cert
        Try
            Me.Invoke(addLogDelegate, vbTab & "[4/" & totalStep & "] Get certs...")
        Catch
        End Try
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
                        AppleService.revokeCertificate(cert.ObjectForKey("serialNumber").ToString, teamId)
                        'certSerial = cert.ObjectForKey("serialNumber").ToString
                        'certData = Convert.ToBase64String(DirectCast(cert.ObjectForKey("certContent"), NSData).Bytes)
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

            Try
                Me.Invoke(addLogDelegate, vbTab & "[4+/" & totalStep & "] Add cert...")
            Catch
            End Try
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
                Try
                    Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot create cert. Please Revoke the existing one!")
                Catch
                End Try
                Return
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
        Try
            Me.Invoke(addLogDelegate, vbTab & "[5/" & totalStep & "] Get AppID...")
        Catch
        End Try
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
                Try
                    Me.Invoke(addLogDelegate, vbTab & "ERROR: Maximun 10 apps reach for your appleId. Please change other appleId or waiting for 7 days")
                Catch
                End Try
                Return
            End If
        End If
        If (appIdId = "") Then
            Try
                Me.Invoke(addLogDelegate, vbTab & "[5+/" & totalStep & "] add AppID...")
            Catch
            End Try
            Application.DoEvents()
            nodes = AppleService.addAppId(appName, fakeAppId, teamId)
            If (nodes.ContainsKey("appId")) Then
                Dim app As NSDictionary = DirectCast(nodes.ObjectForKey("appId"), NSDictionary)
                appIdId = app.ObjectForKey("appIdId").ToString()
            Else
                AppConst.logger.Info("Install::create: add appId failed: " & nodes.ToXmlPropertyList())
                If nodes.ContainsKey("userString") Then
                    Try
                        Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot create App ID: " & nodes.ObjectForKey("userString").ToString)
                    Catch
                    End Try
                    Return
                End If
                Try
                    Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot create App ID. It seem you use other Apple Id to install this app before. Please remove that AppId from menu Tool->Delete AppIds then try again")
                Catch
                End Try
                Return
            End If
        End If

        Database.updateInstalledApp(IIf(cloneId.Equals(""), "", "clone" & cloneId & ".") + appInfo.Package, appleId, Path.GetFileName(ipaOrgFile), udid)

        '6. download prov
        Try
            Me.Invoke(addLogDelegate, vbTab & "[6/" & totalStep & "] Get provision...")
        Catch
        End Try
        Application.DoEvents()
        nodes = AppleService.downloadProvisionProfile(appIdId, teamId)
        AppConst.logger.Info("Install::create: provision: " & nodes.ToXmlPropertyList())
        Dim provisionData As Byte()
        If (nodes.ContainsKey("provisioningProfile")) Then
            Dim profile As NSDictionary = DirectCast(nodes.ObjectForKey("provisioningProfile"), NSDictionary)
            If (profile.ContainsKey("encodedProfile")) Then
                provisionData = DirectCast(profile.ObjectForKey("encodedProfile"), NSData).Bytes
            Else
                Try
                    Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot get provision")
                Catch
                End Try
                Return
            End If
        Else
            Try
                Me.Invoke(addLogDelegate, vbTab & "ERROR: Cannot get provision")
            Catch
            End Try
            Return
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
        Try
            Me.Invoke(addLogDelegate, vbTab & "[7/" & totalStep & "] Sign...")
        Catch
        End Try
        Application.DoEvents()
        Dim output As String = Await Common.RunExe("..\python.exe", "isign -i CFBundleIdentifier=" + fakeAppId + ",CFBundleDisplayName=""" + appInfo.Name + """ -c """ + cerPEMFile + """ -k """ + privatePEMFile + """ -p """ + provisionFile + """ -o superimpact.ipa """ + ipaOrgFile + """")
        Dim err As String = ""
        If File.Exists("superimpact.ipa") Then
            Try
                Me.Invoke(addLogDelegate, vbTab & "[8/" & totalStep & "] Install...")
            Catch
            End Try
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
            Try
                Me.Invoke(addLogDelegate, vbTab & "[9/" & totalStep & "] Few seconds for finishing...")
            Catch
            End Try
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

        If err = "" Then
            Try
                Me.Invoke(addLogDelegate, vbTab & "-----> SUCCESS")
            Catch
            End Try
        Else
            Try
                Me.Invoke(addLogDelegate, vbTab & "-----> ERROR: " & err)
            Catch
            End Try
        End If
    End Function

    Private Async Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim completeDelegate As New CompleteSafe(AddressOf Complete)
        Dim changeTextDelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Dim addLogDelegate As New AddLogSafe(AddressOf AddLog)
        isRunning = True
        For i = 0 To lstAppInfo.Count - 1
            Dim app As AppInfosResign = lstAppInfo.Item(i)
            Try
                Me.Invoke(changeTextDelegate, (i + 1).ToString & "/" & lstAppInfo.Count.ToString, app)
                Me.Invoke(addLogDelegate, "Resign app: '" & app.Name & "'")
            Catch
            End Try
            If BackgroundWorker1.CancellationPending Then 'If user abort download
                Try
                    Me.Invoke(completeDelegate, True)
                Catch
                End Try
                isRunning = False
                Return
            End If

            If app.appleId = "" Then
                Try
                    Me.Invoke(addLogDelegate, "ERROR: Not find appleId that used to sign this app before. Try install/resign the single app again")
                Catch
                End Try
            Else
                Try
                    Await Resign(DirectCast(app, AppInfos), app.appleId, app.password, udid, deviceName, machineName, machineId, app.cloneId)
                Catch ee As Exception
                    Try
                        Me.Invoke(addLogDelegate, "-----> ERROR: exception: " & ee.Message)
                    Catch
                    End Try
                End Try
            End If
            If BackgroundWorker1.CancellationPending Then 'If user abort download
                Try
                    Me.Invoke(completeDelegate, True)
                Catch
                End Try
                isRunning = False
                Return
            End If
        Next
        Try
            Me.Invoke(completeDelegate, False)
        Catch ee As Exception
            MsgBox("Error: " & ee.Message)
        End Try
        isRunning = False
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        If isRunning Then
            Me.BackgroundWorker1.CancelAsync() 'Send cancel request
            cmdCancel.Text = "Waiting..."
            cmdCancel.Enabled = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub AutoResign_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AutoResign_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If isRunning Then
            cmdCancel_Click(sender, e)
            e.Cancel = True
        Else
        End If
    End Sub

    Private Sub picLoading_Click(sender As Object, e As EventArgs) Handles picLoading.Click

    End Sub

    Private Sub lstStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstStatus.SelectedIndexChanged

    End Sub
End Class