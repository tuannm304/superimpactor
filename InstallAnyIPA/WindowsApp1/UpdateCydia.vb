Imports System.Data.SQLite
Imports System.IO
Imports System.Net
Imports System.Threading

Public Class UpdateCydia
    Delegate Sub ChangeTextsSafe(ByVal cydia As String, ByVal percent As Long)
    Delegate Sub CompleteSafe(ByVal cancelled As Boolean)
    Private bComplete As Boolean = False
    Private bAsync As Boolean = False
    Private CrrLoadCydiaId As Integer = -1

    Private Sub Complete(ByVal cancelled As Boolean)
        If (cancelled) Then
            Me.Close()
            Exit Sub
        End If
        bComplete = True
        If CrrLoadCydiaId = -1 Then
            MsgBox("Update all Repos completed!")
        Else
            MsgBox("Add Repos completed!")
        End If
        Me.Close()
    End Sub

    Private Sub ChangeTexts(ByVal cydia As String, ByVal percent As Long)
        If cydia <> "" Then
            lblCydia.Text = "Repos: " & cydia
        End If
        ProgressBar1.Value = percent
    End Sub

    Public Function LoadApp(cydiaId As Integer) As Boolean
        CrrLoadCydiaId = cydiaId
        BackgroundWorker1.WorkerSupportsCancellation = True
        Me.BackgroundWorker1.RunWorkerAsync() 'Start download
        Me.ShowDialog()
        Return bComplete
    End Function

    Public Function UpdateApp() As Boolean
        BackgroundWorker1.WorkerSupportsCancellation = True
        Me.BackgroundWorker1.RunWorkerAsync() 'Start download
        Me.Show()
        Return bComplete
    End Function

    Public Sub UpdateAppAsync()
        bAsync = True
        BackgroundWorker1.WorkerSupportsCancellation = True
        Me.BackgroundWorker1.RunWorkerAsync() 'Start download
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Thread.Sleep(2000)
        Dim textDelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Dim cancelDelegate As New CompleteSafe(AddressOf Complete)
        Dim cydias As Dictionary(Of Integer, String) = Database.GetCydiaRepos(False)
        For Each cyd As KeyValuePair(Of Integer, String) In cydias
            If CrrLoadCydiaId <> -1 Then
                If CrrLoadCydiaId <> cyd.Key Then
                    Continue For
                End If
            End If
            If Not bAsync Then
                Me.Invoke(textDelegate, cyd.Value, 0)
            End If
            File.Delete("Packages")
            CydiaRepoManager.LoadPackages(cyd.Value)
            If File.Exists("Packages") Then
                'xoa
                Dim Sql As String = "DELETE FROM list_app WHERE cydia_repos=" & cyd.Key
                Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
                Command.ExecuteNonQuery()
                If bAsync Then
                    CydiaRepoManager.ParsePackages(cyd.Key,,, cyd.Value)
                Else
                    CydiaRepoManager.ParsePackages(cyd.Key, Me, textDelegate, cyd.Value)
                End If
            End If
            If Me.BackgroundWorker1.CancellationPending Then
                If Not bAsync Then
                    Me.Invoke(cancelDelegate, True)
                End If
                Exit Sub
            End If
        Next

        If Not bAsync Then
            Me.Invoke(cancelDelegate, False)
        End If
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.BackgroundWorker1.CancelAsync() 'Send cancel request
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub
End Class