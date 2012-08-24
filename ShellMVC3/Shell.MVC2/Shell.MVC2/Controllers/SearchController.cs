using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Shell.MVC2.Models;

using Shell.MVC2.Helpers;

using System.Security.Principal;


using System.IO;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

using MvcContrib.Filters;
using MvcContrib;



//TO DO update the custom search stuff last
namespace Shell.MVC2.Controllers
{
    public class SearchController : Controller
    {

        #region "member Search Management"

        // **************************************
        // URL: /Search/Search
        // **************************************

        //this handles the intial data load , not postback so validation is not done, p.s the string value is a 
        //dummy variable to make the get method difftrent in signatuutr from the POST method - to do fix this hack with and 
        //overload later.
        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [Authorize]
        public virtual ActionResult Search(string profileID)
        {
            SearchProfilesViewModel model = new SearchProfilesViewModel();
            //populate values

            return View(model);
        }

        // when this action result runs we need to attempt to access the user's profile to see if they have a photo uploaded using the ID
        //that was passed as well as filling out the values in the text box's
        //change this to accept query string values similar to 
        [HttpPost]
        [PassParametersDuringRedirect]
        [ModelStateToTempData]
        [Authorize]
        public virtual ActionResult Search(SearchProfilesViewModel model)
        {



            if (ModelState.IsValid)
            {



            }
            else
            {
                ModelState.AddModelError("", "Please fix your errors");
                return View(model);
            }


            return View(model);
        }

        //new search controller and view here

        //edit search controller and view here


        #endregion




    }
}
