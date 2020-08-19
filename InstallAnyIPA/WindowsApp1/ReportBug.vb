Imports System.Net.Http
Imports System.Web
Imports System.Web.HttpUtility
Imports System.IO
Imports System.Reflection

Public Class ReportBug
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Async Sub cmdSend_Click(sender As Object, e As EventArgs) Handles cmdSend.Click
        cmdSend.Enabled = False
        picLoading.Visible = True
        lblLoading.Visible = True
        Dim rs = Await sendLog(txtEmail.Text, txtMessage.Text)
        If rs = "" Then
            MsgBox("Send successfully. We will contact you after processing your request")
            Me.Close()
        Else
            cmdSend.Enabled = True
            picLoading.Visible = False
            lblLoading.Visible = False
            MsgBox(rs)
        End If
    End Sub

    Shared Async Function sendLog(email As String, message As String) As Task(Of String)
        Dim log As String
        Dim logFile As String
        If File.Exists(AppConst.m_rootPath & "/Log/SuperImpactor.log") Then
            logFile = AppConst.m_rootPath & "/Log/SuperImpactor.log"
        Else
            logFile = AppConst.m_rootPath & "/bin/Debug/Log/SuperImpactor.log"
        End If

        Dim logFileStream = New FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim logFileReader = New StreamReader(logFileStream)
        log = ""
        While Not logFileReader.EndOfStream
            log = log & logFileReader.ReadLine() & vbCrLf
        End While
        logFileReader.Close()
        logFileStream.Close()
        If log.Length > 10000 Then
            log = log.Substring(log.Length - 10000)
        End If

        message = System.Environment.OSVersion.VersionString & vbCrLf & "v" & Assembly.GetExecutingAssembly().GetName().Version.ToString & vbCrLf & message & vbCrLf
        Dim httpClient As New HttpClient
        Dim pairs = New Dictionary(Of String, String)
        pairs.Add("email", email)
        pairs.Add("message", message)
        pairs.Add("log", log)
        Dim formContent = New FormUrlEncodedContent(pairs)

        Dim response As HttpResponseMessage = Await httpClient.PostAsync(AppConst.API_SENDLOG, formContent)
        response.EnsureSuccessStatusCode()
        Dim content = Await response.Content.ReadAsStringAsync()
        Return ""
    End Function
End Class