Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Ionic.Zip

Public Class Common
    Public Enum ShowWindowType
        Hide = 0
        Minimized = 1
        Maximized = 2
        Restore = 9
    End Enum

    Private Declare Auto Function ShowWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal hideType As ShowWindowType) As Integer

    Shared Sub Unzip(ZipToUnpack As String, UnpackDirectory As String, Optional Password As String = "")
        Using zip1 As ZipFile = ZipFile.Read(ZipToUnpack)
            Dim en As ZipEntry
            If Password <> "" Then
                zip1.Password = Password
            End If
            ' here, we extract every entry, but we could extract conditionally,
            ' based on entry name, size, date, checkbox status, etc.   
            For Each en In zip1
                en.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub

    Shared Sub Zip(Folder As String, ZipFile As String)
        Using zip1 As ZipFile = New ZipFile()
            zip1.AddDirectory(Folder)
            zip1.Save(ZipFile)
        End Using
    End Sub

    Shared Function GetTempFolder() As String
        Dim folder As String = Path.Combine(Path.GetTempPath, Path.GetRandomFileName)
        Do While Directory.Exists(folder) Or File.Exists(folder)
            folder = Path.Combine(Path.GetTempPath, Path.GetRandomFileName)
        Loop

        Return folder
    End Function

    Shared Function GetHash(theInput As String) As String

        Using hasher As MD5 = MD5.Create()    ' create hash object

            ' Convert to byte array and get hash
            Dim dbytes As Byte() =
                 hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Return sBuilder.ToString()
        End Using

    End Function

    Shared Async Function RunExe(exe As String, params As String, Optional getError As Boolean = False) As Task(Of String)
        Dim oProcess As New Process()
        Dim oStartInfo As New ProcessStartInfo(exe, params)
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        If getError Then
            oStartInfo.RedirectStandardError = True
        End If
        oStartInfo.CreateNoWindow = True
        oStartInfo.WindowStyle = ProcessWindowStyle.Hidden
        oProcess.StartInfo = oStartInfo
        oProcess.EnableRaisingEvents = True
        oStartInfo.Verb = "runas"
        Try
            oProcess.Start()
            'ShowWindow(oProcess.MainWindowHandle, ShowWindowType.Hide)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = Await oStreamReader.ReadToEndAsync()
        End Using
        Dim sEO As String
        If getError Then
            Using oStreamReader As System.IO.StreamReader = oProcess.StandardError
                sEO = Await oStreamReader.ReadToEndAsync()
            End Using
            Return sOutput + "Error: " + sEO
        End If
        Return sOutput
    End Function

    Shared Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return input 'If encryption fails, return the unaltered input.
        End Try
    End Function
    'Decrypt a string with AES
    Shared Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return input 'If decryption fails, return the unaltered input.
        End Try
    End Function

    Shared Sub DeleteFilesFromFolder(Folder As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder)
                Try
                    File.Delete(_file)
                Catch
                End Try
            Next
            For Each _folder As String In Directory.GetDirectories(Folder)

                DeleteFilesFromFolder(_folder)
                Try
                    Directory.Delete(_folder, True)
                Catch
                End Try
            Next

        End If

    End Sub

    Shared Sub DeleteFolderInFolder(Folder As String, deleteExtension As String, ByRef fnd As Boolean)
        If Directory.Exists(Folder) Then
            For Each _folder As String In Directory.GetDirectories(Folder)
                If Path.GetExtension(_folder) = "." & deleteExtension Then
                    Directory.Delete(_folder, True)
                    fnd = True
                Else
                    DeleteFolderInFolder(_folder, deleteExtension, fnd)
                End If

            Next

        End If

    End Sub

    Shared Sub DownloadAndInstall(ipaLink As String, ipaName As String)
        Dim downloadPro As New DownloadProgress
        Dim bComplete = downloadPro.Download(ipaLink, AppConst.m_rootPath + AppConst.LOCALTMP + "\download.ipa", ipaName)
        If bComplete Then
            Dim frm As New Install
            frm.installFromFile(AppConst.m_rootPath + AppConst.DOWNLOAD + ipaName)
        End If
        Try
            File.Delete(AppConst.LOCALTMP + "\download.ipa")
        Catch ex As Exception

        End Try
    End Sub

    Shared Sub Download(downloadPro As DownloadProgress, ipaLink As String, ipaName As String)
        downloadPro.DownloadAsync(ipaLink, AppConst.m_rootPath + AppConst.LOCALTMP + "\download" & CLng((DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds) & ".ipa", ipaName)
    End Sub
End Class
