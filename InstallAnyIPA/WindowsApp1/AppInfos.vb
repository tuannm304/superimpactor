Public Class AppInfos
    Public Section As String
    Public Package As String
    Public Version As String
    Public Depends As String
    Public Architecture As String
    Public Filename As String
    Public Size As String
    Public Icon As String
    Public Description As String
    Public Name As String
    Public Author As String
    Public Homepage As String
End Class

Public Class AppInfosResign
    Inherits AppInfos
    Public appleId As String
    Public password As String
    Public cloneId As String
End Class
