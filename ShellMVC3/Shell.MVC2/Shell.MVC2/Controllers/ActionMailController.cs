using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net;
using ActionMailer.Net.Mvc;

using Shell.MVC2.ViewModels.Email;

using Shell.MVC2.Models;
using Shell.MVC2.Filters;

namespace Shell.MVC2.Controllers
{
    public class ActionMailController : MailerBase 
    {

        public EmailResult VerificationEmail(EmailModel model)
        {
            To.Add(model.ProfileID);
            From = Resources.EmailMessageResources.DefaultEmailSender;
            Subject = model.MessageSubject;
            return Email("UpdateEmail", model);
        }

        [GetChatUsersData]
        public EmailResult WeeklyMatches(EmailModel model)
        {
            To.Add(model.ProfileID);
            From = Resources.EmailMessageResources.DefaultEmailSender;
            Subject = model.MessageSubject;

            //create the EMailMatchesViewModel
            EmailMatchesViewModel viewmodel = new EmailMatchesViewModel(model.ProfileID);
            viewmodel.EmailModel = model;

            return Email("WeeklyMatches", viewmodel);

        }




    }
}
