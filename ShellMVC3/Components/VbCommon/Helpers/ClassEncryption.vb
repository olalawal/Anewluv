Imports System
Imports System.IO

Imports System.Text
Imports System.Security.Cryptography



Public Class Encryption
   


    '----------------------------------------------------------------
    ' Converted from C# to VB .NET using CSharpToVBConverter(1.2).
    ' Developed by: Kamal Patel (http://www.KamalPatel.net) 
    '----------------------------------------------------------------
    Public Shared EncryptionKey As String = "12608454@gflgF*3dg"
    Public Shared Key As Byte() = {121, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 21, 24, 175, 144, 133, 234, 196, 29, 24, 27, 17, 218, 131, 236, 53, 209}
    Public Shared IV As Byte() = {146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 251, 112, 79, 32, 114, 156}


    'from website 
    ' http://aspnet.4guysfromrolla.com/articles/101205-1.aspx
    Public Shared Function GeneratePassword(ByVal length As Integer, _
                 ByVal numberOfNonAlphanumericCharacters As Integer) As String
        'Make sure length and numberOfNonAlphanumericCharacters are valid....
        '... checks omitted for brevity ... see live demo for full code ...


        Do While True
            Dim i As Integer
            Dim nonANcount As Integer = 0
            Dim buffer1 As Byte() = New Byte(length - 1) {}

            'chPassword contains the password's characters as it's built up
            Dim chPassword As Char() = New Char(length - 1) {}

            'chPunctionations contains the list of legal non-alphanumeric characters
            Dim chPunctuations As Char() = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray()

            'Get a cryptographically strong series of bytes
            Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
            rng.GetBytes(buffer1)

            For i = 0 To length - 1
                'Convert each byte into its representative character
                Dim rndChr As Integer = (buffer1(i) Mod 87)
                If (rndChr < 10) Then
                    chPassword(i) = Convert.ToChar(Convert.ToUInt16(48 + rndChr))
                Else
                    If (rndChr < 36) Then
                        chPassword(i) = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10))
                    Else
                        If (rndChr < 62) Then
                            chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36))
                        Else
                            chPassword(i) = chPunctuations(rndChr - 62)
                            nonANcount += 1
                        End If
                    End If
                End If
            Next

            If nonANcount < numberOfNonAlphanumericCharacters Then
                Dim rndNumber As New Random
                For i = 0 To (numberOfNonAlphanumericCharacters - nonANcount) - 1
                    Dim passwordPos As Integer
                    Do
                        passwordPos = rndNumber.Next(0, length)
                    Loop While Not Char.IsLetterOrDigit(chPassword(passwordPos))
                    chPassword(passwordPos) = _
                            chPunctuations(rndNumber.Next(0, chPunctuations.Length))
                Next
            End If

            Return New String(chPassword)
        Loop
        Return Nothing
    End Function

    'the salt will be the screen name/login name since we use uniqye screen names for this application
    Public Shared Function EncodePasswordWithSalt(ByVal pwd As String, ByVal salt As String) As String
        'put the salt as upper case to elimitae case sensitivity for the user name
        Dim saltAndPwd As String = String.Concat(pwd, UCase(salt))
        Dim byteSaltAndPwd As Byte() = Encoding.UTF8.GetBytes(saltAndPwd)
        Dim Hash As New SHA1Managed
        Dim bytePasswordHashed As Byte() = Hash.ComputeHash(byteSaltAndPwd)
        Return Convert.ToBase64String(bytePasswordHashed)
    End Function

    'This function just encodes a string
    Public Shared Function EncodeString(ByVal strValue As String) As String
        'put the salt as upper case to elimitae case sensitivity for the user name

        Dim byteValue As Byte() = Encoding.UTF8.GetBytes(strValue)
        Dim Hash As New SHA1Managed
        Dim byteValueHashed As Byte() = Hash.ComputeHash(byteValue)
        Return Convert.ToBase64String(byteValueHashed)
    End Function

    'using a hidden string to decryt here in case db is cracked they still won't have the rest of the key
    Public Shared Function Encrypt(ByVal decryptedString As String, ByVal password As String) As String
        ' decryptedString = decryptedString & "12608454@gflgf*3dg"
        Dim encryptedString As String = String.Empty

        Dim decryptedBytes As Byte() = UTF8Encoding.UTF8.GetBytes(decryptedString)
        Dim saltBytes As Byte() = Encoding.UTF8.GetBytes(password)

        Using aes As New AesManaged()
            Dim rfc As New Rfc2898DeriveBytes(password, saltBytes)

            aes.BlockSize = aes.LegalBlockSizes(0).MaxSize
            aes.KeySize = aes.LegalKeySizes(0).MaxSize
            aes.Key = rfc.GetBytes(CInt(aes.KeySize / 8))
            aes.IV = rfc.GetBytes(CInt(aes.BlockSize / 8))

            Using encryptTransform As ICryptoTransform = aes.CreateEncryptor()
                Using encryptedStream As New MemoryStream()
                    Using encryptor As New CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write)
                        encryptor.Write(decryptedBytes, 0, decryptedBytes.Length)
                        encryptor.Flush()
                        encryptor.Close()

                        Dim encryptedBytes() As Byte = encryptedStream.ToArray()
                        encryptedString = Convert.ToBase64String(encryptedBytes)
                    End Using
                End Using
            End Using

        End Using

        Return encryptedString
    End Function


    Public Shared Function Decrypt(ByVal encryptedString As String, ByVal password As String) As String


        Dim decryptedString As String = String.Empty

        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedString)
        Dim saltBytes As Byte() = Encoding.UTF8.GetBytes(password)

        'added try catch to handle where the password is too short
        Try
            Using aes As New AesManaged()
                Dim rfc As New Rfc2898DeriveBytes(password, saltBytes)

                aes.BlockSize = aes.LegalBlockSizes(0).MaxSize
                aes.KeySize = aes.LegalKeySizes(0).MaxSize
                aes.Key = rfc.GetBytes(CInt(aes.KeySize / 8))
                aes.IV = rfc.GetBytes(CInt(aes.BlockSize / 8))

                Using decryptTransform As ICryptoTransform = aes.CreateDecryptor()
                    Using decryptedStream As New MemoryStream()
                        Using decryptor As New CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write)
                            decryptor.Write(encryptedBytes, 0, encryptedBytes.Length)
                            decryptor.Flush()
                            decryptor.Close()

                            Dim decryptedBytes() As Byte = decryptedStream.ToArray()
                            decryptedString = UTF8Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length)
                        End Using
                    End Using
                End Using

            End Using
        Catch ex As Exception
            Return decryptedString
        End Try
       

        Return decryptedString
    End Function



    'ENCcryption scheme used after 8/6/2011 - to allow for salts always being at least 8 bytes
    'users will have to change passwords



    Public Shared Function encryptString(ByVal dataString As String) As String

        Using Aes As AesManaged = New AesManaged()

            '  dynamic aes = new AesManaged();
            Dim encryptor As ICryptoTransform = Aes.CreateEncryptor(Key, IV)
            Return performCryptoAndEncoding(dataString, encryptor)


        End Using


    End Function


    Public Shared Function decryptString(encryptedString) As String

        Using Aes As AesManaged = New AesManaged()

            Dim decryptor As ICryptoTransform = Aes.CreateDecryptor(Key, IV)
            Return performCryptoAndDecoding(encryptedString, decryptor)

        End Using


    End Function


    Public Shared Function performCryptoAndEncoding(dataToEncryptOrDecrypt As String, transform As ICryptoTransform) As String

        Dim encodedData As Byte() = encodeStringToBytes(dataToEncryptOrDecrypt)



        Dim dataStream As New MemoryStream()


        Dim encryptionStream As New CryptoStream(dataStream, transform, CryptoStreamMode.Write)
        encryptionStream.Write(encodedData, 0, encodedData.Length)


        encryptionStream.FlushFinalBlock()
        dataStream.Position = 0

        Dim transformedBytes As Byte() = dataStream.ToArray()
        dataStream.Read(transformedBytes, 0, transformedBytes.Length)

        encryptionStream.Close()
        dataStream.Close()



        'insteand of normal saving the  encoded function as a noremal string of bytes we  them as base 64 so as not to lose padding
        'string transformedAndReencodedData = encodeBytesToString(transformedBytes);
        ' http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments
        ' byte[] encrypted = transformedBytes.ToArray();

        Return Convert.ToBase64String(transformedBytes)




        'return transformedAndReencodedData;

    End Function


    Public Shared Function performCryptoAndDecoding(dataToEncryptOrDecrypt As String, transform As ICryptoTransform) As String

        'byte[] encodedData = encodeStringToBytes(dataToEncryptOrDecrypt);

        Try
            Dim encodedData As Byte() = Convert.FromBase64String(dataToEncryptOrDecrypt)


            Dim dataStream As New MemoryStream()



            Dim encryptionStream As New CryptoStream(dataStream, transform, CryptoStreamMode.Write)
            encryptionStream.Write(encodedData, 0, encodedData.Length)


            encryptionStream.FlushFinalBlock()
            dataStream.Position = 0

            Dim transformedBytes As Byte() = dataStream.ToArray()
            dataStream.Read(transformedBytes, 0, transformedBytes.Length)

            encryptionStream.Close()
            dataStream.Close()





            'insteand of normal encoding we transformed them encyprt them as base 64 
            Dim transformedAndReencodedData As String = encodeBytesToString(transformedBytes)
            ' http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments
            ' byte[] encrypted = transformedBytes.ToArray();

            'return transformedBytes;




            Return transformedAndReencodedData

        Catch ex As Exception
            Return "ErrorDecoding"
        End Try
       

    End Function





    Public Shared Function encodeStringToBytes(dataToEncode As String) As Byte()
       

        Dim encodedData As Byte() = Encoding.Unicode.GetBytes(dataToEncode)
        Return encodedData
    End Function

    Public Shared Function encodeBytesToString(dataToEncode As Byte()) As String


        Dim encodedData As String = Encoding.Unicode.GetString(dataToEncode, 0, dataToEncode.Length)
        Return encodedData


    End Function



End Class

