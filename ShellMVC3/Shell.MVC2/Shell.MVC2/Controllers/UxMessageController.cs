using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shell.MVC2.Resources;
using Shell.MVC2.AppFabric;
using Shell.MVC2.Models;

namespace Shell.MVC2.Controllers
{
    public partial  class UxMessageController : Controller
    {
       
       // public DatingService datingservicecontext;
        private MembersRepository _membersrepository;
       
        //TO DO convert the other services to do this as well
        public UxMessageController(MembersRepository membersrepositiory)
       {           
           _membersrepository = membersrepositiory;
       }

        [HttpPost]
        [Authorize]
       public   JsonResult  Getmessage(string UxMessageId)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            var model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);

            return Json(new { message = parseUxMessageID(Convert.ToInt32(UxMessageId), model), screenname = model.Profile.ScreenName, success = true });
        }

        [HttpPost]
        public ActionResult Action()
        {
            return View();
        }

        string parseUxMessageID(int UxMessageId,MembersViewModel model)
        {
            var returnmessage = "";
            switch (UxMessageId)
            {
                case 0:  //decativate message
                   //we need the users screen name for this message
                    returnmessage = String.Format(UxMessages.DeactivateAccountMessage,model.Profile.ScreenName );
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return returnmessage;
        }



    }
}
