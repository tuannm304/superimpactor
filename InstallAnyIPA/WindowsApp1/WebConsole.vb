Imports System.Net

Public Class WebConsole
    Public cookie As CookieContainer
    Function http(url As String, Optional postData As String = "") As String
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(url)
            myReq.AllowAutoRedirect = True
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11
            If Not cookie Is Nothing Then
                myReq.CookieContainer = cookie
            Else
                cookie = New CookieContainer
            End If

            If postData <> "" Then
                myReq.Method = "POST"
                myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)
            End If

            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            cookie.Add(myResp.Cookies)
            If postData <> "" Then
                myReq.GetRequestStream.Close()
                myReq.GetRequestStream.Dispose()
            End If
            myResp.Close()
            myResp.Dispose()
            Return myText
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function

    Public Function getKeyValue(rs As String, key As String, ByRef value As String, Optional fromPos As Integer = 0, Optional keyWithoutQuote As Boolean = False, Optional quoteChar As String = """")
        If Not keyWithoutQuote Then
            key = quoteChar + key + quoteChar
        End If
        Dim start As Integer = rs.IndexOf(key, fromPos)
        If start = -1 Then
            Return "Cannot find key"
        End If

        Dim start2 As Integer = rs.IndexOf(quoteChar, start + key.Length)
        If start2 = -1 Then
            Return "incorrect key value 1"
        End If

        start2 = start2 + 1
        Dim start3 As Integer = rs.IndexOf(quoteChar, start2)
        If start3 = -1 Then
            Return "incorrect key value 2"
        End If
        value = rs.Substring(start2, start3 - start2)
        Return ""
    End Function

End Class