Imports System.Data.SQLite

Public Class AppConst
    Public Const API_SENDLOG As String = "http://superimpactor.net/api/send_log.php"
    Public Const DB As String = "\iaidb.sqlite"
    Public Const IMAGE As String = "\res\"
    Public Const IMOBILEDEVICE As String = "\libimobiledevice\"
    Public Const OPENSSL As String = "\openssl\bin\"
    Public Const ZIP7 As String = "\7zip\"
    Public Const CERTSTORE As String = "\certs\"
    Public Const DOWNLOAD As String = "\download\"
    Public Const LOCALTMP As String = "\tmp\"
    Public Shared m_localTmp As String
    Public Shared m_dbConnection As SQLiteConnection
    Public Shared m_lstCydiaRepos As Dictionary(Of Integer, String)
    Public Shared m_rootPath As String
    Public Shared m_randomKey As String
    Public Shared logger As log4net.ILog
    Public Shared mainForm As frmSuperImpactor
End Class
