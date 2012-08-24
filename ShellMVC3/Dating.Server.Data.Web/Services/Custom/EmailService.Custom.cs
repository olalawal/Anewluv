



namespace Dating.Server.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;
       using System.Runtime.Serialization;
    using System.ServiceModel;
    //  using System.ServiceModel.Activation;

    using System.Net.Mail;


    // Implements application logic using the EmailModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]

    [EnableClientAccess()]
    public partial class EmailService : DomainService 
    {
        public bool SendEmail(EmailMessage  emailMessage)
        {
            bool isEmailSendSuccessfully = false;

            try
            {
                MailMessage mailMessage = new MailMessage(emailMessage.From, emailMessage.To);
                mailMessage.IsBodyHtml = true; 
                mailMessage.Subject = emailMessage.Subject;
                mailMessage.Body = emailMessage.Body;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mailMessage);
                isEmailSendSuccessfully = true;

               

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                isEmailSendSuccessfully = false;
            }

            return isEmailSendSuccessfully;
        }

    }
}
