Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Public Class AppDetail
    Public appDetailInfo As AppInfos

    Public Sub showApp(appinfo As AppInfos)
        appDetailInfo = appinfo
        Dim webClient As New WebClient
        lblAppName.Text = appDetailInfo.Name
        lblAuthor.Text = appDetailInfo.Author
        lblCategory.Text = appDetailInfo.Section
        lblVersion.Text = appDetailInfo.Version
        Dim json As String
        Dim app As ItuneResult
        Try
            json = webClient.DownloadString("https://itunes.apple.com/lookup?id=" & appDetailInfo.Package)
            app = JsonConvert.DeserializeObject(Of ItuneResult)(json)
        Catch
            MsgBox("Cannot get app information")
            Exit Sub
        End Try
        Dim tClient As New WebClient
        If app.resultCount > 0 Then
            appDetailInfo.Size = app.results.ElementAt(0).fileSizeBytes
            appDetailInfo.Description = app.results.ElementAt(0).description.Replace(vbLf, vbCrLf)
            appDetailInfo.Architecture = app.results.ElementAt(0).minimumOsVersion
            Dim filesize As Long = CLng(appDetailInfo.Size)
            If (filesize / 1024 / 1024 / 1024) > 1 Then
                lblSize.Text = Math.Round((filesize / 1024 / 1024 / 1024), 2) & " GB"
            ElseIf (filesize / 1024 / 1024) > 1 Then
                lblSize.Text = Math.Round((filesize / 1024 / 1024), 2) & " MB"
            Else
                lblSize.Text = Math.Round((filesize / 1024), 2) & " KB"
            End If
            pic1.Image = Image.FromStream(New MemoryStream(tClient.DownloadData(app.results.ElementAt(0).screenshotUrls.ElementAt(0))))
            pic2.Image = Image.FromStream(New MemoryStream(tClient.DownloadData(app.results.ElementAt(0).screenshotUrls.ElementAt(1))))
            pic3.Image = Image.FromStream(New MemoryStream(tClient.DownloadData(app.results.ElementAt(0).screenshotUrls.ElementAt(2))))
            pic4.Image = Image.FromStream(New MemoryStream(tClient.DownloadData(app.results.ElementAt(0).screenshotUrls.ElementAt(3))))
        Else
            appDetailInfo.Size = ""
            appDetailInfo.Description = ""
        End If
        If appinfo.Icon <> "" And appinfo.Icon.StartsWith("http") Then
            imgApp.Image = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(appinfo.Icon)))
        Else
            imgApp.Image = New Bitmap(AppConst.m_rootPath + AppConst.IMAGE + "/apptype_tweak.png")
        End If
        lblSupport.Text = appDetailInfo.Architecture
        txtDesc.Text = appDetailInfo.Description

        Me.ShowDialog()
    End Sub

    Private Sub cmdInstall_Click(sender As Object, e As EventArgs) Handles cmdInstall.Click
        Dim ipaLink As String
        Dim ipaName As String
        If appDetailInfo.Filename.IndexOf("dailyuploads.net") >= 0 Then
            ipaLink = appDetailInfo.Filename
            ipaName = "checking...ipa"
        Else
            ipaLink = appDetailInfo.Filename
            ipaName = appDetailInfo.Name & ".ipa"
        End If
        Me.Close()
        Common.Download(New DownloadProgress, ipaLink, ipaName)
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class