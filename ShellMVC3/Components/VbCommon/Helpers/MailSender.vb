Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Configuration
Imports EmailSMTP.clsEmailSMTP
Imports System.Web.HttpUtility


Public Class MailSender

   'public class
    Shared email As New EmailSMTP.clsEmailSMTP

    'this button a single mail mesage
    'the from is not needed since we sent is from an appsettings value
    Public Shared Function SendMailMessage(ByVal recipient As String, ByVal bcc As String, ByVal cc As String, ByVal subject As String, ByVal body As String) As Boolean


        ' Instantiate a new instance of MailMessage
        Dim Mail As New System.Net.Mail.MailMessage

        ' Set the sender address of the mail message
        Mail.From = New MailAddress(ConfigurationManager.AppSettings("MailSender"), "NigeriaConnections")
        ' Set the recepient address of the mail message

        'added a function to split up the recipient addresses if they conists of more than one addy
        Dim addrArray As String() = recipient.Split(","c)
        For Each emailAddr As String In addrArray
            Mail.To.Add(New MailAddress(emailAddr))
        Next



        'Mail.To.Add(New MailAddress(recipient))

        ' Check if the bcc value is nothing or an empty string
        If Not bcc Is Nothing And bcc <> String.Empty Then
            ' Set the Bcc address of the mail message
            Mail.Bcc.Add(New MailAddress(bcc))
        End If

        ' Check if the cc value is nothing or an empty value
        If Not cc Is Nothing And cc <> String.Empty Then
            ' Set the CC address of the mail message
            Mail.CC.Add(New MailAddress(cc))
        End If

        ' Set the subject of the mail message
        Mail.Subject = subject
        ' Set the body of the mail message
        Mail.Body = body

        ' Set the format of the mail message body as HTML
        Mail.IsBodyHtml = True
        ' Set the priority of the mail message to normal
        Mail.Priority = MailPriority.Normal
        Mail.SubjectEncoding = UTF8Encoding.UTF8


    
        ' Instantiate a new instance of SmtpClient
        Dim mailClient As SmtpClient = New SmtpClient

        Try
            Dim client = New SmtpClient
            Dim token As Object = "notsent"
            Dim basicAuthenticationInfo As _
   New System.Net.NetworkCredential("olawal", "olakayode02", "Nmedia.com")


       'eventual host
            ' client.Host = "mail.astrocomputersystems.com"
            client.Host = ConfigurationManager.AppSettings("ExchangeServer")

            'client.Host = "192.168.0.110"
            client.Credentials = basicAuthenticationInfo
            ' client.UseDefaultCredentials 
            client.Send(Mail)
            'client.Dispose()
            'if no error return true
            Return True
        Catch ex As Exception
            Dim ex2 As Exception = ex
            Dim errorMessage As String = String.Empty
            While Not (ex2 Is Nothing)
                errorMessage += ex2.ToString()
                ex2 = ex2.InnerException
            End While
            'store this error message in the email errors database
            'add code for this later
            'Log any e-mail error in emailerrors table.
            Dim StrSQL As String = ""

            'no need to inseart the time feild since it is a timestamp type and auto increments based on 
            'the current time of the inseartion
            StrSQL = "INSERT INTO emailerrors (FromEmail,ToEmail,Subject,Body,ErrorDate,ExceptionError) " & _
             "VALUES ('" & ConfigurationManager.AppSettings("MailSender") & "','" & recipient & "','" & HtmlEncode(subject) & "','" & _
                      HtmlEncode(body) & "','" & Now() & "','" & HtmlEncode(errorMessage) & "');"

            ',"'" &  HtmlEncode(subject) & "'",HtmlEncode(body) &, NOW(), HtmlEncode(errorMessage));"
            'Try
            Data_Access.ExecuteDatingDBsql(StrSQL)
            'Catch ex3 As Exception
            '    errorMessage = ex3.ToString
            'End Try
            'blnProfileCreated = True
            'Catch excep As Exception
            Return False
        End Try


    End Function

    Private Shared Function GetMailCredentials() As System.Net.NetworkCredential


        Dim cred As New System.Net.NetworkCredential()

        cred.UserName = ConfigurationManager.AppSettings("User")

        cred.Password = ConfigurationManager.AppSettings("password")

        cred.Domain = ConfigurationManager.AppSettings("Domain")

        Return cred

    End Function


 'this mod was stolen from trialbills , we use the same app config settings from work
    Public Shared Function EmailError(ByVal strErrorTitle As String, ByVal strErrorMessage As String) As Boolean
        Dim emailProgrammers As String = ConfigurationManager.AppSettings("EmailProg")
        Dim emailfrom As String = ConfigurationManager.AppSettings("EmailFrom")

        Dim sendto As String = ConfigurationManager.AppSettings("EMailAdmin")

        Try
             email.Send(sendto, emailfrom, strErrorTitle, strErrorMessage)
            Return True
            'store the error if it occurs in the database
        Catch ex As Exception
            Dim ex2 As Exception = ex
            Dim errorMessage As String = String.Empty
            While Not (ex2 Is Nothing)
                errorMessage += ex2.ToString()
                ex2 = ex2.InnerException
            End While
            'store this error message in the email errors database
            'add code for this later
            'Log any e-mail error in emailerrors table.
            Dim StrSQL As String = ""


            StrSQL = "INSERT INTO emailerrors (FromEmail,ToEmail,Subject,Body,ExceptionError,ErrorDate) " & _
            "VALUES ('" & emailfrom & "','" & sendto & "','" & HtmlEncode(strErrorTitle) & "','" & _
            HtmlEncode(errorMessage) & "','" & HtmlEncode(errorMessage) & "','" & DateTime.Now & "' );"

            Try 'send mail add a try catch here too
                '2/17/2010 send email to myself 
                MailSender.SendMailMessage(ConfigurationManager.AppSettings("EMailToMeOnly").ToString(), _
                                            "", "", "EMail error generated", _
                                            "The user " & sendto & "generated the following errror:" & _
                                             HtmlEncode(errorMessage) & (DateTime.Now) & " inner exception was :" & ex2.InnerException.ToString)

                'StrSQL = "INSERT INTO emailerrors (FromEmail,ToEmail,Subject,Body,ErrorDate,ExceptionError) " & _
                ' "VALUES ('" &  & "','" &  & "',""" & HtmlEncode() & _
                ' """,""" & HtmlEncode() & """, NOW(),""" & HtmlEncode() & """);"
                Data_Access.ExecuteDatingDBsql(StrSQL)
            Catch ex3 As Exception
                Return False

            End Try
         
            'End Try
            'blnProfileCreated = True
            'Catch excep As Exception
            Return False
        End Try



    End Function




End Class

