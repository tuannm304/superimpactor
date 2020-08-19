Public Class AppleAppInfo
    Public price As Double
    Public fileSizeBytes As String
    Public description As String
    Public releaseDate As String
    Public minimumOsVersion As String
    Public screenshotUrls As List(Of String)
End Class

Public Class ItuneResult
    Public resultCount As Integer
    Public results As List(Of AppleAppInfo)
End Class