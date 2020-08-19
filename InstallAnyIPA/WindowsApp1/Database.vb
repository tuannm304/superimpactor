Imports System.Data.SQLite

Public Class Database
    Public Shared Function GetCydiaRepos(Optional getName As Boolean = True) As Dictionary(Of Integer, String)
        Dim Sql As String = "select * from cydia_repos order by id"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Dim readerCydia As SQLiteDataReader = Command.ExecuteReader()
        Dim rs As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
        While readerCydia.Read()
            If getName Then
                rs.Add(readerCydia.Item("id"), readerCydia.Item("name"))
            Else
                rs.Add(readerCydia.Item("id"), readerCydia.Item("path"))
            End If
        End While
        Return rs
    End Function

    Public Shared Sub SaveAccount(appId As String, pass As String)
        Dim encryptPass As String
        encryptPass = Common.AES_Encrypt(pass, AppConst.m_randomKey + appId)
        Dim Sql As String = "select * from account where appleId=@appleId"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@appleId", appId)
        End With
        AppConst.logger.Debug("SaveAccount: 1. " + Sql + ", " + appId)

        Dim reader As SQLiteDataReader = Command.ExecuteReader()
        'Dim rs As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

        While reader.Read()
            If reader.Item("password") = encryptPass Then
                Return
            End If
            'exist
            Sql = "UPDATE account SET appleId=@appleId, password=@password"
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            With Command.Parameters
                .AddWithValue("@appleId", appId)
                .AddWithValue("@password", encryptPass)
            End With
            AppConst.logger.Debug("SaveAccount: 2. " + Sql + ", " + appId)
            Command.ExecuteNonQuery()
            Return
        End While
        Sql = "INSERT INTO account(appleId, password) VALUES(@appleId, @password)"
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@appleId", appId)
            .AddWithValue("@password", encryptPass)
        End With
        AppConst.logger.Debug("SaveAccount: 3. " + Sql + ", " + appId)
        Command.ExecuteNonQuery()
    End Sub

    Public Shared Function GetAccounts() As Dictionary(Of String, String)
        Dim tmp As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim Sql As String = "select * from account"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Dim readerCydia As SQLiteDataReader = Command.ExecuteReader()
        While readerCydia.Read()
            tmp.Add(readerCydia.Item("appleId"), Common.AES_Decrypt(readerCydia.Item("password"), AppConst.m_randomKey + readerCydia.Item("appleId")))
        End While
        Return tmp
    End Function

    Public Shared Sub DeleteAccount(appId As String)
        Dim Sql As String = "DELETE From account where appleId=@appleId"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@appleId", appId)
        End With
        Command.ExecuteNonQuery()
    End Sub

    Public Shared Sub DeleteAccounts()
        Dim Sql As String = "DELETE From account"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        Command.ExecuteNonQuery()
    End Sub

    Public Shared Sub updateInstalledApp(package As String, appleId As String, fileIpa As String, udid As String)
        Dim Sql As String = "select * from list_installed_app where installed_appleid_email=@appleId AND package=@package AND udid=@udid"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@appleId", appleId)
            .AddWithValue("@package", package)
            .AddWithValue("@udid", udid)
        End With
        Dim reader As SQLiteDataReader = Command.ExecuteReader()
        'Dim rs As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

        While reader.Read()
            'exist
            Sql = "UPDATE list_installed_app SET file_ipa=@fileIpa, installed_time=@timenow WHERE installed_appleid_email=@appleId AND package=@package AND udid=@udid"
            Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
            With Command.Parameters
                .AddWithValue("@fileIpa", fileIpa)
                .AddWithValue("@timenow", (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)
                .AddWithValue("@appleId", appleId)
                .AddWithValue("@package", package)
                .AddWithValue("@udid", udid)
            End With
            Command.ExecuteNonQuery()
            Return
        End While
        Sql = "INSERT INTO list_installed_app(installed_appleid_email, package, file_ipa, installed_time, udid) VALUES(@appleId, @package, @fileIpa, @timenow, @udid)"
        Command = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@appleId", appleId)
            .AddWithValue("@package", package)
            .AddWithValue("@fileIpa", fileIpa)
            .AddWithValue("@udid", udid)
            .AddWithValue("@timenow", (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)
        End With
        Command.ExecuteNonQuery()
    End Sub

    Public Shared Function getInstalledApp(package As String, udid As String, ByRef fileIpa As String) As String
        Dim Sql As String = "select * from list_installed_app where package=@package"
        Dim Command As SQLiteCommand = New SQLiteCommand(Sql, AppConst.m_dbConnection)
        With Command.Parameters
            .AddWithValue("@package", package)
        End With
        Dim reader As SQLiteDataReader = Command.ExecuteReader()

        Dim firstItem As String = ""
        While reader.Read()
            'exist
            firstItem = reader.Item("installed_appleid_email")
            If reader.Item("udid").Equals(udid) Then
                fileIpa = reader.Item("file_ipa")
                Return reader.Item("installed_appleid_email")
            End If
        End While
        If firstItem <> "" Then
            Return firstItem
        Else
            Return ""
        End If

    End Function

End Class
