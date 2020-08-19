Imports System.Collections.Specialized
Imports System.IO
Imports System.Threading
Imports System.Web.HttpUtility

Public Class URLInstall
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub optDailyUpload_Click(sender As Object, e As EventArgs) Handles optDailyUpload.Click
        txtDailyUpload.Enabled = True
        txtFileUp.Enabled = False
        txtOpenload.Enabled = False
        txtSendspace.Enabled = False
        txtDirect.Enabled = False
    End Sub

    Private Sub optFileUp_Click(sender As Object, e As EventArgs) Handles optFileUp.Click
        txtDailyUpload.Enabled = False
        txtFileUp.Enabled = True
        txtOpenload.Enabled = False
        txtSendspace.Enabled = False
        txtDirect.Enabled = False
    End Sub

    Private Sub optOpenload_Click(sender As Object, e As EventArgs) Handles optOpenload.Click
        txtDailyUpload.Enabled = False
        txtFileUp.Enabled = False
        txtOpenload.Enabled = True
        txtSendspace.Enabled = False
        txtDirect.Enabled = False
    End Sub

    Private Sub optSendSpace_Click(sender As Object, e As EventArgs) Handles optSendSpace.Click
        txtDailyUpload.Enabled = False
        txtFileUp.Enabled = False
        txtOpenload.Enabled = False
        txtSendspace.Enabled = True
        txtDirect.Enabled = False
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        Dim ipaLink As String
        Dim ipaName As String
        Dim rs As Integer
        If optDailyUpload.Checked = True Then
            rs = DailyUpload(txtDailyUpload.Text, ipaLink, ipaName)
        Else
            ipaLink = txtDirect.Text
            ipaName = Path.GetFileNameWithoutExtension(ipaLink) & ".ipa"
        End If
        If (rs < 0) Then
            MsgBox("Cannot download file! Is your link correct? " + rs.ToString)
            Return
        End If
        Common.Download(New DownloadProgress, ipaLink, ipaName)
        Me.Close()
    End Sub

    Public Shared Function DailyUpload(url As String, ByRef ipaLink As String, ByRef ipaName As String) As Integer
        Dim webConsole As New WebConsole
        Dim rs As String
        Dim name As String
        Dim homepage As String = webConsole.http(url)
        Dim pos As Integer = homepage.IndexOf("name=""fname""")
        If pos > 0 Then
            rs = webConsole.getKeyValue(homepage, "value", name, pos + 1, True)
            If rs <> "" Then
                Return -2
            End If
            pos = homepage.IndexOf("name=""id""")
            If pos = -1 Then
                Return -1
            End If
            Dim idid As String
            rs = webConsole.getKeyValue(homepage, "value", idid, pos + 1, True)
            If rs <> "" Then
                Return -2
            End If

            homepage = webConsole.http(url, "op=download1&usr_login=&id=" + idid + "&fname=" + UrlEncode(name) + "&referer=&method_free=Free+Download")
        End If
        pos = homepage.IndexOf("name=""id""")
        If pos = -1 Then
            Return -1
        End If

        Dim id As String
        rs = webConsole.getKeyValue(homepage, "value", id, pos + 1, True)
        If rs <> "" Then
            Return -2
        End If
        pos = homepage.IndexOf("name=""rand""")
        If pos = -1 Then
            Return -3
        End If
        Dim rand As String
        rs = webConsole.getKeyValue(homepage, "value", rand, pos + 1, True)
        If rs <> "" Then
            Return -4
        End If
        Thread.Sleep(5000)
        rs = webConsole.http(url, "op=download2&id=" + id + "&rand=" + rand + "&referer=&method_free=&method_premium=&down_script=1&fs_download_file=")
        pos = rs.IndexOf("This direct link will be available for your IP")
        If pos = -1 Then
            File.WriteAllText("dl.html", rs)
            Return -5
        End If
        rs = webConsole.getKeyValue(rs, "href", ipaLink, pos + 1, True)
        If rs <> "" Then
            Return -7
        End If
        ipaName = Path.GetFileName(ipaLink)
        Return 0
    End Function

    Private Sub optDirect_CheckedChanged(sender As Object, e As EventArgs) Handles optDirect.CheckedChanged
        txtDailyUpload.Enabled = False
        txtFileUp.Enabled = False
        txtOpenload.Enabled = False
        txtSendspace.Enabled = False
        txtDirect.Enabled = True
    End Sub
End Class