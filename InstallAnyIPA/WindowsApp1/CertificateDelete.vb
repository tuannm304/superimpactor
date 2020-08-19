Imports System.Data.SQLite
Imports Claunia.PropertyList

Public Class CertificateDelete
    Private lstAcc As Dictionary(Of String, String)
    Private teamId As String

    Private Function LoadCertificates()
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

        Application.DoEvents()
        nodes = AppleService.allDevelopmentCert(teamId)
        lstCydia.Clear()
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Name", 500)
        lstCydia.Columns.Add("Serial")
        Dim certData As String = ""
        If (nodes.ContainsKey("certificates")) Then
            Dim lstcerts As NSArray = DirectCast(nodes.ObjectForKey("certificates"), NSArray)
            For i = 0 To lstcerts.Count - 1
                Dim cert As NSDictionary = lstcerts.ElementAt(i)
                If (cert.ContainsKey("name") And cert.ContainsKey("serialNumber")) Then
                    Dim lvi As New ListViewItem
                    If cert.ContainsKey("machineName") Then
                        lvi.Text = cert.ObjectForKey("name").ToString() & " - " & cert.ObjectForKey("machineName").ToString()
                    Else
                        lvi.Text = cert.ObjectForKey("name").ToString()
                    End If
                    lvi.SubItems.Add(cert.ObjectForKey("serialNumber").ToString())
                        lstCydia.Items.Add(lvi)
                    End If
            Next i
            If lstCydia.Items.Count = 0 Then
                Return "No Certificates"
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
        Dim rs = LoadCertificates()
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
            AppleService.revokeCertificate(lvi.SubItems.Item(1).Text, teamId)
            lvi.Remove()
        Next
    End Sub

    Private Sub cmdRemoveAll_Click(sender As Object, e As EventArgs) Handles cmdRemoveAll.Click
        For Each lvi As ListViewItem In lstCydia.Items
            AppleService.revokeCertificate(lvi.SubItems.Item(1).Text, teamId)
            lvi.Remove()
        Next
        lstCydia.Clear()
    End Sub
End Class