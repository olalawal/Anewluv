
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;
using Shell.MVC2.Models.Chat;



using Ninject.Web.Mvc;
using Ninject;

using Akismet.NET;
using System.Configuration;
using Shell.MVC2.Models;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using System.Diagnostics;
using LoggingLibrary;







namespace Shell.MVC2.Filters
{

    //Region a new Filter that we will make as base 


    //Important filter to add the information from the chat service to a controller i.e the list of current users
    //this way we do not have to do anything in the conroller I think
    public class GetChatUsersDataAttribute : ActionFilterAttribute
    {
        

        [Inject]
        public IChatService _chatservice { get; set; } 



        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult == null)
                return;

            //get the currnt online users and add it to the viewbag of this filtered view
            //TO DO i dont want anything more than the gender and the screen name
            viewResult.ViewBag.OnlineChatUsers =  _chatservice.GetOnlineUsers();
            //viewResult.ViewBag.Keywords = keywords;
            //other stuff as needed i guess

            base.OnActionExecuted(filterContext);
        }
    }

    public class SessionExpireActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];

                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        ActionResult result = null;

                        if (context.Request.IsAjaxRequest())
                        {
                            result = new JsonResult { Data = new { LogonRequired = true } };
                        }
                        else
                        {
                            string redirectTo = "~/Account/Logon";
                            if (!string.IsNullOrEmpty(context.Request.RawUrl))
                            {
                                redirectTo = string.Format("~/Account/Logon?ReturnUrl={0}",
                                    HttpUtility.UrlEncode(context.Request.RawUrl));
                            }

                            result = new RedirectResult(redirectTo);
                        }

                        filterContext.Result = result;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
    
    //attribute to accespt params
     public class AcceptParameterAttribute : ActionMethodSelectorAttribute
 {
       public string Name { get; set; }
       public string Value { get; set; }
   
      public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
       {
          var req = controllerContext.RequestContext.HttpContext.Request;
          return req.Form[this.Name] == this.Value;
      }
  }

     public class NoCache : ActionFilterAttribute
     {
         public override void OnResultExecuting(ResultExecutingContext filterContext)
         {
             var ddd = DateTime.UtcNow.AddDays(-1);

             filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
             filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
             filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
             filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
         

              filterContext.HttpContext.Response.Cache.SetNoServerCaching();
             filterContext.HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
	     

                 filterContext.HttpContext.Response.Cache.SetNoStore();

             base.OnResultExecuting(filterContext);
         }
     }

    //Logging filters

    //5-8-2012 elmah logging filter thingy
     public class HandleErrorCustomAttribute : HandleErrorAttribute
     {
        

         public override void OnException(ExceptionContext context)
         {
             
             base.OnException(context);
             var requestinfo =  context.RequestContext.HttpContext.Request.ToString();
             var httpcontext = context.HttpContext ;
             var ex = context.Exception;

             if (context.ExceptionHandled) // if unhandled, will be logged anyhow in the handler                    
                 return;
             //|| RaiseErrorSignal(ex)      // prefer signaling, if possible
                     //|| IsFiltered(context))     // filtered?
            
             LogException(ex, httpcontext );
             context.ExceptionHandled = true;
         }

         private static bool RaiseErrorSignal(Exception e)
         {
             //var context = HttpContext.Current;
             //if (context == null)
             //    return false;
             //var signal = ErrorSignal.FromContext(context);
             //if (signal == null)
             //    return false;
             //signal.Raise(e, context);
             return true;
         }

         

         private static bool IsFiltered(ExceptionContext context)
         {
             return true;

             //var config = context.HttpContext.GetSection("elmah/errorFilter")
                  //                    as ErrorFilterConfiguration;

            // if (config == null)
              //   return false;

            // var testContext = new ErrorFilterModule.AssertionHelperContext(
            //                                                     context.Exception, HttpContext.Current);

           //  return config.Assertion.Test(testContext);
         }

         private static void LogException(Exception e,HttpContextBase context)
         {
            // var context = HttpContext.Current;

             //TO DO find a better way to store and retrive the app ID

             ErroLogging myLogger = new ErroLogging(1);
             using (myLogger)
             {                  
              myLogger.WriteSingleEntry(logseverityEnum.CriticalError, e,context);
             }
             
         }
     }

    public class AkismetCheckAttribute : ActionFilterAttribute
{

   // public string CommentField { get; set; }
   // public string Screenname { get; set; }
   // public string EmailField { get; set; }
    //public string Website { get; set; }
    public string ParameterName { get; set; }
    private LoggerService _loggerservicecontext;
    private AnewLuvLogEntities _loggerdb;
    private LocalEmailService  _localemailservice;
  
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //Create a new instance of the Akismet API and verify your key is valid.


        var result = (MailMessageModel)filterContext.ActionParameters[ParameterName]; 
        
        //Akismet api = new Akismet("-= API KEY HERE =-", "http://CodeTunnel.com", filterContext.HttpContext.Request.UserAgent);
       // if (!api.VerifyKey()) throw new Exception("Akismet API key invalid.");

        //Now create an instance of AkismetComment, populating it with values
        //from the POSTed form collection.
        Comment akismetComment = new Comment()
        {
            //Blog = "http://CodeTunnel.com",
            //UserIp = filterContext.HttpContext.Request.UserHostAddress,
            //UserAgent = filterContext.HttpContext.Request.UserAgent,
            //CommentContent = filterContext.HttpContext.Request[this.CommentField],
            //CommentType = "comment",
            //CommentAuthor = filterContext.HttpContext.Request[this.AuthorField],
            //CommentAuthorEmail = filterContext.HttpContext.Request[this.EmailField],
            //CommentAuthorUrl = filterContext.HttpContext.Request[this.WebsiteField]


            // initialize result
                blog = ConfigurationManager.AppSettings["AKISMET_DOMAIN"],               
                comment_type = "comment",
                comment_author =  result.SenderName ,
                comment_author_email =  result.SenderID ,
                comment_content =  result.Body ,
                //comment_author_url = ConfigurationManager.AppSettings["AKISMET_DOMAIN"],
                permalink = String.Empty,
                referrer = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath,
                user_agent = filterContext.HttpContext.Request.UserAgent,
                user_ip = filterContext.HttpContext.Request.UserHostAddress
        };

        //Check if Akismet thinks this comment is spam. Returns TRUE if spam.
        if (Extensions.IsSpam(akismetComment))
        {    //Comment is spam, add error to model state.
            filterContext.Controller.ViewData.ModelState.AddModelError("spam", "Comment identified as spam.");
            IKernel kernel = new StandardKernel();
            //log error to spam area
            _loggerservicecontext = kernel.Get<LoggerService>();
            _loggerdb = kernel.Get<AnewLuvLogEntities>();
            _localemailservice = kernel.Get<LocalEmailService>();

            //grab whatever is in the last search if its empty
            var data = new SpamMessagesLog
            {
                SessionID = HttpContext.Current.Session.SessionID,
                BlockedBy = "AkismetSpamActionFilter",
                Creationdate = DateTime.UtcNow,
                MessageBody = result.Body,
                Subject = result.Subject,
                Reason = "Comment identified as spam.",
                RecipientID = result.RecipientID,
                SenderId = result.SenderID
            };

            _loggerdb.SpamMessagesLogs.AddObject(data);
            _loggerdb.SaveChanges();

            //semd message
            //build the email model
            var Email = new EmailModel
            {
                MiscelleaneousData = data.BlockedBy,
                SenderProfileID = data.SenderId,

            };
        
          // _localemailservice 
            Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileReceivedMailMessage);          
           _localemailservice.SendEmailMessage(Email);
        }
     

        base.OnActionExecuting(filterContext);
    }
}

    public class InvalidCharsInMessageCheckAttribute : ActionFilterAttribute
    {

       
        public string ParameterName { get; set; }
        private LoggerService _loggerservicecontext;
        private AnewLuvLogEntities _loggerdb;
        private LocalEmailService _localemailservice;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Create a new instance of the Akismet API and verify your key is valid.


            var result = (MailMessageModel)filterContext.ActionParameters[ParameterName];




            if (Extensions.MessageContainsBannedChars(result.Body) | Extensions.MessageContainsBannedChars(result.Subject))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("spam", "Comment identified to have email addresses , and could be spam");
            //log error to spam area
                IKernel kernel = new StandardKernel();
                _loggerservicecontext = kernel.Get<LoggerService>();
                _loggerdb = kernel.Get<AnewLuvLogEntities>();

                //grab whatever is in the last search if its empty
                var data = new SpamMessagesLog 
                {
                    SessionID = HttpContext.Current.Session.SessionID,
                    BlockedBy = "InvalidCharsInEmailFilter",
                     Creationdate =DateTime.UtcNow ,
                      MessageBody = result.Body ,
                       Subject = result.Subject ,
                    Reason = "Comment identified to have email addresses , and could be spam",
                     RecipientID = result.RecipientID ,
                     SenderId = result.SenderID 
                     

                };

                _loggerdb.SpamMessagesLogs.AddObject(data);

                _loggerdb.SaveChanges();

                //send and email message
                //semd message
                //build the email model
                var Email = new EmailModel
                {
                    MiscelleaneousData = data.BlockedBy,
                    SenderProfileID = data.SenderId,

                };

              

                 Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileReceivedMailMessage);
                _localemailservice.SendEmailMessage(Email);

            }


            base.OnActionExecuting(filterContext);
        }
    }




}