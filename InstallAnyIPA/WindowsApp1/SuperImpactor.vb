Imports System.Data.SQLite
Imports System.IO
Imports System.Net
Imports System.Threading
Imports Claunia.PropertyList
Imports ICSharpCode.SharpZipLib
Imports SuperImpactor
Imports NetSparkle
Imports System.Reflection
Imports System.Xml

Public Class frmSuperImpactor
    Private Declare Auto Function SendMessage Lib "user32" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const LVM_FIRST As Long = &H1000
    Public Const LVM_SETICONSPACING As Long = LVM_FIRST + 53
    Private crrTools As Integer
    Dim lstAcc As Dictionary(Of String, String)
    Dim lstProvision As Dictionary(Of String, ProvisionInfo) = New Dictionary(Of String, ProvisionInfo)

    Dim sparkle As Sparkle
    Dim isNewApp As Boolean = False
    Dim WithEvents dt As System.Windows.Forms.Timer

    Private Sub dt_Tick() Handles dt.Tick
        dt.Stop()
        cmdCheckForUpdate.PerformClick()
    End Sub

    Public Sub SetListViewSpacing(lst As ListView, x As Integer, y As Integer)
        SendMessage(lst.Handle, LVM_SETICONSPACING, 0, x * 65536 + y)
    End Sub

    Private Async Sub ReloadApps(Optional search As String = "")
        Try
            Dim Sql As String
            If search = "" Then
                Sql = "select * from list_app order by name limit 50"
            Else
                Sql = "select * from list_app where name like '%" + search + "%' order by name limit 50"
            End If
            Dim Command As SQLiteCommand
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Dim reader As SQLiteDataReader = Command.ExecuteReader()
            lstApps.Clear()
            lstApps.Columns.Add("Apps", Me.Width - 20)
            Dim tClient As WebClient = New WebClient
            While reader.Read()
                Dim lvwExtra As New ExtraData
                lvwExtra.DescText = reader.Item("description")
                lvwExtra.HeaderText = reader.Item("name")
                lvwExtra.MinorText = "from " + Trim(AppConst.m_lstCydiaRepos.Item(reader.Item("cydia_repos"))) + " [" + reader.Item("section") + "]"
                lvwExtra.AppDetailObj = New AppInfos
                With lvwExtra.AppDetailObj
                    .Icon = reader.Item("icon").ToString()
                    .Architecture = reader.Item("arch").ToString()
                    .Author = reader.Item("author").ToString()
                    .Depends = reader.Item("depends").ToString()
                    .Description = reader.Item("description").ToString()
                    .Filename = reader.Item("filename").ToString()
                    .Homepage = reader.Item("homepage").ToString()
                    .Name = reader.Item("name").ToString()
                    .Package = reader.Item("package").ToString()
                    .Section = reader.Item("section").ToString()
                    .Size = reader.Item("size").ToString()
                    .Version = reader.Item("version").ToString()
                End With
                If lvwExtra.AppDetailObj.Icon <> "" And lvwExtra.AppDetailObj.Icon.StartsWith("http") Then
                    Using MemStream As New MemoryStream(Await tClient.DownloadDataTaskAsync(lvwExtra.AppDetailObj.Icon)) ' wait the downloading and append into memory stream
                        lvwExtra.MainImage = Bitmap.FromStream(MemStream)  ' add a list with image from loaded memory stream.
                    End Using

                    'lvwExtra.MainImage = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(lvwExtra.AppDetailObj.Icon)))
                Else
                    lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("apptweak"), Bitmap))
                End If
                Application.DoEvents()
                lvwExtra.ButtonText1 = "DOWNLOAD"
                Dim lvw As ListViewItem = lstApps.Items.Add("")
                lvw.Tag = lvwExtra
                Application.DoEvents()
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub frmSuperImpactor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Get the logger as named in the configuration file.
            AppConst.logger = log4net.LogManager.GetLogger("SuperImpactor")
            AppConst.logger.Info("Start ver " & Assembly.GetExecutingAssembly().GetName().Version.ToString)
        Catch ex As Exception
        Finally
        End Try

        sparkle = New Sparkle("http://superimpactor.net/release/versions.xml", Me.Icon)
        'sparkle.EnableSilentMode = False
        AppConst.mainForm = Me
        homeBrower.ScriptErrorsSuppressed = True
        Try
            homeBrower.Url = New Uri("http://home.superimpactor.net/?ver=" & System.Web.HttpUtility.UrlEncode(Assembly.GetExecutingAssembly().GetName().Version.ToString))
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
        AppConst.m_localTmp = Common.GetTempFolder

        Dim appPath As String = Application.StartupPath()
        AppConst.m_rootPath = appPath + "\..\..\"
        If Not File.Exists(AppConst.m_rootPath + AppConst.DB) Then
            AppConst.m_rootPath = appPath + "\"
            If Not File.Exists(AppConst.m_rootPath + AppConst.DB) Then
                MsgBox("Application error! Please install again!", MsgBoxStyle.ApplicationModal, "Error")
                End
            End If
        End If
        AppConst.m_dbConnection = New SQLiteConnection("Data Source=" + AppConst.m_rootPath + AppConst.DB + ";Version=3;")
        AppConst.m_dbConnection.Open()
        SetListViewSpacing(lstApps, 16, 16)

        AppConst.m_lstCydiaRepos = Database.GetCydiaRepos()
        crrTools = 0
        lblCydiaRepos.BackColor = Color.FromName("GradientActiveCaption")
        lblCydiaRepos_Click(Nothing, Nothing)

        For i = 0 To 16
            AppConst.m_randomKey = AppConst.m_randomKey + Char.ConvertFromUtf32(i * 2 + 64)
        Next
        lstAcc = Database.GetAccounts

        picHomeBtn_Click(Nothing, Nothing)

        Dim trd As Thread = New Thread(AddressOf ThreadTask)
        'trd.SetApartmentState(ApartmentState.STA)
        trd.Start()

        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionHandler

        Me.Show()
        sparkle.StartLoop(True)
        If sparkle.CheckForUpdatesQuietly() = Sparkle.UpdateStatus.UpdateAvailable Then
            dt = New System.Windows.Forms.Timer
            dt.Interval = 10000
            dt.Start()
        End If

        If File.Exists(AppConst.m_rootPath + "/" + AppConst.LOCALTMP + "/running.pid") Then
            'send 
            ReportBug.sendLog("crashreport", "auto")
        Else
            File.WriteAllText(AppConst.m_rootPath + "/" + AppConst.LOCALTMP + "/running.pid", Date.Now)
        End If

    End Sub

    Public Shared Sub UnhandledExceptionHandler(ByVal sender As Object, ByVal args As UnhandledExceptionEventArgs)
        AppConst.logger.Error("Exception: " & CType(args.ExceptionObject, Exception).Message & " - " & CType(args.ExceptionObject, Exception).StackTrace)
    End Sub

    Private Sub picHomeBtn_Click(sender As Object, e As EventArgs) Handles picHomeBtn.Click
        picAppBtn.BorderStyle = BorderStyle.None
        picDeviceBtn.BorderStyle = BorderStyle.None
        picDownloadBtn.BorderStyle = BorderStyle.None
        picHomeBtn.BorderStyle = BorderStyle.Fixed3D
        picToolBtn.BorderStyle = BorderStyle.None

        isShowDownload = False
        panelHome.BringToFront()
    End Sub

    Dim firstTimeLoadApp As Boolean = True
    Private Sub picAppBtn_Click(sender As Object, e As EventArgs) Handles picAppBtn.Click
        picAppBtn.BorderStyle = BorderStyle.Fixed3D
        picDeviceBtn.BorderStyle = BorderStyle.None
        picDownloadBtn.BorderStyle = BorderStyle.None
        picHomeBtn.BorderStyle = BorderStyle.None
        picToolBtn.BorderStyle = BorderStyle.None

        panelApp.BringToFront()
        isShowDownload = False
        If firstTimeLoadApp Then
            firstTimeLoadApp = False
            Dim searchFnc As New MySearchMethodDelegrate(AddressOf MySearchMethod)
            Me.Invoke(searchFnc)
        End If

    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim frm As UpdateCydia = New UpdateCydia
        If frm.UpdateApp Then
            'ReloadApps()
        End If

    End Sub
    Private mint_LastReceivedTimerID As Integer = 0
    Private mint_LastInitializedTimerID As Integer = 0

    Private Sub txtAppSearch_TimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        'Increment the counter for the number of times timers have elapsed
        mint_LastReceivedTimerID = mint_LastReceivedTimerID + 1

        'If the total number of textbox changes equals the total number of times timers have elapsed (fire method for only the latest character change)
        If mint_LastReceivedTimerID = mint_LastInitializedTimerID Then
            'Fire method on the Main UI Thread
            Dim searchFnc As New MySearchMethodDelegrate(AddressOf MySearchMethod)
            Me.Invoke(searchFnc)
        End If
    End Sub

    Delegate Sub MySearchMethodDelegrate()

    Private Sub MySearchMethod()
        ReloadApps(txtAppSearch.Text)
    End Sub

    Private Sub txtAppSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAppSearch.TextChanged
        'Increment the counter for the number of times the textbox has been changed
        mint_LastInitializedTimerID = mint_LastInitializedTimerID + 1

        'Wait longer for shorter strings or strings without spaces
        Dim intMilliseconds As Integer = 500
        If txtAppSearch.Text.Length >= 6 Then
            intMilliseconds = 250
        End If
        If txtAppSearch.Text.Contains(" ") = True And txtAppSearch.Text.EndsWith(" ") = False Then
            intMilliseconds = 175
        End If

        Dim objTimer As New System.Timers.Timer(intMilliseconds)
        AddHandler objTimer.Elapsed, AddressOf txtAppSearch_TimerElapsed

        objTimer.AutoReset = False
        objTimer.Enabled = True
    End Sub


    Private Sub lstApps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstApps.SelectedIndexChanged

    End Sub

    Private Sub picToolBtn_Click(sender As Object, e As EventArgs) Handles picToolBtn.Click
        picAppBtn.BorderStyle = BorderStyle.None
        picDeviceBtn.BorderStyle = BorderStyle.None
        picDownloadBtn.BorderStyle = BorderStyle.None
        picHomeBtn.BorderStyle = BorderStyle.None
        picToolBtn.BorderStyle = BorderStyle.Fixed3D

        isShowDownload = False
        panelTools.BringToFront()
    End Sub

    Private Class DeviceInfo
        Public udid As String = ""
        Public deviceName As String = ""
        Public deviceClass As String = ""
        Public productVersion As String = ""
        Public serialNumber As String = ""
        Public modelNumber As String = ""
        Public phoneNumber As String = ""
    End Class

    Private trd As Thread
    Delegate Sub DeviceInvokeDelegate()
    Dim isExit As Boolean
    Dim listDevice As List(Of DeviceInfo) = New List(Of DeviceInfo)


    Private Sub ThreadTask()
        Dim webClient As New System.Net.WebClient
        Dim i As Integer = 0


        Do While Not isExit
            i = (i + 1) Mod 10

            Try
                If i = 0 Then
                    detectDevice()
                    Me.BeginInvoke(New DeviceInvokeDelegate(AddressOf DeviceInvokeMethod))
                End If
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Thread.Sleep(200)
        Loop
    End Sub

    Public Sub DeviceInvokeMethod()
        cmbDevice.Items.Clear()
        For i = 0 To listDevice.Count - 1
            cmbDevice.Items.Add(listDevice.Item(i).deviceName + " - " + listDevice.Item(i).udid)
        Next i
        'Console.WriteLine(sOutput)
        If cmbDevice.Items.Count = 0 Then
            cmbDevice.Text = ""
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
        Dim deviceClass As String = ""
        Dim productVersion As String = ""
        Dim serialNumber As String = ""
        Dim modelNumber As String = ""
        Dim phoneNumber As String = ""

        For i = 0 To line.Length - 1
            If line(i).StartsWith("UniqueDeviceID") Then
                udid = line(i).Substring("UniqueDeviceID:".Length).Trim()
            ElseIf line(i).StartsWith("DeviceName") Then
                deviceName = line(i).Substring("DeviceName:".Length).Trim()
            ElseIf line(i).StartsWith("ProductVersion") Then
                productVersion = line(i).Substring("ProductVersion:".Length).Trim()
            ElseIf line(i).StartsWith("DeviceClass") Then
                deviceClass = line(i).Substring("DeviceClass:".Length).Trim()
            ElseIf line(i).StartsWith("SerialNumber") Then
                serialNumber = line(i).Substring("SerialNumber:".Length).Trim()
            ElseIf line(i).StartsWith("ModelNumber") Then
                modelNumber = line(i).Substring("ModelNumber:".Length).Trim()
            ElseIf line(i).StartsWith("PhoneNumber") Then
                phoneNumber = line(i).Substring("PhoneNumber:".Length).Trim()
            End If
        Next
        listDevice.Clear()
        If (udid <> "" And deviceName <> "") Then
            Dim device As DeviceInfo = New DeviceInfo
            device.udid = udid
            device.deviceName = deviceName
            device.productVersion = productVersion
            device.deviceClass = deviceClass
            device.serialNumber = serialNumber
            device.modelNumber = modelNumber
            device.phoneNumber = phoneNumber
            listDevice.Add(device)
        End If

    End Function

    Private Sub picDeviceBtn_Click(sender As Object, e As EventArgs) Handles picDeviceBtn.Click
        picAppBtn.BorderStyle = BorderStyle.None
        picDeviceBtn.BorderStyle = BorderStyle.Fixed3D
        picDownloadBtn.BorderStyle = BorderStyle.None
        picHomeBtn.BorderStyle = BorderStyle.None
        picToolBtn.BorderStyle = BorderStyle.None

        isShowDownload = False
        panelDevice.BringToFront()
        cmbDevice.Text = ""

    End Sub

    Private Sub lstApps_Button1Click(sender As Object, e As FileEventArgs) Handles lstApps.Button1Click
        Dim ipaLink As String
        Dim ipaName As String
        Dim downloadPro As New DownloadProgress
        If e.data.AppDetailObj.Filename.IndexOf("dailyuploads.net") >= 0 Then
            ipaLink = e.data.AppDetailObj.Filename
            ipaName = "checking...ipa"
        Else
            If e.data.AppDetailObj.Filename.EndsWith(".deb") Then
                ipaName = e.data.AppDetailObj.Name & ".deb"
            End If
            ipaLink = e.data.AppDetailObj.Filename
        End If
        Common.Download(downloadPro, ipaLink, ipaName)

    End Sub

    Private Sub lstApps_Button2Click(sender As Object, e As FileEventArgs) Handles lstApps.Button2Click

    End Sub

    Private Sub lstApps_DoubleClick(sender As Object, e As EventArgs) Handles lstApps.DoubleClick
        If lstApps.SelectedItems.Count > 0 Then
            Dim frmDetail As AppDetail = New AppDetail
            frmDetail.showApp(DirectCast(lstApps.SelectedItems.Item(0).Tag, ExtraData).AppDetailObj)
        End If
    End Sub
    Private isShowDownload As Boolean = False

    Private Sub picDownloadBtn_Click(sender As Object, e As EventArgs) Handles picDownloadBtn.Click
        picAppBtn.BorderStyle = BorderStyle.None
        picDeviceBtn.BorderStyle = BorderStyle.None
        picDownloadBtn.BorderStyle = BorderStyle.Fixed3D
        picHomeBtn.BorderStyle = BorderStyle.None
        picToolBtn.BorderStyle = BorderStyle.None

        isShowDownload = True
        panelDownloads.BringToFront()
        lstDownloads.Clear()
        lstDownloads.View = View.Details
        lstDownloads.Columns.Add("Name", Me.Width - 20)
        Dim files As String() = Directory.GetFiles(AppConst.m_rootPath + AppConst.DOWNLOAD, "*.ipa")
        Dim i As Integer
        For i = 0 To UBound(files)
            Dim myFile As New FileInfo(files(i))
            Dim size As String
            If myFile.Length / 1024 / 1024 / 1024 >= 1 Then
                size = Math.Round(myFile.Length / 1024 / 1024 / 1024, 2).ToString + " GB"
            ElseIf myFile.Length / 1024 / 1024 >= 1 Then
                size = Math.Round(myFile.Length / 1024 / 1024, 2).ToString + " MB"
            Else
                size = Math.Round(myFile.Length / 1024, 2).ToString + " KB"
            End If

            Dim lvwExtra As New ExtraData
            lvwExtra.HeaderText = myFile.Name
            lvwExtra.MinorText = size
            lvwExtra.DescText = myFile.LastWriteTime
            If File.Exists(myFile.FullName & ".png") Then
                lvwExtra.MainImage = New Bitmap(myFile.Name & ".png")
            Else
                lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("ipafile"), Bitmap))
            End If
            lvwExtra.ButtonText1 = "INSTALL"
            lvwExtra.ButtonText2 = "REMOVE"
            Dim lvw As ListViewItem = lstDownloads.Items.Add("")
            lvw.Tag = lvwExtra

        Next

    End Sub

    Private Sub lstDownloads_Button1Click(sender As Object, e As FileEventArgs) Handles lstDownloads.Button1Click
        Dim frm As New Install
        frm.installFromFile(AppConst.m_rootPath + AppConst.DOWNLOAD + e.data.HeaderText)
    End Sub

    Private Sub lstDownloads_Button2Click(sender As Object, e As FileEventArgs) Handles lstDownloads.Button2Click
        If lstDownloads.SelectedItems.Count > 0 Then
            File.Delete(AppConst.m_rootPath + AppConst.DOWNLOAD + DirectCast(lstDownloads.SelectedItems.Item(0).Tag, ExtraData).HeaderText)
            lstDownloads.Items.Remove(lstDownloads.SelectedItems.Item(0))
        Else
            MsgBox("No file selected")
        End If
    End Sub

    Public Sub onDownloadComplete()
        If isShowDownload Then
            picDownloadBtn_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub panelDownloads_Paint(sender As Object, e As PaintEventArgs) Handles panelDownloads.Paint

    End Sub

    Private Sub lblDeleteAppId_MouseLeave(sender As Object, e As EventArgs) Handles lblDeleteAppId.MouseLeave
        If crrTools = 3 Then
            lblDeleteAppId.BackColor = Color.FromName("GradientActiveCaption")
        Else
            lblDeleteAppId.BackColor = Color.FromName("WhiteSmoke")
        End If
    End Sub

    Private Sub lblDeleteAppId_MouseMove(sender As Object, e As MouseEventArgs) Handles lblDeleteAppId.MouseMove
        lblDeleteAppId.BackColor = Color.FromName("Info")
    End Sub

    Private Sub lblRevokeCert_MouseMove(sender As Object, e As MouseEventArgs) Handles lblRevokeCert.MouseMove
        lblRevokeCert.BackColor = Color.FromName("Info")
    End Sub

    Private Sub lblRevokeCert_MouseLeave(sender As Object, e As EventArgs) Handles lblRevokeCert.MouseLeave
        If crrTools = 2 Then
            lblRevokeCert.BackColor = Color.FromName("GradientActiveCaption")
        Else
            lblRevokeCert.BackColor = Color.FromName("WhiteSmoke")
        End If
    End Sub

    Private Sub lblAccount_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAccount.MouseMove
        lblAccount.BackColor = Color.FromName("Info")
    End Sub

    Private Sub lblAccount_MouseLeave(sender As Object, e As EventArgs) Handles lblAccount.MouseLeave
        If crrTools = 1 Then
            lblAccount.BackColor = Color.FromName("GradientActiveCaption")
        Else
            lblAccount.BackColor = Color.FromName("WhiteSmoke")
        End If
    End Sub

    Private Sub lblCydiaRepos_MouseMove(sender As Object, e As MouseEventArgs) Handles lblCydiaRepos.MouseMove
        lblCydiaRepos.BackColor = Color.FromName("Info")
    End Sub

    Private Sub lblCydiaRepos_MouseLeave(sender As Object, e As EventArgs) Handles lblCydiaRepos.MouseLeave
        If crrTools = 0 Then
            lblCydiaRepos.BackColor = Color.FromName("GradientActiveCaption")
        Else
            lblCydiaRepos.BackColor = Color.FromName("WhiteSmoke")
        End If
    End Sub

    Private Sub lblRevokeCert_Click(sender As Object, e As EventArgs) Handles lblRevokeCert.Click
        crrTools = 2
        lblRevokeCert.BackColor = Color.FromName("GradientActiveCaption")
        lblCydiaRepos.BackColor = Color.FromName("WhiteSmoke")
        lblDeleteAppId.BackColor = Color.FromName("WhiteSmoke")
        lblAccount.BackColor = Color.FromName("WhiteSmoke")
        cmbAppleId.Items.Clear()
        For Each kvp As KeyValuePair(Of String, String) In lstAcc
            cmbAppleId.Items.Add(kvp.Key)
        Next
        lstRevoke.Clear()
        childPanelRevoke.BringToFront()
    End Sub

    Private Sub lblCydiaRepos_Click(sender As Object, e As EventArgs) Handles lblCydiaRepos.Click
        crrTools = 0
        lblCydiaRepos.BackColor = Color.FromName("GradientActiveCaption")
        lblRevokeCert.BackColor = Color.FromName("WhiteSmoke")
        lblDeleteAppId.BackColor = Color.FromName("WhiteSmoke")
        lblAccount.BackColor = Color.FromName("WhiteSmoke")
        lstCydia.Clear()
        LoadCydiaRepos()
        childPanelRepo.BringToFront()
    End Sub

    Private Sub lblAccount_Click(sender As Object, e As EventArgs) Handles lblAccount.Click
        crrTools = 1
        lblAccount.BackColor = Color.FromName("GradientActiveCaption")
        lblCydiaRepos.BackColor = Color.FromName("WhiteSmoke")
        lblDeleteAppId.BackColor = Color.FromName("WhiteSmoke")
        lblRevokeCert.BackColor = Color.FromName("WhiteSmoke")
        childPanelAppleIds.BringToFront()
        LoadAccounts()
    End Sub

    Private Sub LoadAccounts()
        lstAcc = Database.GetAccounts()
        lstAccount.Clear()
        lstAccount.View = View.Details
        lstAccount.Columns.Add("Saved Apple Ids", childPanelAppleIds.Width - 5)
        For i = 0 To lstAcc.Keys.Count - 1
            Dim lvwExtra As New ExtraData
            lvwExtra.HeaderText = lstAcc.Keys.ElementAt(i)
            lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("appleid"), Bitmap))
            lvwExtra.ButtonText2 = "REMOVE"
            Dim lvw As ListViewItem = lstAccount.Items.Add("")
            lvw.Tag = lvwExtra
        Next
    End Sub

    Private Sub lblDeleteAppId_Click(sender As Object, e As EventArgs) Handles lblDeleteAppId.Click
        crrTools = 3
        lblDeleteAppId.BackColor = Color.FromName("GradientActiveCaption")
        lblCydiaRepos.BackColor = Color.FromName("WhiteSmoke")
        lblRevokeCert.BackColor = Color.FromName("WhiteSmoke")
        lblAccount.BackColor = Color.FromName("WhiteSmoke")
        cmbAppleId.Items.Clear()
        For Each kvp As KeyValuePair(Of String, String) In lstAcc
            cmbAppleId.Items.Add(kvp.Key)
        Next
        lstRevoke.Clear()
        childPanelRevoke.BringToFront()
    End Sub

    Private Sub cmbAppleId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAppleId.SelectedIndexChanged
        txtPassword.Text = lstAcc.Item(cmbAppleId.Text)
    End Sub

    Private Sub cmdRefreshRevoke_Click(sender As Object, e As EventArgs) Handles cmdRefreshRevoke.Click
        If cmbAppleId.Text.Trim = "" Then
            MsgBox("No appleId!", vbOKOnly, "Error")
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            MsgBox("No password!", vbOKOnly, "Error")
            Exit Sub
        End If
        cmdRefreshRevoke.Enabled = False
        Dim rs As String
        If lblDeleteAppId.BackColor = Color.FromName("GradientActiveCaption") Then
            rs = LoadApps()
        Else
            rs = LoadCertificates()
        End If
        cmdRefreshRevoke.Enabled = True
        If rs <> "" Then
            MsgBox(rs, vbOKOnly, "Warning")
        End If
    End Sub

    Private Function LoadCertificates()
        Dim accInfo As String
        Application.DoEvents()
        Dim nodes As NSDictionary = AppleService.login(cmbAppleId.Text, txtPassword.Text)
        If (Not nodes.ContainsKey("myacinfo")) Then
            Return "Cannot login itune..."
        End If
        accInfo = nodes.ObjectForKey("myacinfo").ToString()

        '2. Get team
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
            Return "Not have teamId"
        End If

        Application.DoEvents()
        nodes = AppleService.allDevelopmentCert(teamId)
        lstRevoke.Clear()
        lstRevoke.Columns.Add("Name", childPanelRevoke.Width - 5)
        lstRevoke.View = View.Details
        lstRevoke.Tag = teamId
        Dim certData As String = ""
        If (nodes.ContainsKey("certificates")) Then
            Dim lstcerts As NSArray = DirectCast(nodes.ObjectForKey("certificates"), NSArray)
            For i = 0 To lstcerts.Count - 1
                Dim cert As NSDictionary = lstcerts.ElementAt(i)
                If (cert.ContainsKey("name") And cert.ContainsKey("serialNumber")) Then

                    Dim lvwExtra As New ExtraData

                    If cert.ContainsKey("machineName") Then
                        lvwExtra.HeaderText = cert.ObjectForKey("name").ToString() & " - " & cert.ObjectForKey("machineName").ToString()
                    Else
                        lvwExtra.HeaderText = cert.ObjectForKey("name").ToString()
                    End If
                    lvwExtra.MinorText = cert.ObjectForKey("serialNumber").ToString
                    lvwExtra.DescText = cert.ObjectForKey("expirationDate").ToString
                    lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("cert"), Bitmap))
                    lvwExtra.ButtonText2 = "REMOVE"
                    Dim lvw As ListViewItem = lstRevoke.Items.Add("")
                    lvw.Tag = lvwExtra
                End If
            Next i
            If lstRevoke.Items.Count = 0 Then
                Return "No Certificates"
            End If
        End If
        Return ""
    End Function

    Private Sub lstRevoke_Button2Click(sender As Object, e As FileEventArgs) Handles lstRevoke.Button2Click
        If lstRevoke.SelectedItems.Count > 0 Then
            If lblDeleteAppId.BackColor = Color.FromName("GradientActiveCaption") Then
                AppleService.deleteAppId(DirectCast(lstRevoke.SelectedItems.Item(0).Tag, ExtraData).MinorText, lstRevoke.Tag)
            Else
                AppleService.revokeCertificate(DirectCast(lstRevoke.SelectedItems.Item(0).Tag, ExtraData).MinorText, lstRevoke.Tag)
            End If
            lstRevoke.Items.Remove(lstRevoke.SelectedItems.Item(0))
        Else
            MsgBox("No Certificate/AppId selected")
        End If
    End Sub

    Private Function LoadApps()
        Dim accInfo As String
        Application.DoEvents()
        Dim nodes As NSDictionary = AppleService.login(cmbAppleId.Text, txtPassword.Text)
        If (Not nodes.ContainsKey("myacinfo")) Then
            Return "Cannot login itune..."
        End If
        accInfo = nodes.ObjectForKey("myacinfo").ToString()

        '2. Get team
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
            Return "Not have teamId"
        End If

        lstRevoke.Clear()
        lstRevoke.Columns.Add("Name", childPanelRevoke.Width - 5)
        lstRevoke.View = View.Details
        lstRevoke.Tag = teamId


        Application.DoEvents()
        nodes = AppleService.appIds(teamId)
        Dim appIdId As String = ""
        If (nodes.ContainsKey("appIds")) Then
            Dim lstApps As NSArray = DirectCast(nodes.ObjectForKey("appIds"), NSArray)
            For i = 0 To lstApps.Count - 1
                Dim app As NSDictionary = lstApps.ElementAt(i)
                If (app.ContainsKey("appIdId") And app.ContainsKey("name")) Then
                    appIdId = app.ObjectForKey("appIdId").ToString()

                    Dim lvwExtra As New ExtraData
                    lvwExtra.HeaderText = app.ObjectForKey("name").ToString()
                    lvwExtra.MinorText = app.ObjectForKey("appIdId").ToString
                    'lvwExtra.DescText = app.ObjectForKey("expirationDate").ToString
                    lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("appid"), Bitmap))
                    lvwExtra.ButtonText2 = "REMOVE"
                    Dim lvw As ListViewItem = lstRevoke.Items.Add("")
                    lvw.Tag = lvwExtra
                End If
            Next i
            If lstRevoke.Items.Count = 0 Then
                Return "No AppId"
            End If
        End If
        Return ""
    End Function

    Private Sub lstAccount_Button2Click(sender As Object, e As FileEventArgs) Handles lstAccount.Button2Click
        If lstAccount.SelectedItems.Count > 0 Then
            Database.DeleteAccount(DirectCast(lstAccount.SelectedItems.Item(0).Tag, ExtraData).HeaderText)
            LoadAccounts()
        Else
            MsgBox("Select account to remove")
        End If
    End Sub

    Private Function LoadCydiaRepos(Optional cydiaReposName As String = "")
        Dim Sql As String = "select * from cydia_repos order by id"
        Dim Command As SQLiteCommand
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Dim reader As SQLiteDataReader = Command.ExecuteReader()
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Repos", childPanelRepo.Width - 5)
        Dim id As Integer
        id = -1
        While reader.Read()
            Dim lvwExtra As New ExtraData
            lvwExtra.HeaderText = reader.Item("name")
            'lvwExtra.MinorText = app.ObjectForKey("appIdId").ToString
            lvwExtra.MinorText = reader.Item("path")
            lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("repos"), Bitmap))
            lvwExtra.ButtonText2 = "REMOVE"
            Dim lvw As ListViewItem = lstCydia.Items.Add("")
            lvw.Tag = lvwExtra

            If reader.Item("path") = cydiaReposName Then
                id = reader.Item("id")
            End If
        End While
        Return id
    End Function

    Private Sub lstCydia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCydia.SelectedIndexChanged

    End Sub

    Private Async Sub cmdAddCydia_Click(sender As Object, e As EventArgs) Handles cmdAddCydia.Click
        Dim cydiaRepos As String
        cydiaRepos = txtCydia.Text
        If Trim(cydiaRepos) = "" Then
            MsgBox("Incorrect URL", vbOKOnly, "Error")
            Exit Sub
        End If
        If Not cydiaRepos.StartsWith("http") Then
            cydiaRepos = "http://" + cydiaRepos
        End If
        '1. check if exist
        For i = 0 To lstCydia.Items.Count - 1
            Dim lvi As ListViewItem = lstCydia.Items(i)
            If DirectCast(lvi.Tag, ExtraData).MinorText = cydiaRepos Then
                MsgBox("Cydia existed!", MsgBoxStyle.ApplicationModal, "Error")
                Exit Sub
            End If
        Next
        Try
            File.Delete("Release.txt")
            File.Delete("Release")
        Catch
        End Try
        Dim reposName As String = "Noname"
        Try
            Dim webClient As New WebClient
            webClient.DownloadFile(cydiaRepos + "/Release", "Release.txt")
            Dim releaseStr() As String = File.ReadAllLines("Release.txt")
            For i = LBound(releaseStr) To UBound(releaseStr)
                If releaseStr(i).StartsWith("Label:") Then
                    reposName = releaseStr(i).Substring(6)
                    Exit For
                End If
            Next
        Catch
        End Try
        If Not LoadPackages(cydiaRepos) Then
            Return
        End If

        Dim Sql As String = "INSERT INTO cydia_repos(name, path) VALUES(@name, @path)"
        Dim Command As SQLiteCommand
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@name", reposName)
            .AddWithValue("@path", cydiaRepos)
        End With
        Command.ExecuteNonQuery()

        Dim cydiaId = LoadCydiaRepos(cydiaRepos)

        Dim frm As New UpdateCydia
        frm.LoadApp(cydiaId)

        AppConst.m_lstCydiaRepos = Database.GetCydiaRepos()
        Exit Sub

        If File.Exists("Packages") Then
            AppConst.logger.Info("Parse Package")
            Await ParsePackages(cydiaId, ,, cydiaRepos)
            AppConst.logger.Info("End Parse")
        End If
        AppConst.m_lstCydiaRepos = Database.GetCydiaRepos()
    End Sub

    Shared Async Function ParsePackages(cydiaId As Integer, Optional frm As UpdateCydia = Nothing, Optional fn As Object = Nothing, Optional cydiaUrl As String = "") As Task
        Try
            File.Delete("Packages.xml")
        Catch
        End Try
        Dim rs As String = Await Common.RunExe(AppConst.m_rootPath + "/repo2xml.exe", "Packages Packages.xml """ & cydiaUrl & """", True)
        AppConst.logger.Debug("Convert repo2xml: " & rs)
        Dim doc As New XmlDocument()
        doc.Load("Packages.xml")
        Dim nodes As XmlNodeList = doc.DocumentElement.SelectNodes("/allapp/app")

        Dim Sql As String = "INSERT INTO list_app(cydia_repos,package,version,section,depends,arch,filename,size,icon,description,name,author,homepage) 
                                                VALUES(@cydia_repos,@package,@version,@section,@depends,@arch,@filename,@size,@icon,@description,@name,@author,@homepage)"
        Dim Command As SQLiteCommand
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Dim total As Integer = nodes.Count
        Dim i As Integer = 0
        For Each node As XmlNode In nodes
            With Command.Parameters
                .AddWithValue("@cydia_repos", cydiaId)
                .AddWithValue("@package", node.SelectSingleNode("Package").InnerText)
                .AddWithValue("@version", node.SelectSingleNode("Version").InnerText)
                .AddWithValue("@section", node.SelectSingleNode("Section").InnerText)
                .AddWithValue("@depends", node.SelectSingleNode("Depends").InnerText)
                .AddWithValue("@arch", node.SelectSingleNode("Architecture").InnerText)
                .AddWithValue("@filename", node.SelectSingleNode("Filename").InnerText)
                .AddWithValue("@size", node.SelectSingleNode("Size").InnerText)
                .AddWithValue("@icon", node.SelectSingleNode("Icon").InnerText)
                .AddWithValue("@description", node.SelectSingleNode("Description").InnerText)
                .AddWithValue("@name", node.SelectSingleNode("Name").InnerText)
                .AddWithValue("@author", node.SelectSingleNode("Author").InnerText)
                .AddWithValue("@homepage", node.SelectSingleNode("Homepage").InnerText)
            End With
            Command.ExecuteNonQuery()
            i = i + 1
            If Not frm Is Nothing Then
                frm.Invoke(fn, "", CInt(i * 100 / total))
            End If
        Next
    End Function

    Shared Function LoadPackages(cydiaRepos As String) As Boolean
        'Download package
        Dim webClient As New WebClient
        Try
            webClient.DownloadFile(cydiaRepos + "/Packages.bz2", "Packages.bz2")
            Dim fileToDecompressAsStream As FileStream
            Dim decompressedStream As FileStream
            Dim fileToBeZipped As New FileInfo("Packages.bz2")
            fileToDecompressAsStream = fileToBeZipped.OpenRead()
            Using (fileToDecompressAsStream)
                decompressedStream = File.Create("Packages")
                Using (decompressedStream)
                    BZip2.BZip2.Decompress(fileToDecompressAsStream, decompressedStream, True)
                End Using
            End Using
        Catch
            Try
                webClient.DownloadFile(cydiaRepos + "/Packages", "Packages")
            Catch
                MsgBox("Invalid cydia")
                Return False
            End Try
        End Try
        Return True
    End Function

    Private Sub lstCydia_Button2Click(sender As Object, e As FileEventArgs) Handles lstCydia.Button2Click
        If lstCydia.SelectedItems.Count > 0 Then
            Dim Sql As String = "select * from cydia_repos WHERE path='" + DirectCast(lstCydia.SelectedItems.Item(0).Tag, ExtraData).MinorText + "'"
            Dim Command As SQLiteCommand
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Dim reader As SQLiteDataReader = Command.ExecuteReader()
            Dim cydiaId As Long = -1
            While reader.Read()
                cydiaId = reader.Item("id")
                Exit While
            End While

            Sql = "DELETE FROM cydia_repos WHERE id=" & cydiaId
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Command.ExecuteNonQuery()

            Sql = "DELETE FROM list_app WHERE cydia_repos=" & cydiaId
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Command.ExecuteNonQuery()

            LoadCydiaRepos()
        Else
            MsgBox("No repos to remove")
        End If
    End Sub

    Private Sub panelDevice_Paint(sender As Object, e As PaintEventArgs) Handles panelDevice.Paint

    End Sub

    Private Sub picLogo_Click(sender As Object, e As EventArgs) Handles picLogo.Click

    End Sub

    Public crrAppsOnDevice As List(Of AppInfos) = New List(Of AppInfos)

    Private Async Function getAppsOnDevice(udid As String, iOSVersion As Integer) As Task
        Dim sOutput As String = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceinstaller.exe", "-l")
        Dim line As String() = sOutput.Split(vbLf)
        Dim id As String = "si"
        crrAppsOnDevice.Clear()
        For i = 0 To line.Length - 1
            If line(i).StartsWith(id) Then
                Dim p As Integer = line(i).IndexOf("-")
                If iOSVersion <= 1 Then
                    If p > 0 Then
                        Dim appDetail As AppInfos = New AppInfos
                        appDetail.Version = line(i).Split(" ").Last
                        appDetail.Name = Trim(line(i).Substring(p + 1, line(i).Length - appDetail.Version.Length - p - 1).Replace(vbCr, "").Replace(vbLf, ""))
                        appDetail.Version = Trim(appDetail.Version.Replace(vbCr, "").Replace(vbLf, ""))
                        appDetail.Package = Trim(line(i).Substring(0, p - 1).Replace(vbCr, "").Replace(vbLf, "").Replace(udid & ".", ""))
                        crrAppsOnDevice.Add(appDetail)
                    End If
                Else
                    Dim tmp As String() = line(i).Replace(vbCr, "").Split(",")
                    If tmp.Length = 3 Then
                        Dim appDetail As AppInfos = New AppInfos
                        appDetail.Name = Trim(tmp(2).Replace("""", ""))
                        appDetail.Version = Trim(tmp(1).Replace("""", ""))
                        appDetail.Package = Trim(tmp(0).Replace(udid & ".", ""))
                        crrAppsOnDevice.Add(appDetail)
                    End If
                End If
            End If
        Next
    End Function

    Private Class ProvisionInfo
        Public eDate As DateTime
        Public id As String
    End Class

    Private Async Sub cmbDevice_TextChanged(sender As Object, e As EventArgs) Handles cmbDevice.TextChanged
        If cmbDevice.Text = "" Then
            picDevice.Visible = False
            lstAppOnDevice.Clear()
            cmdResignAll.Visible = False
            cmdResignExpired.Visible = False
            cmdFixCrash.Visible = False
            cmdInstallFromFile.Visible = False
            lblPhone.Text = ""
            lblDeviceName.Text = ""
            lblDeviceClass.Text = ""
            lblSerialNumber.Text = ""
            lblModelNumber.Text = ""
            lblProductionVersion.Text = ""
        Else
            lstAppOnDevice.Clear()
            cmdResignAll.Visible = True
            cmdResignExpired.Visible = True
            cmdFixCrash.Visible = True
            cmdInstallFromFile.Visible = True
            For i = 0 To listDevice.Count - 1
                If cmbDevice.Text = listDevice.Item(i).deviceName + " - " + listDevice.Item(i).udid Then
                    lblPhone.Text = "PhoneNumber: " & listDevice.Item(i).phoneNumber
                    lblDeviceName.Text = "DeviceName: " & listDevice.Item(i).deviceName
                    lblDeviceClass.Text = "DeviceClass: " & listDevice.Item(i).deviceClass
                    lblSerialNumber.Text = "SerialNumber: " & listDevice.Item(i).serialNumber
                    lblModelNumber.Text = "ModelNumber: " & listDevice.Item(i).modelNumber
                    lblProductionVersion.Text = "ProductVersion: " & listDevice.Item(i).productVersion
                    If listDevice.Item(i).deviceClass = "iPhone" Then
                        picDevice.Image = My.Resources.ResourceManager.GetObject("iphone")
                    Else
                        picDevice.Image = My.Resources.ResourceManager.GetObject("ipad")
                    End If

                    'get list app
                    crrAppsOnDevice.Clear()
                    Await getAppsOnDevice(cmbDevice.Text.Substring(cmbDevice.Text.Length - 40), Int32.Parse(listDevice.Item(i).productVersion.Split(".")(0)))
                    Dim provisonOnDevice As String
                    Try
                        provisonOnDevice = AppConst.m_rootPath + AppConst.CERTSTORE + "\pr\" + listDevice.Item(i).udid
                    Catch
                        Continue For
                    End Try
                    Try
                        Directory.CreateDirectory(provisonOnDevice)
                    Catch
                    End Try

                    Dim output As String = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "copy """ & provisonOnDevice & """", True)
                    Dim files As String() = Directory.GetFiles(provisonOnDevice)

                    For p = 0 To UBound(files)
                        If Path.GetExtension(files(p)).ToLower() = ".mobileprovision" Then
                            If Not File.Exists(files(p)) Then
                                Continue For
                            End If
                            Dim lines As String() = File.ReadAllLines(files(p))
                            Dim n As Integer
                            Dim deleteFile As Boolean = False
                            Dim oDate As DateTime
                            Dim pk As String = ""
                            For n = LBound(lines) To UBound(lines)
                                If (lines(n).IndexOf("<key>ExpirationDate</key>") >= 0) Then
                                    Dim ex As String = lines(n + 1).Replace("<date>", "").Replace("</date>", "").Replace("T", " ").Replace("Z", "").Trim
                                    oDate = DateTime.ParseExact(ex, "yyyy-MM-dd HH:mm:ss", Nothing)
                                ElseIf (lines(n).IndexOf("<key>application-identifier</key>") >= 0) Then
                                    Dim ex As String = lines(n + 1).Replace("<string>", "").Replace("</string>", "").Trim
                                    Dim r As Integer = ex.IndexOf(".")
                                    If (r > 0) Then
                                        pk = ex.Substring(r + 1)
                                    End If
                                End If
                            Next n
                            If (pk <> "") Then
                                Dim tmp As New ProvisionInfo
                                tmp.eDate = oDate
                                tmp.id = files(p)
                                If lstProvision.ContainsKey(pk) Then
                                    If oDate > lstProvision.Item(pk).eDate Then
                                        Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "remove " & Path.GetFileName(lstProvision.Item(pk).id).ToLower().Replace(".mobileprovision", ""))
                                        Try
                                            File.Delete(lstProvision.Item(pk).id)
                                        Catch ex As Exception

                                        End Try

                                        lstProvision.Remove(pk)
                                        lstProvision.Add(pk, tmp)
                                    Else
                                        If oDate < lstProvision.Item(pk).eDate Then
                                            Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "remove " & Path.GetFileName(tmp.id).ToLower().Replace(".mobileprovision", ""))
                                            Try
                                                File.Delete(files(p))
                                            Catch ex As Exception

                                            End Try
                                        End If
                                    End If
                                Else
                                    lstProvision.Add(pk, tmp)
                                End If
                            End If
                            Application.DoEvents()
                        End If
                    Next p

                    lstAppOnDevice.Clear()
                    lstAppOnDevice.View = View.Details
                    lstAppOnDevice.Columns.Add("App Installed by SuperImpact", lstAppOnDevice.Width - 5)
                    For n = 0 To crrAppsOnDevice.Count - 1
                        Dim lvwExtra As New ExtraData
                        If Not lstProvision.ContainsKey(crrAppsOnDevice(n).Package) Then
                            lvwExtra.HeaderColor = Color.Red
                            lvwExtra.MinorText = "Version: " & crrAppsOnDevice(n).Version & " - Expire On: ? "
                        Else
                            lvwExtra.MinorText = "Version: " & crrAppsOnDevice(n).Version & " - Expire On: " & lstProvision.Item(crrAppsOnDevice(n).Package).eDate
                            If lstProvision.Item(crrAppsOnDevice(n).Package).eDate <= Now.Date Then
                                lvwExtra.HeaderColor = Color.Red
                            End If
                        End If
                        lvwExtra.HeaderText = crrAppsOnDevice(n).Name
                        lvwExtra.DescText = crrAppsOnDevice(n).Package
                        lvwExtra.MainImage = New Bitmap(DirectCast(My.Resources.ResourceManager.GetObject("appid"), Bitmap))
                        lvwExtra.ButtonText1 = "RESIGN"
                        lvwExtra.ButtonText2 = "REMOVE"
                        Dim lvw As ListViewItem = lstAppOnDevice.Items.Add("")
                        lvw.Tag = lvwExtra
                    Next

                    Exit For
                End If
            Next i
            picDevice.Visible = True
            picDevice.BringToFront()
        End If
    End Sub

    Private Sub lstAppOnDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAppOnDevice.SelectedIndexChanged

    End Sub

    Private Sub lstAppOnDevice_Button1Click(sender As Object, e As FileEventArgs) Handles lstAppOnDevice.Button1Click
        Dim frm As New Install
        Dim ipaFile As String
        Dim package As String
        Dim cloneID As String
        package = ""
        package = e.data.DescText.Substring(3 + 33) 'si.md5.
        If (package.StartsWith("clone")) Then
            cloneID = package.Substring(5, 7)
        Else
            cloneID = ""
        End If

        Database.getInstalledApp(package, cmbDevice.Text.Substring(cmbDevice.Text.Length - 40), ipaFile)
        If File.Exists(AppConst.m_rootPath + AppConst.DOWNLOAD + ipaFile) Then
            frm.installFromFile(AppConst.m_rootPath + AppConst.DOWNLOAD + ipaFile, cloneID, e.data.HeaderText)
        Else
            MsgBox("IPA file have been remove from Downloads. Please download this app again then install",, "Error")
        End If
    End Sub

    Private Async Sub lstAppOnDevice_Button2Click(sender As Object, e As FileEventArgs) Handles lstAppOnDevice.Button2Click
        Dim output As String = Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceinstaller.exe", "-u " & cmbDevice.Text.Substring(cmbDevice.Text.Length - 40) & " -U " & e.data.DescText)
        cmbDevice.Text = ""
    End Sub

    Private Sub resign(expire As Boolean)
        Dim lstAppInfoResign As List(Of AppInfosResign) = New List(Of AppInfosResign)
        Dim udid As String = cmbDevice.Text.Substring(cmbDevice.Text.Length - 40)
        For i = 0 To crrAppsOnDevice.Count - 1
            Dim pro As Boolean = False
            If Not expire Then
                pro = True
            ElseIf lstProvision.ContainsKey(crrAppsOnDevice(i).Package) Then
                pro = lstProvision.Item(crrAppsOnDevice(i).Package).eDate <= Now.Date
            Else
                pro = True
            End If
            If pro Then
                If crrAppsOnDevice.Item(i).Package.Length > 40 Then
                    Dim app As New AppInfosResign
                    Dim ipaFile As String
                    app.Architecture = crrAppsOnDevice.Item(i).Architecture
                    app.Author = crrAppsOnDevice.Item(i).Author
                    app.Depends = crrAppsOnDevice.Item(i).Depends
                    app.Description = crrAppsOnDevice.Item(i).Description
                    app.Filename = crrAppsOnDevice.Item(i).Filename
                    app.Homepage = crrAppsOnDevice.Item(i).Homepage
                    app.Icon = crrAppsOnDevice.Item(i).Icon
                    app.Name = crrAppsOnDevice.Item(i).Name
                    app.Package = crrAppsOnDevice.Item(i).Package.Substring(3 + 33) 'si.md5.
                    If (app.Package.StartsWith("clone")) Then
                        app.cloneId = app.Package.Substring(5, 7)
                        app.Package = app.Package.Substring(13)
                    Else
                        app.cloneId = ""
                    End If
                    app.Section = crrAppsOnDevice.Item(i).Section
                    app.Size = crrAppsOnDevice.Item(i).Size
                    app.Version = crrAppsOnDevice.Item(i).Version

                    app.appleId = Database.getInstalledApp(app.Package, udid, ipaFile)
                    If app.appleId = "" Then
                        app.password = ""
                    Else
                        app.Filename = ipaFile
                        If lstAcc.ContainsKey(app.appleId) Then
                            app.password = lstAcc.Item(app.appleId)
                        End If
                    End If
                    lstAppInfoResign.Add(app)
                End If
            End If
        Next i
        If Application.OpenForms().OfType(Of AutoResign).Any Then
            MessageBox.Show("Other Resign process is already running. Please wait until current Resign process finished!")
        Else
            Dim frm As New AutoResign
            frm.ResignAsync(lstAppInfoResign, udid, cmbDevice.Text.Substring(0, cmbDevice.Text.Length - 40), "SuperImpactor", Common.GetHash(udid))
        End If
    End Sub

    Private Sub cmdResignAll_Click(sender As Object, e As EventArgs) Handles cmdResignAll.Click
        resign(False)
    End Sub

    Private Sub cmdResignExpired_Click(sender As Object, e As EventArgs) Handles cmdResignExpired.Click
        resign(True)
    End Sub

    Private Sub frmSuperImpactor_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        isExit = True
        Try
            File.Delete(AppConst.m_rootPath + "/" + AppConst.LOCALTMP + "/running.pid")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picDevice_Click(sender As Object, e As EventArgs) Handles picDevice.Click

    End Sub

    Private Sub homeBrower_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles homeBrower.Navigating
        If e.Url.ToString.EndsWith(".ipa") Then
            e.Cancel = True
            Dim ipaName = e.Url.ToString.Substring(e.Url.ToString.LastIndexOf("/") + 1)
            Common.Download(New DownloadProgress, e.Url.ToString, ipaName)
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub cmdInstallFromFile_Click(sender As Object, e As EventArgs) Handles cmdInstallFromFile.Click
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\\"
        openFileDialog1.Filter = "IPA Files (*.ipa)|*.ipa"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If (openFileDialog1.ShowDialog() = DialogResult.OK) Then
            Dim frm As Install = New Install()
            frm.installFromFile(openFileDialog1.FileName)
        End If
    End Sub

    Private Async Sub cmdFixCrash_Click(sender As Object, e As EventArgs) Handles cmdFixCrash.Click
        Try
            cmdFixCrash.Enabled = False
            Dim udid As String = cmbDevice.Text.Substring(cmbDevice.Text.Length - 40)
            Dim provisonOnDevice As String = AppConst.m_rootPath + AppConst.CERTSTORE + "\pr\" + udid
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
                        Await Common.RunExe(AppConst.m_rootPath + AppConst.IMOBILEDEVICE + "ideviceprovision.exe", "install """ & files(i) & """")
                    End If
                    Application.DoEvents()
                End If
            Next i
            MsgBox("Fix crash completed! Please check on your device.")
        Catch ee As Exception
            MsgBox("Fix crash fail: " & ee.Message)
        End Try
        cmdFixCrash.Enabled = True
    End Sub

    Private Sub cmdAbout_Click(sender As Object, e As EventArgs) Handles cmdAbout.Click
        MsgBox("v" & Assembly.GetExecutingAssembly().GetName().Version.ToString & " - Copyright @2017 - TuanHa Jsc" & vbCrLf & "Contact: flashsupporter@gmail.com" & vbCrLf & "Website: http://superimpactor.net" & vbCrLf & vbCrLf & "This program uses: " & vbCrLf & "- libimobiledevice team: http://www.libimobiledevice.org/" & vbCrLf & "- Copyright (c) 1998-2017 The OpenSSL Project, Copyright (C) 1995-1998 Eric Young (eay@cryptsoft.com). All rights reserved." & vbCrLf & "- isign by Neil Kandalgaonkar" & vbCrLf & "- 7zip" & vbCrLf & "- zip",, "About")
    End Sub

    Private Sub cmdCheckForUpdate_Click(sender As Object, e As EventArgs) Handles cmdCheckForUpdate.Click
        sparkle.CheckForUpdatesAtUserRequest()
    End Sub

    Private Sub cmdClearText_Click(sender As Object, e As EventArgs) Handles cmdClearText.Click
        txtAppSearch.Text = ""
    End Sub

    Private Sub cmdSupport_Click(sender As Object, e As EventArgs) Handles cmdSupport.Click
        Dim frm As New ReportBug
        frm.ShowDialog()
    End Sub
End Class
