Imports System.IO
Imports System.Net

Public Class DownloadProgress
    Delegate Sub ChangeTextsSafe(ByVal length As Long, ByVal position As Long, ByVal percent As Long, ByVal speed As Double)
    Delegate Sub ChangeStatusSafe(ByVal text As String)
    Delegate Sub DownloadCompleteSafe(ByVal cancelled As Boolean)
    Private downloadLink As String
    Private localFile As String
    Private storeFile As String
    Private bComplete As Boolean = False

    Private Sub DownloadComplete(ByVal cancelled As Boolean)
        If (cancelled) Then
            Me.Close()
            Exit Sub
        End If
        If chkStore.Checked Then
            Try
                File.Delete(AppConst.m_rootPath + AppConst.DOWNLOAD + storeFile)
            Catch
            End Try
            Try
                File.Move(localFile, AppConst.m_rootPath + AppConst.DOWNLOAD + storeFile)
                AppConst.mainForm.onDownloadComplete()
            Catch
            End Try
        End If
        bComplete = True
        Me.Close()
    End Sub

    Private Sub ChangeStatus(ByVal text As String)
        lblFile.Text = text
        Me.Text = text
    End Sub

    Private Sub ChangeTexts(ByVal length As Long, ByVal position As Long, ByVal percent As Long, ByVal speed As Double)
        If length = -1 Then
            lblFileSize.Text = "calculating..."
        Else
            If (length / 1024 / 1024) > 1 Then
                lblFileSize.Text = Math.Round((length / 1024 / 1024), 2) & " MB"
            Else
                lblFileSize.Text = Math.Round((length / 1024), 2) & " KB"
            End If
        End If

        If (position / 1024 / 1024) > 1 Then
            lblDownload.Text = Math.Round((position / 1024 / 1024), 2) & " MB"
        Else
            lblDownload.Text = Math.Round((position / 1024), 2) & " KB"
        End If


        If speed = -1 Then
            lblSpeed.Text = "calculating..."
        Else
            If (speed / 1024 / 1024) > 1 Then
                lblSpeed.Text = Math.Round((speed / 1024 / 1024), 2) & " MB/s"
            Else
                lblSpeed.Text = Math.Round((speed / 1024), 2) & " KB/s"
            End If
        End If

        Me.ProgressBar1.Value = percent
        lblPercent.BringToFront()
        lblPercent.Text = percent & "%"
        Me.Text = percent & "%" & "  " & storeFile
        lblPercent.BackColor = Color.Transparent
        lblFile.Text = storeFile
    End Sub

    Public Function Download(url As String, tmpFile As String, storedFile As String) As Boolean
        downloadLink = url
        localFile = tmpFile
        storeFile = storedFile
        ChangeTexts(-1, 0, 0, -1)
        BackgroundWorker1.WorkerSupportsCancellation = True
        BackgroundWorker1.RunWorkerAsync() 'Start download
        Me.Text = "Download:    " & storedFile
        ShowDialog()
        Return bComplete
    End Function

    Public Sub DownloadAsync(url As String, tmpFile As String, storedFile As String)
        downloadLink = url
        localFile = tmpFile
        storeFile = storedFile
        ChangeTexts(-1, 0, 0, -1)
        BackgroundWorker1.WorkerSupportsCancellation = True
        BackgroundWorker1.RunWorkerAsync() 'Start download
        Show()
        Me.Text = "Download: " & storedFile
    End Sub

    Private Async Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If downloadLink.IndexOf("dailyuploads.net") >= 0 Then
            Dim rs As Integer
            rs = URLInstall.DailyUpload(downloadLink, downloadLink, storeFile)
            If (rs < 0) Then
                MsgBox("Cannot download file! Is your link correct? " + rs.ToString)
                Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)
                Try
                    Me.Invoke(cancelDelegate, True)
                Catch
                End Try
                Exit Sub
            End If
        End If
        'Creating the request and getting the response
        Dim theResponse As HttpWebResponse
        Dim theRequest As HttpWebRequest
        Try 'Checks if the file exist

            theRequest = WebRequest.Create(downloadLink)
            theResponse = theRequest.GetResponse
        Catch ex As Exception
            MessageBox.Show("An error occurred while downloading file. Possibe causes:" & ControlChars.CrLf &
                            "1) File doesn't exist" & ControlChars.CrLf &
                            "2) Remote server error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)
            Try
                Me.Invoke(cancelDelegate, True)
            Catch
            End Try
            Exit Sub
        End Try
        Dim length As Long = theResponse.ContentLength 'Size of the response (in bytes)

        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Try
            Me.Invoke(safedelegate, length, 0, 0, 0) 'Invoke the TreadsafeDelegate
        Catch
        End Try

        Dim writeStream As New IO.FileStream(localFile, IO.FileMode.Create)

        'Replacement for Stream.Position (webResponse stream doesn't support seek)
        Dim nRead As Long

        'To calculate the download speed
        Dim speedtimer As New Stopwatch
        Dim currentspeed As Double = -1
        Dim readings As Integer = 0
        Do
            If BackgroundWorker1.CancellationPending Then 'If user abort download
                Exit Do
            End If

            speedtimer.Start()

            Dim readBytes(4095) As Byte
            Dim bytesread As Integer = theResponse.GetResponseStream.Read(readBytes, 0, 4096)
            nRead += bytesread
            Dim percent As Long = (nRead * 100) / length
            Try
                Me.Invoke(safedelegate, length, nRead, percent, currentspeed)
            Catch
                theResponse.GetResponseStream.Close()
                writeStream.Close()
                Try
                    IO.File.Delete(localFile)
                    Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)
                    Me.Invoke(cancelDelegate, True)
                Catch
                End Try
                Exit Sub
            End Try
            If bytesread = 0 Then Exit Do
            writeStream.Write(readBytes, 0, bytesread)
            speedtimer.Stop()
            readings += 1
            If readings >= 100 Then 'For increase precision, the speed it's calculated only every five cicles
                currentspeed = 409600 / (speedtimer.ElapsedMilliseconds / 1000)
                speedtimer.Reset()
                readings = 0
            End If
        Loop
        'Close the streams
        theResponse.GetResponseStream.Close()
        writeStream.Close()
        If Me.BackgroundWorker1.CancellationPending Then
            IO.File.Delete(localFile)
            Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)
            Try
                Me.Invoke(cancelDelegate, True)
            Catch
            End Try
            Exit Sub
        End If

        If (storeFile.EndsWith(".deb")) Then
            'convert to ipa
            Try
                Directory.CreateDirectory(AppConst.m_localTmp)
            Catch
            End Try
            Try
                File.Delete(AppConst.m_localTmp & "/data.tar")
            Catch
            End Try
            Try
                Common.DeleteFilesFromFolder(AppConst.m_localTmp & "/data")
            Catch
            End Try
            Dim changeStatusDelegate As New ChangeStatusSafe(AddressOf ChangeStatus)
            Try
                Me.Invoke(changeStatusDelegate, "Please wait for processing download file...")
            Catch
            End Try
            Application.DoEvents()
            Dim output As String = Await Common.RunExe(AppConst.m_rootPath + AppConst.ZIP7 + "7z.exe", "e """ & localFile & """ -o""" & AppConst.m_localTmp & """", True)
            output = Await Common.RunExe(AppConst.m_rootPath + AppConst.ZIP7 + "7z.exe", "x """ & AppConst.m_localTmp & "\data.tar""" & " -o""" & AppConst.m_localTmp & "\data\"" -r -aoa", True)
            Directory.Move(AppConst.m_localTmp & "\data\Applications", AppConst.m_localTmp & "\data\Payload")
            output = Await Common.RunExe(AppConst.m_rootPath + AppConst.ZIP7 + "7z.exe", "a -tzip """ & localFile & ".zip"" """ & AppConst.m_localTmp & "\data\*""")
            storeFile = storeFile & ".ipa"
            Try
                File.Delete(localFile)
            Catch ee As Exception
            End Try
            Try
                File.Move(localFile & ".zip", localFile)
            Catch ee As Exception
                MsgBox(ee.Message)
            End Try
        End If

        Dim completeDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)
        Try
            Me.Invoke(completeDelegate, False)
        Catch
        End Try





    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs)
        Me.BackgroundWorker1.CancelAsync() 'Send cancel request
    End Sub

    Private Sub DownloadProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BackgroundWorker1.WorkerSupportsCancellation = True
    End Sub

    Private Sub chkStore_CheckedChanged(sender As Object, e As EventArgs) Handles chkStore.CheckedChanged

    End Sub

    Private Sub DownloadProgress_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        BackgroundWorker1.CancelAsync() 'Send cancel request
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub
End Class