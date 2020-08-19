
Imports System.Data.SQLite
Imports System.IO
Imports System.Net
Imports Claunia.PropertyList

Public Class Main
    Private Declare Auto Function SendMessage Lib "user32" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Const LVM_FIRST As Long = &H1000
    Public Const LVM_SETICONSPACING As Long = LVM_FIRST + 53

    Public Sub SetListViewSpacing(lst As ListView, x As Integer, y As Integer)
        SendMessage(lst.Handle, LVM_SETICONSPACING, 0, x * 65536 + y)
    End Sub

    Private Sub AppleAccountsManagerMenuItem_Click(sender As Object, e As EventArgs) Handles AppleAccountsManagerMenuItem.Click
        Dim frm As New AppleAccounts
        frm.ShowDialog()
    End Sub

    Private Sub CydiaReposManagerMenuItem_Click(sender As Object, e As EventArgs) Handles CydiaReposManagerMenuItem.Click
        Dim frmCydiaReposManager = New CydiaRepoManager()
        frmCydiaReposManager.ShowDialog()
        ReloadApps()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

    End Sub

    Delegate Sub InvokeDelegate()
    Private Sub ThreadTask()

    End Sub


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Get the logger as named in the configuration file.
            AppConst.logger = log4net.LogManager.GetLogger("SuperImpactor")
            AppConst.logger.Info("Start")
        Catch ex As Exception
        Finally
        End Try
        Try
            WebBrowser1.Url = New Uri("https://www.iphonecake.com")
            WebBrowser1.ScriptErrorsSuppressed = True
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
        Dim appPath As String = Application.StartupPath()
        AppConst.m_rootPath = appPath + "\" & "..\..\"
        AppConst.m_localTmp = Common.GetTempFolder

        If Not File.Exists(AppConst.m_rootPath + AppConst.DB) Then
            MsgBox("Application error! Please install again!", MsgBoxStyle.ApplicationModal, "Error")
            End
        End If
        AppConst.m_dbConnection = New SQLiteConnection("Data Source=" + AppConst.m_rootPath + AppConst.DB + ";Version=3;")
        AppConst.m_dbConnection.Open()
        SetListViewSpacing(lstApps, 16, 16)

        AppConst.m_lstCydiaRepos = Database.GetCydiaRepos()

        For i = 0 To 16
            AppConst.m_randomKey = AppConst.m_randomKey + Char.ConvertFromUtf32(i * 2 + 64)
        Next
        ReloadApps()
    End Sub

    Private Sub ReloadApps(Optional search As String = "")
        Try

            Dim Sql As String
            If search = "" Then
                Sql = "select * from list_app order by name"
            Else
                Sql = "select * from list_app where name like '%" + search + "%' order by name"
            End If
            Dim Command As SQLiteCommand
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Dim reader As SQLiteDataReader = Command.ExecuteReader()
            lstApps.Clear()
            lstApps.Columns.Add("Apps", Me.Width - 30)
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
                    lvwExtra.MainImage = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(lvwExtra.AppDetailObj.Icon)))
                Else
                    lvwExtra.MainImage = New Bitmap(AppConst.m_rootPath + AppConst.IMAGE + "/apptype_tweak.png")
                End If
                Dim lvw As ListViewItem = lstApps.Items.Add("")
                lvw.Tag = lvwExtra
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdHot.Click
        WebBrowser1.Visible = True
        lstApps.Visible = False
    End Sub

    Private Sub lstApps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstApps.SelectedIndexChanged

    End Sub

    Private Sub lstApps_DoubleClick(sender As Object, e As EventArgs) Handles lstApps.DoubleClick
        Dim lvw As ListViewItem = lstApps.SelectedItems(0)
        Dim frmDetail As AppDetail
        Dim extra As ExtraData = lvw.Tag
        frmDetail = New AppDetail
        frmDetail.showApp(extra.AppDetailObj)

    End Sub

    Private Sub txtAppSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAppSearch.TextChanged
        WebBrowser1.Visible = False
        lstApps.Visible = True
        ReloadApps(txtAppSearch.Text)
    End Sub

    Private Sub txtAppSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAppSearch.KeyPress
        If e.KeyChar = vbCr Then
            cmdHot.PerformClick()
        End If
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click

    End Sub

    Private Sub InstallFromURLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InstallFromURLToolStripMenuItem.Click
        Dim frm As URLInstall = New URLInstall
        frm.ShowDialog()
    End Sub

    Private Sub InstallFromReposToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InstallFromReposToolStripMenuItem.Click
        Dim frm As CydiaRepoManager = New CydiaRepoManager
        frm.ShowDialog()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Dim webAddress As String = "http://www.superimpactor.net/help"
        Process.Start(webAddress)
    End Sub

    Private Sub InstallFromFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InstallFromFileToolStripMenuItem.Click
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\\"
        openFileDialog1.Filter = "IPA Files (*.ipa)|*.ipa|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If (openFileDialog1.ShowDialog() = DialogResult.OK) Then
            Dim frm As Install = New Install()
            frm.installFromFile(openFileDialog1.FileName)
        End If
    End Sub

    Private Sub DownloadedFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadedFilesToolStripMenuItem.Click
        Dim frm As DownloadFiles = New DownloadFiles
        frm.ShowDialog()
    End Sub

    Private Sub FromDownloadedFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromDownloadedFilesToolStripMenuItem.Click
        Dim frm As DownloadFiles = New DownloadFiles
        frm.ShowDialog()
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim frm As UpdateCydia = New UpdateCydia
        If frm.UpdateApp Then
            ReloadApps()
            MsgBox("Update all cydia completed!")
        End If
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        MsgBox("Copyright @2017 - TuanHa Jsc" & vbCrLf & vbCrLf & "This program use: " & vbCrLf & "- libimobiledevice team: http://www.libimobiledevice.org/" & vbCrLf & "- Copyright (c) 1998-2017 The OpenSSL Project, Copyright (C) 1995-1998 Eric Young (eay@cryptsoft.com). All rights reserved." & vbCrLf & "- isign by Neil Kandalgaonkar",, "About")
    End Sub

    Private Sub RevokeCertificatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevokeCertificatesToolStripMenuItem.Click
        Dim frm As CertificateDelete = New CertificateDelete
        frm.ShowDialog()
    End Sub

    Private Sub DeleteAppIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAppIDToolStripMenuItem.Click
        Dim frm As AppIdDelete = New AppIdDelete
        frm.ShowDialog()
    End Sub

    Private Sub WebBrowser1_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating
        If MsgBox("You are trying to go to:" & vbCr & e.Url.ToString() & vbCr & "Cancel Navigate?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            e.Cancel = True
        End If
    End Sub
End Class
