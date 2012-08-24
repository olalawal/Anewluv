using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

using Shell.MVC2.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;
using Shell.MVC2.AppFabric;



namespace Shell.MVC2.Controllers



{
    public class ComboBoxController : Controller
    {

        private MembersRepository  membersrepo = new  MembersRepository();
        private SharedRepository sharedrepo = new SharedRepository();
        private  PostalDataService postaldataservicecontext = new PostalDataService().Initialize();


        //TO Do , mighh be able to do away with the account methods since we 
        //now cache guest data as well , so they are good till its cleared.
        //Memebers only so profileID is assumed
        [Authorize]
        [HttpPost]
        public ActionResult _FilteringAjaxAccount(string text, int? filterMode)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
          //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            MembersViewModel model = new MembersViewModel();      
            //updated 9/29/2011 - test this if statement for logged on and non logged on members

            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);

            //this handles filterring for register and quick search sevelal models
            //

            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items = sharedrepo.GetFilteredCities(text, model.Account.Country, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);


            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }

        [HttpPost]
        public ActionResult _FilteringAjaxRegister(string text,string country, int? filterMode)
        {
            //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            //MembersViewModel model = new MembersViewModel();
           // model  = membersrepo.GetMembersViewModel(this.HttpContext);

            //this handles filterring for register and quick search sevelal models
            //

            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items = sharedrepo.GetFilteredCities(text, country, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);

            if (items == null) return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = "" };
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }
        
        [HttpPost]
        public ActionResult _FilteringAjax(string text, int? filterMode)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);


            //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            MembersViewModel model = new MembersViewModel();
           // var  cf = new CachingFactory.MembersViewModelHelper();
            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID):CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


            //this handles filterring for register and quick search sevelal models
            //

            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items =  sharedrepo.GetFilteredCities(text, model.MyQuickSearch.MySelectedCountryName, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);


            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }

        [HttpPost]
        public JsonResult GetDefaultCityList(string CountryName)
        {

           


            if (CountryName != "" && CountryName != "Select A Country")
            {

                string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
                //retrive and save the country i think
                MembersViewModel model = new MembersViewModel();

                model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


                // model.MyQuickSearch.MySelectedCountryName = CountryName;
                //model = (  || model != null) ? (from q in Profiles.Where(a => a.DistanceFromMe <= MaxDistanceFromMe) select q) :
                //                   Profiles;
                //update it now with the country name if user is logged in then add it
                //if we have no quicksearch model create a temporary one to use to store the current country

                //get the postal code status of this country , only applies to the model not to search model since users have no countrol of 
                //what countrie postal code status is for the users they have in thier list

                //store postal code status
                bool CurrentPostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(CountryName) == 1) ? true : false;

               
                model.MyQuickSearch.MySelectedCountryName = CountryName;

                //clear out old values wehn country changes
                //TO DO find a better way to get default city values for now
                //save the selected city on submit tho
                model.MyQuickSearch.MySelectedCityStateProvince = "";
                model.MyQuickSearch.MySelectedCity = "";

                //update Cache
                //depeneding on guest or member 
                model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData (model,_ProfileID) : CachingFactory.MembersViewModelHelper.UpdateGuestData(model,this.HttpContext);
            

                return Json(new { Cities = sharedrepo.GetFilteredCities("", CountryName, 0), PostalCodeStatus = CurrentPostalCodeStatus, MyCity = model.MyCity });
                //return Json(sharedrepo.GetFilteredCities("",CountryName ,0));

            }

           

            return Json(new object());

        }


        //get the city list for the account model
        [Authorize]
        [HttpPost]
        public JsonResult GetDefaultCityListAccount(string CountryName)
        {
            

            //Updated to ignore when no city is selected
            if (CountryName != "" && CountryName != "Select A Country")
            {

                string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

                //retrive and save the country i think
                MembersViewModel model = new MembersViewModel();

                model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);


                // model.MyQuickSearc.MySelectedCountryName = CountryName;
                //model = (  || model != null) ? (from q in Profiles.Where(a => a.DistanceFromMe <= MaxDistanceFromMe) select q) :
                //                   Profiles;
                //update it now with the country name if user is logged in then add it
                //if we have no quicksearch model create a temporary one to use to store the current country

                //get the postal code status of this country , only applies to the model not to search model since users have no countrol of 
                //what countrie postal code status is for the users they have in thier list

                //store postal code status
                bool CurrentPostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(CountryName) == 1) ? true : false;

                //update the account model
                model.Account.Country = CountryName;
                model.Account.PostalCodeStatus = CurrentPostalCodeStatus;


                //update session 
                CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID);

                return Json(new { Cities = sharedrepo.GetFilteredCities("", CountryName, 0), PostalCodeStatus = CurrentPostalCodeStatus, MyCity = model.Account.City });



            }
               
            //return Json(sharedrepo.GetFilteredCities("",CountryName ,0));

            return Json(new object());

        }


        //get the city list for the account model
        [HttpPost]
        public JsonResult GetDefaultCityListRegister(string CountryName)
        {

            MembersViewModel model = new MembersViewModel();
            model = CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);

            //retrive and save the country i think
            //MembersViewModel model = new MembersViewModel();

          //  model = membersrepo.GetMembersViewModel(this.HttpContext);

            // model.MyQuickSearch.MySelectedCountryName = CountryName;
            //model = (  || model != null) ? (from q in Profiles.Where(a => a.DistanceFromMe <= MaxDistanceFromMe) select q) :
            //                   Profiles;
            //update it now with the country name if user is logged in then add it
            //if we have no quicksearch model create a temporary one to use to store the current country

            //get the postal code status of this country , only applies to the model not to search model since users have no countrol of 
            //what countrie postal code status is for the users they have in thier list

       
            //update the account model
       


            ////update session
          //   CachingFactory.MembersViewModelHelper.UpdateMemberData(model, this.HttpContext);


            if (CountryName != "" && CountryName != "Select A Country")
            {
                //store postal code status
                bool CurrentPostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(CountryName) == 1) ? true : false;

                //update register model
                model.Register.Country = CountryName;
                model.Register.PostalCodeStatus = CurrentPostalCodeStatus;

                //update guest stuff
                CachingFactory.MembersViewModelHelper.UpdateGuestData(model, this.HttpContext);


                return Json(new { Cities = sharedrepo.GetFilteredCities("", CountryName, 0), PostalCodeStatus = CurrentPostalCodeStatus });

                //return Json(sharedrepo.GetFilteredCities("",CountryName ,0));
            }
            return Json(new object());

        }

        [HttpPost]
        public JsonResult GetCurrentCity(string ProfileID)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

            //retrive and save the country i think        
            MembersViewModel model = new MembersViewModel();
           model =   (_ProfileID != null)?  CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID): CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


            //get the most current version of the profile/
            //var myProfile = membersrepo.GetProfileDataByProfileID(model.Profile.ProfileID);
            
           if (model.profiledata != null &&  model.profiledata.City != null)
           {
               return Json(new { MyCity = model.profiledata.City });
           }
           else if (model.MyQuickSearch != null &&  model.MyQuickSearch.MySelectedCity != null)
           {
               return Json(new { MyCity = model.MyQuickSearch.MySelectedCity });
           }
            return Json(new { MyCity = "ALL" });          

        }

        //Only members can update account
        [Authorize]
        [HttpPost]
        public JsonResult GetCurrentCityAccount(string ProfileID)
        {

           //User is already pre authed
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //retrive and save the country i think        
           MembersViewModel model = new MembersViewModel();
           model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);


            //get the most current version
           // var myProfile = membersrepo.GetProfileDataByProfileID(model.Profile.ProfileID);
           //updated to pull value from quicksearchsearch
            return Json(new { MyCity = model.MyQuickSearch.MySelectedCityStateProvince  });



        }

        //Only members can run this so no need for a guest method
        [Authorize]
        [HttpPost]
        public JsonResult GetCurrentPostalCode(string ProfileID)
        {

          string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //retrive and save the country i think
          MembersViewModel model = new MembersViewModel();          
          model =   (_ProfileID != null)?  CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID): CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


            //get the most current version from database
            //var myProfile = membersrepo.GetProfileDataByProfileID(_ProfileID);
            return Json(new { MyPostalCode = model.profiledata.PostalCode });            

        }

        //Memebers only so profileID is assumed
        [Authorize]
        [HttpPost]
        public JsonResult GetCurrentPostalCodeAccount(string ProfileID)
        {
            
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //retrive and save the country i think
            MembersViewModel model = new MembersViewModel();
           //Only members can update accoutn settings anyeays
            model =   CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);


            //get the most current version
          //  var myProfile = membersrepo.GetProfileDataByProfileID(model.Account.ZipOrPostalCode);

            return Json(new { MyPostalCode = model.Account.ZipOrPostalCode });

        }


        //get the list of postal codes for th country and city combo and pass it to the combobox
        [Authorize]
        [HttpPost]
        public JsonResult GetDefaultPostalCodeListMembersHome(string CountryName, string City)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

            //retrive and save the country i think
            MembersViewModel model = new MembersViewModel();
            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);




            //check to make sure that a country was passed in
             bool CurrentPostalCodeStatus = false;
            //retrive and save the country i think
            //MembersViewModel model = new MembersViewModel();
            // model = membersrepo.GetMembersViewModel(this.HttpContext);
             if (CountryName != null && CountryName != "Select A Country"){
                 CurrentPostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(CountryName) == 1) ? true : false;              
             }
             else
             {
               return Json( new {error = "No Country Was Selected"});            
             }
          
             
            //update the model values in case city and state are needed for filtering postal codes
            if (model.MyQuickSearch.MySelectedCityStateProvince != null && City != null  )
            {
                //update the model with new values and save, also get the geodata as well and update               
                model.MyQuickSearch.MySelectedCityStateProvince  = City;  //do away with one of these
                model.MyQuickSearch.MySelectedCity = City;
                model.MyQuickSearch.MySelectedCountryName = CountryName;
                model.MyQuickSearch.MySelectedPostalCodeStatus = CurrentPostalCodeStatus;

                //update cache             
                CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID);
                //get the postal code status of the country and city we selected

                //only send the json result if we actually have a country with a postalcode
                //if we dont sent the geo code back as json result
                if (model.MyQuickSearch.MySelectedPostalCodeStatus)                
                {
                              
                    return Json(new { postalcodes = sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0), PostalCodeStatus = model.MyQuickSearch.MySelectedPostalCodeStatus });
                    //  return Json(sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0));
                }
                else 
                {
                    //now get the gecode if the status is false
                  //  string GeCode = (model.Register.PostalCodeStatus = false) ? postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City) : "";
                  //  string GeCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City);
                    string GeCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City);               
                    return Json(new { geocode = GeCode, PostalCodeStatus = model.MyQuickSearch.MySelectedPostalCodeStatus });
                }
          
            }
              
           //we should never hit this with new caching code, quick search should always be populated               
            else
            {    
                model.MyQuickSearch.MySelectedCity = City;
                CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID);
                //only send the json result if we actually have a country with a postalcode
                //if we dont sent the geo code back as json result
                if (model.MyPostalCodeStatus == true && City != null)                
                    return Json(new { postalcodes = sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0) });
                    //  return Json(sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0));             
                
            }
            

              return Json( new {error = "No City Was Selected"});   

        
        }

        //Memebers only so profileID is assumed
        [Authorize]
        [HttpPost]
        public JsonResult GetDefaultPostalCodeListAccount(string CountryName, string City)
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //retrive and save the country i think
            MembersViewModel model = new MembersViewModel();

            model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID); //: CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);



            //update the model values in case city and state are needed for filtering postal codes
            if (model.Account  != null && City != null)
            {
                model.MyCity = City;

                

                //only send the json result if we actually have a country with a postalcode
                //if we dont sent the geo code back as json result
                if (model.Account.PostalCodeStatus)
                {

                    //update session with the new city
                    //CachingFactory.MembersViewModelHelper.Update(model, this.HttpContext);
                    return Json(new { postalcodes = sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0), PostalCodeStatus = model.Account.PostalCodeStatus });
                    //  return Json(sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0));
                }
                else
                {
                    //now get the gecode if the status is false
                    //  string GeCode = (model.Register.PostalCodeStatus = false) ? postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City) : "";


                    //string GeCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City);
                    string GeCode = "";
                    //saves the postal code into the model before update
                    //model.Account.ZipOrPostalCode = GeCode;
                    //update session with the new city
                   //  CachingFactory.MembersViewModelHelper.UpdateMemberData(model, this.HttpContext);
                    return Json(new { geocode = GeCode, PostalCodeStatus = model.Account.PostalCodeStatus });
                  
                }

            }
            else
            {
                model.MyQuickSearch.MySelectedCity = City;
                //only send the json result if we actually have a country with a postalcode
                //if we dont sent the geo code back as json result
                if (model.MyPostalCodeStatus == true && City != null)
                    return Json(new { postalcodes = sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0) });
                //  return Json(sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0));             

            }
            //update session        
             CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID );

            return Json(new object());


        }


        [HttpPost]
        public JsonResult GetDefaultPostalCodeListRegister(string CountryName, string City)
        {

             bool CurrentPostalCodeStatus = false;

            //retrive and save the country i think
            //MembersViewModel model = new MembersViewModel();
           // model = membersrepo.GetMembersViewModel(this.HttpContext);

             if (CountryName != null && CountryName != "Select A Country"){
                 CurrentPostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(CountryName) == 1) ? true : false;              
             }
             else
             {
               return Json(new object());            
             }

            //update the model values in case city and state are needed for filtering postal codes
            if (City != null)
            {
                //set geao or postacode for either
                string GeCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City);
               
                //model.Register.City   = City;
                //get postal code status
                

                //only send the json result if we actually have a country with a postalcode
                //if we dont sent the geo code back as json result
                if (CurrentPostalCodeStatus)
                {

                    //update session with the new city
                    //CachingFactory.MembersViewModelHelper.Update(model, this.HttpContext);

                    return Json(new { postalcodes = sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0),geocode = GeCode, PostalCodeStatus = CurrentPostalCodeStatus });
                    //  return Json(sharedrepo.GetFilteredPostalCodes("", CountryName, City, 0));
                }
                else
                {
                   //string GeCode = "";
                    //now get the gecode if the status is false
                    //string GeCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(CountryName, City);

                    //add the geocode to the model and update it

                 
                    
                    //saves the postal code into the model before update
                    //model.Account.ZipOrPostalCode = GeCode;
                    //update session with the new city
                    //  CachingFactory.MembersViewModelHelper.UpdateMemberData(model, this.HttpContext);

                    return Json(new { geocode = GeCode, PostalCodeStatus = CurrentPostalCodeStatus  });

                }

            }
            
              

            return Json(new object());


        }


        [HttpPost]
        public ActionResult _FilteringPostalCodesAjax(string text, int? filterMode)
        {

          string _ProfileID= CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            MembersViewModel model = new MembersViewModel();

            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items =    sharedrepo.GetFilteredPostalCodes(text, model.MyQuickSearch.MySelectedCountryName,model.MyQuickSearch.MySelectedCity, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);


            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }

        [HttpPost]
        [Authorize]
        public ActionResult _FilteringPostalCodesAjaxAccount(string text, int? filterMode)
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            MembersViewModel model = new MembersViewModel();
            
            model =   (_ProfileID != null)?  CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID): CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);

            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items = sharedrepo.GetFilteredPostalCodes(text, model.Account.Country, model.Account.City, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);


            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }


        [HttpPost]
        public ActionResult _FilteringPostalCodesAjaxRegister(string text,string country,string city, int? filterMode)
        {
            //temporary solution till we right more jquery to grab the country ID 
            //get model out of session to retirve the values needed
            //MembersViewModel model = new MembersViewModel();
           // model = membersrepo.GetMembersViewModel(this.HttpContext);

            //there always should be a selectect country name stored in session, if its a search data is null then use the countryname in model
            List<SelectListItem> items = sharedrepo.GetFilteredPostalCodes(text, country, city, 0);
            //SelectList items = sharedrepo.GetFilteredCities(text,model.MyQuickSearch.MySelectedCountryName,0);


            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = items };
        }

        #region LoadingCityItems
        //public JsonResult LoadingCityItems(ComboBoxLoadingItemsEventArgs args)
        //{

        //    //get model out of session to retirve the values needed
        //    MembersViewModel model = new MembersViewModel();
        //    if (Session["MyProfile"] != null)
        //    {
        //        model = (MembersViewModel)(Session["MyProfile"]);      
                          
        //    }
        //   // SelectList items = membersrepo.GetFilteredCities(args.Text,model.MySelectedCountryName,0);
            
        //    //JsonResult result = new JsonResult
        //    //{
        //    //    Data = new {
        //    //        Items = items
        //    //    },
        //    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    //};

        //   
        //}
        #endregion


        #region ApiserverEvents_SelectedIndexChanged

        //public ActionResult CountryCombobox_SelectedIndexChanged(string ComboBox1, string ComboBox1_SelectedText)
        //{
        //   ViewData["ComboBox1"] = membersrepo.CountrySelectList();
        //    //update the model with the new coutnry selected
        //    //verufy its not just a page referesh attemt to get model from view data

        //    MembersViewModel model = new MembersViewModel();
        //    if (Session["MyProfile"] != null)
        //    {
        //        model = (MembersViewModel)(Session["MyProfile"]);
        //        // Save MyMatches for global in quick search for now use
        //        model.MySelectedCountryName = ComboBox1_SelectedText;
        //        //clear our the current city as well and zip etc
        //        model.MySelectedCity = "";
        //        //resave the model in session
        //        Session["MyProfile"] = model;
        //    }
        //    return RedirectToAction("MembersHome", "Members");
        //}


        //public ActionResult City_SelectedIndexChanged(string ComboBox2, string ComboBox2_SelectedText)
        //{
        //   //ViewData["ComboBox2"] = membersrepo.CountrySelectList();
        //    string [] temp = ComboBox2_SelectedText.Split(',');
            
        //    //update the model with the new coutnry selected
        //    //verufy its not just a page referesh attemt to get model from view data

        //    MembersViewModel model = new MembersViewModel();
        //    if (Session["MyProfile"] != null)
        //    {
        //        model = (MembersViewModel)(Session["MyProfile"]);
        //        // Save MyMatches for global in quick search for now use
        //       // model.MySelectedCountry = ComboBox2_SelectedText;
        //        model.MySelectedCity = temp[0];                          
        //       // model.MySelectedStateProvince = temp[1];
        //        //resave the model in session
        //        Session["MyProfile"] = model;
        //    }
        //    return RedirectToAction("MembersHome", "Members");
        //}


        //public ActionResult MyGender_SelectedIndexChanged(string ComboBox2, string ComboBox2_SelectedText)
        //{
        //    //ViewData["ComboBox2"] = membersrepo.CountrySelectList();
        //    string[] temp = ComboBox2_SelectedText.Split(',');

        //    //update the model with the new coutnry selected
        //    //verufy its not just a page referesh attemt to get model from view data

        //    MembersViewModel model = new MembersViewModel();
        //    if (Session["MyProfile"] != null)
        //    {
        //        model = (MembersViewModel)(Session["MyProfile"]);
        //        // Save MyMatches for global in quick search for now use
        //        // model.MySelectedCountry = ComboBox2_SelectedText;
        //        model.MySelectedCity = temp[0];
        //     //   model.MySelectedStateProvince = temp[1];
        //        //resave the model in session
        //        Session["MyProfile"] = model;
        //    }
        //    return RedirectToAction("MembersHome", "Members");
        //}



        #endregion


    }
}
