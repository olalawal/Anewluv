
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
    using Dating.Server.Data;
    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Security.Principal;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using Dating.Server.Data.Models;
    using Common;
    using System.Runtime.Serialization; 
    using System.Web.Security;
    using System.Web;
    //this domain serverice is used by silverlight to authenticate users , the custom membership provider is used by MVC
    //to do - force SL to use the custom one as well.
    [EnableClientAccess()]
    public class AnewluvAuthenticationDomainService : LinqToEntitiesDomainService<AnewLuvFTSEntities>, IAuthentication<User>
    {

        DatingService context = new DatingService();

        private static User DefaultUser = new User()
        {
          
     
            ScreenName  = string.Empty,
            //llName = string.Empty,
            //PhoneNumber = string.Empty,
          
        };
       
        // Dim _service As AnewluvAuthenticationDomainService = New AnewluvAuthenticationDomainService()
        //change it to use GetProfileIdbyUsername in dating service
        protected User GetAuthenticatedUser(System.Security.Principal.IPrincipal principal)
        {
                        

            //pull authenticated user stuff from the session instead or rebuilding it
           // string strScreenName = "";
            //using (AnewluvFTSEntities context = new AnewluvFTSEntities())
            //{
                

            //    var tmpScreenName = from p in context.profiles
            //                        where p.UserName == principal.Identity.Name
            //                        select new { p.ScreenName };

            //    strScreenName = tmpScreenName.FirstOrDefault().ToString();

            //}

            User usr = new User();
                         
             usr.ScreenName  = HttpContext.Current.Session["ScreenName"].ToString();
             usr.Name = HttpContext.Current.Session["UserName"].ToString();
             usr.Roles = HttpContext.Current.Session["Roles"].ToString().Split(new Char[] { ',' });
            //usr.ScreenName = strScreenName;
            //usr.Name = principal.Identity.Name;

            return usr;
        }

        protected virtual void IssueAuthenticationToken(string principal, bool isPersistent)
        {
            //add other values here as well?
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                                    1,
                                                    principal,
                                                    DateTime.Now,
                                                    DateTime.Now.AddMinutes(30),
                                                    true,
                                                    string.Empty,
                                                    FormsAuthentication.FormsCookiePath
                                                    );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(30);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }



       

         public User Login(string userName, string password, bool isPersistent,string customdata)
        {


            #if DEBUG
            Console.WriteLine("Debug version");
            if (this.ValidateUser(userName))
            #else
                  if (this.ValidateUser(userName, password))             
            #endif
            {
                

                 //FormsAuthentication.SetAuthCookie(userName, isPersistent);
                //custom cookie test
                IssueAuthenticationToken(userName, isPersistent);

                return GetUser(userName);
                
              
            }
            return null;
        }


         public void UpdateUser(User user)
         {
             // Ensure the user data that will be modified represents the currently
             // authenticated identity 
             if ((this.ServiceContext.User == null) ||
                 (this.ServiceContext.User.Identity == null) ||
                 !string.Equals(this.ServiceContext.User.Identity.Name, user.Name, System.StringComparison.Ordinal))
             {
                 throw new UnauthorizedAccessException("You are only authorized to modify your own profile.");
             }


            // this.ObjectContext.profiles.AttachAsModified( , this.ChangeSet.GetOriginal(user));
             
         }


        //use the code in the custom validation provider so we keep one codebase
        protected bool ValidateUser(string username, string password)
        {

            var myMembershipProvider = new AnewLuvMembershipProvider();

            if (myMembershipProvider.ValidateUser(username, password))
            {
                return true;
            }
            return false;
        }

        //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
        protected bool ValidateUser(string username)
        {
            var myMembershipProvider = new AnewLuvMembershipProvider();

            if (myMembershipProvider.ValidateUser(username))
            {
                return true;
            }
            return false;
        }

        public User GetUser()
        {
            if ((this.ServiceContext != null) &&
                (this.ServiceContext.User != null) &&
                this.ServiceContext.User.Identity.IsAuthenticated)
            {
                return this.GetUser(this.ServiceContext.User.Identity.Name);
            }
            return AnewluvAuthenticationDomainService.DefaultUser;
        }

        private User GetUser(string userName)
        {

            string strScreenName = "";

            using (AnewLuvFTSEntities context = new AnewLuvFTSEntities())
            {


                var tmpScreenName = from p in context.profiles
                                    where p.UserName == userName 
                                    select new { p.ScreenName };

                strScreenName = tmpScreenName.FirstOrDefault().ToString();

            }

            User usr = new User();
            usr.ScreenName = strScreenName;
            usr.Name = userName;

            return usr;
           
        }

     

        //logs u out
        public User Logout()
        {
            FormsAuthentication.SignOut();
            return AnewluvAuthenticationDomainService.DefaultUser;
        }


       

    }


  



    public partial class User : UserBase , IUser
    {

        private string _ScreenName;
        /// <summary>
        /// Gets and sets the friendly name of the user.
        /// </summary>


        // NOTE: Profile properties can be added for use in Silverlight application.
        // To enable profiles, edit the appropriate section of web.config file.
        //
        // public string MyProfileProperty { get; set; }
        [DataMember]
        [Key]
        public string ScreenName
        {
            get { return _ScreenName; }
            set { _ScreenName = value; }
        }

      

        //Public Property PhoneNumber() As String
        //    Get
        //        Return m_PhoneNumber
        //    End Get
        //    Set(ByVal value As String)
        //        m_PhoneNumber = Value
        //    End Set
        //End Property
        //Private m_PhoneNumber As String


       
    }

   
}