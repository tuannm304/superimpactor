Public Class AppleAccounts
    Private Sub LoadAccounts()
        Dim lstAcc As Dictionary(Of String, String) = Database.GetAccounts()
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Apple Id")
        lstCydia.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        lstCydia.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        For i = 0 To lstAcc.Keys.Count - 1
            Dim lvi As New ListViewItem
            lvi.Text = lstAcc.Keys.ElementAt(i)
            lvi.SubItems.Add(lstAcc.Keys.ElementAt(i))
            lstCydia.Items.Add(lvi)
        Next
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub AppleAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts()
    End Sub

    Private Sub cmdRemoveAll_Click(sender As Object, e As EventArgs) Handles cmdRemoveAll.Click
        Database.DeleteAccounts()
        lstCydia.Clear()
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        If lstCydia.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lstCydia.SelectedItems(0)
            Database.DeleteAccount(lvi.SubItems.Item(0).Text)
            LoadAccounts()
        Else
            MsgBox("Select account to remove")
        End If
    End Sub
End Class