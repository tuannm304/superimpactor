Imports System.Data.SQLite
Imports System.IO
Imports System.Net
Imports ICSharpCode.SharpZipLib


Public Class CydiaRepoManager
    Private Function LoadCydiaRepos(Optional cydiaReposName As String = "")
        Dim Sql As String = "select * from cydia_repos order by id"
        Dim Command As SQLiteCommand
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Dim reader As SQLiteDataReader = Command.ExecuteReader()
        lstCydia.Clear()
        lstCydia.View = View.Details
        lstCydia.Columns.Add("Name")
        lstCydia.Columns.Add("Cydia URL")
        lstCydia.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        lstCydia.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        Dim id As Integer
        id = -1
        While reader.Read()
            Dim lvi As New ListViewItem
            lvi.Text = reader.Item("name")
            lvi.SubItems.Add(reader.Item("path"))
            lstCydia.Items.Add(lvi)
            If reader.Item("path") = cydiaReposName Then
                id = reader.Item("id")
            End If
        End While
        Return id
    End Function

    Private Sub CydiaRepoManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCydiaRepos()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim cydiaRepos As String
        cydiaRepos = InputBox("Enter URL of Cydia Repos", "Add Cydia Repos")
        If Trim(cydiaRepos) = "" Then
            MsgBox("Incorrect URL", vbOKOnly, "Error")
            Exit Sub
        End If
        If Not cydiaRepos.StartsWith("http") Then
            cydiaRepos = "http://" + cydiaRepos
        End If
        '1. check if exist
        For i = 0 To lstCydia.Items.Count - 1
            Dim lvi As ListViewItem = lstCydia.Items(i)
            If lvi.SubItems.Item(1).Text = cydiaRepos Then
                MsgBox("Cydia existed!", MsgBoxStyle.ApplicationModal, "Error")
                Exit Sub
            End If
        Next

        File.Delete("Release.txt")
        Dim reposName As String = "Noname"
        Try
            Dim webClient As New WebClient
            webClient.DownloadFile(cydiaRepos + "/Release", "Release.txt")
            Dim releaseStr() As String = File.ReadAllLines("Release.txt")
            For i = LBound(releaseStr) To UBound(releaseStr)
                If releaseStr(i).StartsWith("Label:") Then
                    reposName = releaseStr(i).Substring(6)
                    Exit For
                End If
            Next
        Catch
        End Try
        Dim Sql As String = "INSERT INTO cydia_repos(name, path) VALUES(@name, @path)"
        Dim Command As SQLiteCommand
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@name", reposName)
            .AddWithValue("@path", cydiaRepos)
        End With
        Command.ExecuteNonQuery()
        Dim cydiaId = LoadCydiaRepos(cydiaRepos)

        LoadPackages(cydiaRepos)
        If File.Exists("Packages") Then
            ParsePackages(cydiaId)
        End If

    End Sub

    Shared Sub ParsePackages(cydiaId As Integer, Optional frm As UpdateCydia = Nothing, Optional fn As Object = Nothing, Optional cydiaUrl As String = "")
        Dim infoStr As String = File.ReadAllText("Packages")
        Dim appInfo As String() = infoStr.Split(New String() {"Package:"}, StringSplitOptions.RemoveEmptyEntries)
        Dim lineA As String
        Dim total As Integer
        Dim Sql As String = "INSERT INTO list_app(cydia_repos,package,version,section,depends,arch,filename,size,icon,description,name,author,homepage) 
                                                VALUES(@cydia_repos,@package,@version,@section,@depends,@arch,@filename,@size,@icon,@description,@name,@author,@homepage)"
        Dim Command As SQLiteCommand
        Dim trans As SQLiteTransaction = AppConst.m_dbConnection.BeginTransaction()
        For i = LBound(appInfo) To UBound(appInfo)
            Dim lineInfo As String() = appInfo(i).Split(vbLf)
            Dim objApp As AppInfos = New AppInfos()
            objApp.Package = Trim(lineInfo(0))
            total = UBound(lineInfo)
            For l = 1 To total
                lineA = lineInfo(l)
                If (lineA.StartsWith("Section:")) Then
                    objApp.Section = Trim(lineInfo(l).Substring(8))
                ElseIf (lineA.StartsWith("Version:")) Then
                    objApp.Version = Trim(lineInfo(l).Substring(8))
                ElseIf (lineA.StartsWith("Depends:")) Then
                    objApp.Depends = Trim(lineInfo(l).Substring(8))
                ElseIf (lineA.StartsWith("Architecture:")) Then
                    objApp.Architecture = Trim(lineInfo(l).Substring(13))
                ElseIf (lineA.StartsWith("Filename:")) Then
                    objApp.Filename = Trim(lineInfo(l).Substring(9))
                    If Not objApp.Filename.StartsWith("http") Then
                        objApp.Filename = cydiaUrl & "/" & objApp.Filename
                    End If
                ElseIf (lineA.StartsWith("Size:")) Then
                    objApp.Size = Trim(lineInfo(l).Substring(5))
                ElseIf (lineA.StartsWith("Icon:")) Then
                    objApp.Icon = Trim(lineInfo(l).Substring(5))
                ElseIf (lineA.StartsWith("Description:")) Then
                    objApp.Description = Trim(lineInfo(l).Substring(12))
                ElseIf (lineA.StartsWith("Name:")) Then
                    objApp.Name = Trim(lineInfo(l).Substring(5))
                ElseIf (lineA.StartsWith("Author:")) Then
                    objApp.Author = Trim(lineInfo(l).Substring(7))
                ElseIf (lineA.StartsWith("Homepage:")) Then
                    objApp.Homepage = Trim(lineInfo(l).Substring(9))
                End If
            Next
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            With Command.Parameters
                .AddWithValue("@cydia_repos", cydiaId)
                .AddWithValue("@package", objApp.Package)
                .AddWithValue("@version", objApp.Version)
                .AddWithValue("@section", objApp.Section)
                .AddWithValue("@depends", objApp.Depends)
                .AddWithValue("@arch", objApp.Architecture)
                .AddWithValue("@filename", objApp.Filename)
                .AddWithValue("@size", objApp.Size)
                .AddWithValue("@icon", objApp.Icon)
                .AddWithValue("@description", objApp.Description)
                .AddWithValue("@name", objApp.Name)
                .AddWithValue("@author", objApp.Author)
                .AddWithValue("@homepage", objApp.Homepage)
            End With
            Command.ExecuteNonQuery()
            If i Mod 10 = 0 Then
                If Not frm Is Nothing Then
                    frm.Invoke(fn, "", CInt(i * 100 / UBound(appInfo)))
                End If
            End If
        Next
        trans.Commit()
    End Sub

    Shared Sub LoadPackages(cydiaRepos As String)
        'Download package
        Dim webClient As New WebClient
        Try
            webClient.DownloadFile(cydiaRepos + "/Packages.bz2", "Packages.bz2")
            Dim fileToDecompressAsStream As FileStream
            Dim decompressedStream As FileStream
            Dim fileToBeZipped As New FileInfo("Packages.bz2")
            fileToDecompressAsStream = fileToBeZipped.OpenRead()
            Using (fileToDecompressAsStream)
                decompressedStream = File.Create("Packages")
                Using (decompressedStream)
                    BZip2.BZip2.Decompress(fileToDecompressAsStream, decompressedStream, True)
                End Using
            End Using
        Catch
            Try
                webClient.DownloadFile(cydiaRepos + "/Packages", "Packages")
            Catch
                MsgBox("Invalid cydia")
            End Try
        End Try
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        If lstCydia.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lstCydia.SelectedItems(0)

            Dim Sql As String = "select * from cydia_repos WHERE path='" + lvi.SubItems.Item(1).Text + "'"
            Dim Command As SQLiteCommand
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Dim reader As SQLiteDataReader = Command.ExecuteReader()
            Dim cydiaId As Long = -1
            While reader.Read()
                cydiaId = reader.Item("id")
                Exit While
            End While

            Sql = "DELETE FROM cydia_repos WHERE id=" & cydiaId
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Command.ExecuteNonQuery()

            Sql = "DELETE FROM list_app WHERE cydia_repos=" & cydiaId
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            Command.ExecuteNonQuery()

            LoadCydiaRepos()
        End If
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class