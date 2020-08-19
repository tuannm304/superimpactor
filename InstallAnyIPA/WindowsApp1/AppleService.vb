Imports System.IO
Imports System.Net
Imports System.Web.HttpUtility
Imports Claunia.PropertyList

Public Class AppleService
    ' Apple web services base URL
    Private Shared base_url_services As String = "https://developerservices2.apple.com/services/QH65B2/"
    Private Shared protocol_version_daw2 As String = "A1234"
    Private Shared protocol_version_connect1 As String = "QH65B2"
    Private Shared app_id_key As String = "ba2ec180e6ca6e6c6a542255453b24d6e6e5b2be0cc48bc1b0d8ad64cfe0228f"
    Private Shared client_id As String = "XABBG36SBA"
    Private Shared cookie As CookieContainer

    Shared Function login(appleId As String, password As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create("https://idmsa.apple.com/IDMSWebAuth/clientDAW.cgi")

            myReq.Method = "POST"
            myReq.ContentType = "application/x-www-form-urlencoded"

            Dim postData As String = "format=plist&appIdKey=" + app_id_key +
                    "&appleId=" + UrlEncode(appleId) + "&password=" + UrlEncode(password) +
                    "&userLocale=en_US&protocolVersion=" + protocol_version_daw2

            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.KeepAlive = True

            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)
            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function listTeam(accInfo As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "listTeams.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>myacinfo</key>" + vbLf +
                vbTab + "<string>" + accInfo + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            cookie = New CookieContainer()
            cookie.Add(New Uri("https://developerservices2.apple.com/"), New Cookie("myacinfo", accInfo))

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function listDevices(teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/listDevices.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function addDevice(udid As String, deviceName As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/addDevice.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>deviceNumber</key>" + vbLf +
                vbTab + "<string>" + udid + "</string>" + vbLf +
                vbTab + "<key>name</key>" + vbLf +
                vbTab + "<string>" + deviceName + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)

            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function allDevelopmentCert(teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/listAllDevelopmentCerts.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"
            myReq.Timeout = 10000

            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function addDevelopmentCert(csrContent As String, machineName As String, machineId As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/submitDevelopmentCSR.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>machineName</key>" + vbLf +
                vbTab + "<string>" + machineName + "</string>" + vbLf +
                vbTab + "<key>machineId</key>" + vbLf +
                vbTab + "<string>" + machineId + "</string>" + vbLf +
                vbTab + "<key>csrContent</key>" + vbLf +
                vbTab + "<string>" + csrContent + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function appIds(teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/listAppIds.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function addAppId(appName As String, appId As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/addAppId.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>identifier</key>" + vbLf +
                vbTab + "<string>" + appId + "</string>" + vbLf +
                vbTab + "<key>name</key>" + vbLf +
                vbTab + "<string>" + appName + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function downloadProvisionProfile(appIdId As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/downloadTeamProvisioningProfile.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>appIdId</key>" + vbLf +
                vbTab + "<string>" + appIdId + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function revokeCertificate(serialNumber As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/revokeDevelopmentCert.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>serialNumber</key>" + vbLf +
                vbTab + "<string>" + serialNumber + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Shared Function deleteAppId(appIdId As String, teamId As String) As NSDictionary
        Try
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create(base_url_services + "/ios/deleteAppId.action?clientId=" + client_id)

            myReq.Method = "POST"
            myReq.ContentType = "text/x-xml-plist"


            Dim postData As String =
                "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbLf +
                "<!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">" + vbLf +
                "<plist version=""1.0"">" + vbLf +
                "<dict>" + vbLf +
                vbTab + "<key>clientId</key>" + vbLf +
                vbTab + "<string>" + client_id + "</string>" + vbLf +
                vbTab + "<key>teamId</key>" + vbLf +
                vbTab + "<string>" + teamId + "</string>" + vbLf +
                vbTab + "<key>appIdId</key>" + vbLf +
                vbTab + "<string>" + appIdId + "</string>" + vbLf +
                vbTab + "<key>protocolVersion</key>" + vbLf +
                vbTab + "<string>" + protocol_version_connect1 + "</string>" + vbLf +
                vbTab + "<key>requestId</key>" + vbLf +
                vbTab + "<string>" + Guid.NewGuid().ToString().ToUpper() + "</string>" + vbLf +
                vbTab + "<key>userLocale</key>" + vbLf +
                vbTab + "<array>" + vbLf +
                vbTab + vbTab + "<string>en_US</string>" + vbLf +
                vbTab + "</array>" + vbLf +
                "</dict>" + vbLf +
                "</plist>" + vbLf
            myReq.UserAgent = "Xcode"
            myReq.Accept = "text/x-xml-plist"
            myReq.Headers.Add("Accept-Language", "en-us")
            myReq.Headers.Add("X-Xcode-Version", "7.0 (7A120f)")
            myReq.Headers.Add("Accept-Encoding", "gzip, deflate")
            myReq.KeepAlive = True
            'myReq.ServicePoint.Expect100Continue = False
            myReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11

            myReq.CookieContainer = cookie
            myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(postData), 0, System.Text.Encoding.UTF8.GetBytes(postData).Count)


            myResp = myReq.GetResponse
            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String
            myText = myreader.ReadToEnd
            File.WriteAllText(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist", myText)
            Dim filePl As FileInfo = New FileInfo(AppConst.m_rootPath + AppConst.LOCALTMP + "data.plist")
            myReq.GetRequestStream.Close()
            myReq.GetRequestStream.Dispose()
            myResp.Close()
            myResp.Dispose()
            Return PropertyListParser.Parse(filePl)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

End Class

