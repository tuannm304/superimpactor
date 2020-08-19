Imports System.IO

Public Class DownloadFiles
    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub DownloadFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Name", lstCydia.Width - 70, HorizontalAlignment.Left)
        lstCydia.Columns.Add("Size")
        Dim files As String() = Directory.GetFiles(AppConst.m_rootPath + AppConst.DOWNLOAD)
        Dim i As Integer
        For i = 0 To UBound(files)
            Dim lvi As New ListViewItem

            Dim myFile As New FileInfo(files(i))
            Dim size As String
            lvi.Text = myFile.Name
            If myFile.Length / 1024 / 1024 / 1024 >= 1 Then
                size = Math.Round(myFile.Length / 1024 / 1024 / 1024, 2).ToString + " GB"
            ElseIf myFile.Length / 1024 / 1024 >= 1 Then
                size = Math.Round(myFile.Length / 1024 / 1024, 2).ToString + " MB"
            Else
                size = Math.Round(myFile.Length / 1024, 2).ToString + " KB"
            End If
            lvi.SubItems.Add(size)
            lstCydia.Items.Add(lvi)
        Next
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        For Each lvi As ListViewItem In lstCydia.SelectedItems
            File.Delete(AppConst.m_rootPath + AppConst.DOWNLOAD + lvi.SubItems.Item(0).Text)
            lvi.Remove()
        Next

    End Sub

    Private Sub cmdRemoveAll_Click(sender As Object, e As EventArgs) Handles cmdRemoveAll.Click
        Common.DeleteFilesFromFolder(AppConst.m_rootPath + AppConst.DOWNLOAD)
        lstCydia.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lstCydia.SelectedItems.Count = 0 Then
            MsgBox("Please select file to install")
        Else
            Dim lvi As ListViewItem
            lvi = lstCydia.SelectedItems(0)
            Dim f As New Install
            f.installFromFile(AppConst.m_rootPath + AppConst.DOWNLOAD + lvi.SubItems.Item(0).Text)
        End If
    End Sub
End Class