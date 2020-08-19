Imports System.Data.SQLite
Imports Claunia.PropertyList

Public Class AppIdDelete
    Private lstAcc As Dictionary(Of String, String)
    Private teamId As String

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
        teamId = ""
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

        lstCydia.Clear()
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Name", 234)
        lstCydia.Columns.Add("Id")


        Application.DoEvents()
        nodes = AppleService.appIds(teamId)
        Dim appIdId As String = ""
        If (nodes.ContainsKey("appIds")) Then
            Dim lstApps As NSArray = DirectCast(nodes.ObjectForKey("appIds"), NSArray)
            For i = 0 To lstApps.Count - 1
                Dim app As NSDictionary = lstApps.ElementAt(i)
                If (app.ContainsKey("appIdId") And app.ContainsKey("name")) Then
                    appIdId = app.ObjectForKey("appIdId").ToString()
                    Dim lvi As New ListViewItem
                    lvi.Text = app.ObjectForKey("name").ToString()
                    lvi.SubItems.Add(app.ObjectForKey("appIdId").ToString())
                    lstCydia.Items.Add(lvi)
                End If
            Next i
            If lstCydia.Items.Count = 0 Then
                Return "No AppId"
            End If
        End If
        Return ""
    End Function

    Private Sub CertificateDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstAcc = Database.GetAccounts
        For Each kvp As KeyValuePair(Of String, String) In lstAcc
            cmbAppleId.Items.Add(kvp.Key)
        Next
    End Sub

    Private Sub cmdGetCert_Click(sender As Object, e As EventArgs) Handles cmdGetCert.Click
        If cmbAppleId.Text.Trim = "" Then
            MsgBox("No appleId!", vbOKOnly, "Error")
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            MsgBox("No password!", vbOKOnly, "Error")
            Exit Sub
        End If
        cmdGetCert.Enabled = False
        Dim rs = LoadApps()
        cmdGetCert.Enabled = True
        If rs <> "" Then
            MsgBox(rs, vbOKOnly, "Warning")
        End If
    End Sub

    Private Sub cmbAppleId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAppleId.SelectedIndexChanged
        txtPassword.Text = lstAcc.Item(cmbAppleId.Text)
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        For Each lvi As ListViewItem In lstCydia.SelectedItems
            AppleService.deleteAppId(lvi.SubItems.Item(1).Text, teamId)
            lvi.Remove()
        Next
    End Sub

    Private Sub cmdRemoveAll_Click(sender As Object, e As EventArgs) Handles cmdRemoveAll.Click
        For Each lvi As ListViewItem In lstCydia.Items
            AppleService.deleteAppId(lvi.SubItems.Item(1).Text, teamId)
            lvi.Remove()
        Next
        lstCydia.Clear()
    End Sub
End Class