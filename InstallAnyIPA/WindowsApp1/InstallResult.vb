Public Class InstallResult
    Public Sub showMessage(rs As String, detail As String)
        lblMessage.Text = rs
        txtDetail.Text = detail
        Me.ShowDialog()
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdDetail_Click(sender As Object, e As EventArgs) Handles cmdDetail.Click
        Me.Height = txtDetail.Height + txtDetail.Top + 50
        cmdDetail.Visible = False
    End Sub

    Private Sub cmdSupport_Click(sender As Object, e As EventArgs) Handles cmdSupport.Click
        Dim frm As New ReportBug
        frm.ShowDialog()
    End Sub
End Class