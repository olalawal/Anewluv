using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Shell.MVC2.Models;

using Shell.MVC2.Filters;

using System.Security.Principal;


using System.IO;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;



using MvcContrib.Filters;
using MvcContrib;

using System.Data;
using System.Data.Entity;


using Shell.MVC2.AppFabric;

namespace Shell.MVC2.Controllers
{
    public class EditProfileController : Controller
    {
        private AnewLuvFTSEntities _db;
        private MembersRepository _membersrepository;
        private EditProfileRepository _editProfileRepository;
       private SharedRepository _sharedRepository;

     public   EditProfileController(AnewLuvFTSEntities db,
     MembersRepository membersrepository,
      EditProfileRepository editProfileRepository,     
    SharedRepository sharedRepository)
       {
           _db = db;
           _membersrepository = membersrepository;
           _editProfileRepository = editProfileRepository;         
           _sharedRepository = sharedRepository;

       }

        // GET: /Account/EditAccount
        [Authorize]
        public ActionResult EditProfile()
        {
           string _ProfileId = AppFabric.CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //10-26-2011 Ola lawal
            //added code to get the photos since we display them here now
            EditProfilePhotosViewModel model = new EditProfilePhotosViewModel();
           // if (model.IsUploading == true)
            //    return View(model);

            int page = 1;
            int ps = 12;
            var MyPhotos = _editProfileRepository.MyPhotos(this.HttpContext.User.Identity.Name);

            // Retrieve updated single photo profile by PhotoId           
            var Approved = _editProfileRepository.GetApproved(MyPhotos, "Yes", page, ps);
            var NotApproved = _editProfileRepository.GetApproved(MyPhotos, "No", page, ps);
            var Private = _editProfileRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
            model = _editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
          
            //add this model temporarily to the members view mode; (not save it though)
            MembersViewModel membersmodel = new MembersViewModel();
            //load model from app fabric
            membersmodel = AppFabric.CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileId);
            membersmodel.MyEditProfilePhotosViewModel = model; //add photo viewmodel

            return View(membersmodel);
        }

        //New logics as of 11/19/2011
        // added a full edit flag to the calling of all the methods so we can determine if the user is doing a full edit or not 
        //also fixed saving bugs, added ability to go to next pag on save with full edit flag set to true

        //Logic here is load the inital view for edit profile which is the first page and not a partial view
        //When they hit next or clicked on
        [HttpGet]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        public  ActionResult EditProfileBasicSettings(int? ViewIndex, bool? fulledit)
        {

            //View index determines what view we are looking at 
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

            EditProfileSettingsViewModel ViewModel = new EditProfileSettingsViewModel();         
            //populate model
            // EditProfileRepository editprofilerepo = new EditProfileRepository();
            // get an update the members view model to synch it up with any changes in the database beofre this view           
            var mapper = new ViewModelMapper();
            var membersmodel = mapper.MapMember(_ProfileID);
            //get users session data
            //TO DO change this to a new query against user name (DONE)

           // membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));
         
            //requery the profileData
            //do it in one query 
          //  membersmodel.profiledata = _membersrepository.GetProfileDataByProfileID(_ProfileID);
            //update the saved model in ram
            CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID);
           
            //changed this to get the value from the database not from the session in case we forgot to update it
            EditProfileBasicSettingsModel model = new EditProfileBasicSettingsModel(membersmodel.profiledata);
            
                   
            
            //setup the searchModel part
           // membersmodel.profiledata.SearchSettings.Add(_membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID));
           
            // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
            //rebuild the values that were not passed using the shared repo
            //get the search setting table for MyMatches
            //SearchSetting searchmodel = _db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();
            // SearchSetting searchmodel = _db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();           
            SearchModelBasicSettings searchmodelbasicsettings = new SearchModelBasicSettings(membersmodel.profiledata.SearchSettings.FirstOrDefault());

   
         
                               
            ViewData["Status"] = "Describe what makes up you. What makes you one of a kind, incomparable, unrivaled. What are you looking for in" +
            "that special person? What makes your heart pound with excitement? What qualities you feel best compliment your personality?";

            //combine the models to the viewmodel
            ViewModel.BasicProfileSettings = model;
            ViewModel.BasicSearchSettings = searchmodelbasicsettings;
            //added to test for full editing 
           ViewModel.IsFullEditing = fulledit == null ? true : fulledit.GetValueOrDefault();
            ViewModel.ViewIndex = ViewIndex.GetValueOrDefault();

                        
            return View(ViewModel);
        }

      
       
 
        [PassParametersDuringRedirect]
        [HttpPost]
        public  ActionResult EditProfileBasicSettings( EditProfileSettingsViewModel   model,
            int? ViewIndex, FormCollection formCollection, int?[] SelectedGenderIds, int?[] SelectedShowMeIds, int?[] SelectedSortByIds, bool? fulledit)
        {
            //reset viewmodel errors for now
            model.CurrentErrors.Clear();

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);            
          
            //TOD DO move this to a function i think or repository for search and have it populate those there
            //repopulate checkboxes for postback
            //model.BasicSearchSettings = new SearchModelBasicSettings(membersmodel.profiledata.SearchSettings.FirstOrDefault());

        

           
             if (ModelState.IsValid != true)
                {
                    //restore the model state
                    return View(model);
                }

            //TO do move this to edit profile reposotiory 
            //i.e separate code to update each page
            //conitional validation needed - i.e check what page we are on 

            if (ViewIndex == Constants.BasicSettingsPage1 )
            {
                model = _editProfileRepository.EditProfileBasicSettingsPage1Update(model, formCollection, SelectedGenderIds,
                    _ProfileID);

              

                if (model.CurrentErrors.Count() > 0 )
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                    ModelState.AddModelError("", error);
                    }   
                    return View(model);
                }
                //no errors here
                ViewData["EditProfileBasicSettingsStatus"] = "Your Profile has been Succesfully updated!";


                //redirect them to next page test code should work
           if (fulledit.GetValueOrDefault() == true)   return RedirectToAction("EditProfileBasicSettings","EditProfile", new { ViewIndex = Constants.BasicSettingsPage2,fulledit = true });
                
            }

            if (ViewIndex == Constants.BasicSettingsPage2 )
            {
                model = _editProfileRepository.EditProfileBasicSettingsPage2Update(model, formCollection,SelectedShowMeIds,SelectedSortByIds ,
                    _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileBasicSettingsStatus"] = "Your Profile has been Succesfully updated!"; 
           
                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileAppearanceSettings", "EditProfile", new { ViewIndex = Constants.AppearanceSettingsPage1, fulledit = true });


            }
            return View(model);

        }




        //Logic here is load the inital view for edit profile which is the first page and not a partial view
        //When they hit next or clicked on
        [HttpGet]
        [ModelStateToTempData]      
        [PassParametersDuringRedirect]
        public ActionResult EditProfileAppearanceSettings(int? ViewIndex, bool? fulledit)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //View index determines what view we are looking at 
            EditProfileSettingsViewModel ViewModel = new EditProfileSettingsViewModel();
            //populate model
            // get an update the members view model to synch it up with any changes in the database beofre this view           
            var mapper = new ViewModelMapper();
            var membersmodel = mapper.MapMember(_ProfileID);

            //get users session data
            //TO DO change this to a new query against user name (DONE)
            //membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));

            //requery the profileData
            //do it in one query 
            //membersmodel.profiledata = _membersrepository.GetProfileDataByProfileID(_ProfileID);
            //update the saved model in ram
            CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID);
            //changed this to get the value from the database not from the session in case we forgot to update it
            EditProfileAppearanceSettingsModel model = new EditProfileAppearanceSettingsModel(membersmodel.profiledata);

            //setup the searchModel part
            // membersmodel.profiledata.SearchSettings.Add(_membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID));
            //SearchSetting searchmodel = _db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();
            SearchModelAppearanceSettings searchmodelapperancesettings = new SearchModelAppearanceSettings(membersmodel.profiledata.SearchSettings.FirstOrDefault());

   
          //  SearchModelAppearanceSettings apperancesearchsettings = new SearchModelAppearanceSettings(searchmodel);
                        
            ViewData["Status"] = "Describe what makes up you. What makes you one of a kind, incomparable, unrivaled. What are you looking for in" +
            "that special person? What makes your heart pound with excitement? What qualities you feel best compliment your personality?";

            //combine the models to the viewmodel
            ViewModel.AppearanceSettings  = model;
            ViewModel.AppearanceSearchSettings = searchmodelapperancesettings;
            ViewModel.IsFullEditing = fulledit == null ? true : fulledit.GetValueOrDefault();
            ViewModel.ViewIndex = ViewIndex.GetValueOrDefault();

            return View(ViewModel);
        }


        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        
        public ActionResult EditProfileAppearanceSettings(EditProfileSettingsViewModel model,
            int? ViewIndex, FormCollection formCollection, int?[] SelectedYourBodyTypesIds,
            int?[] SelectedYourEthnicityIds,int?[] SelectedMyEthnicityIds,int?[] SelectedYourEyeColorIds,
            int?[] SelectedYourHairColorIds,int?[] SelectedYourHotFeatureIds, int?[] SelectedMyHotFeatureIds,bool? fulledit)
        {

            //reset viewmodel errors for now
            model.CurrentErrors.Clear();
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //repopulate checkboxes for postback

                        


             if (ModelState.IsValid != true)
                {
                    //restore the model state
                    return View(model);
                }


            if (ViewIndex == Constants.AppearanceSettingsPage1)
            {
                model = _editProfileRepository.EditProfileAppearanceSettingsPage1Update(model, formCollection,
                    SelectedYourBodyTypesIds,_ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();
               
                if (model.CurrentErrors.Count() > 0 )
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                    ModelState.AddModelError("", error);
                    }   
                    return View(model);
                }
                //no errors here
                ViewData["EditProfileAppearanceStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileAppearanceSettings", "EditProfile", new { ViewIndex = Constants.AppearanceSettingsPage2, fulledit = true });

                
            }

            if (ViewIndex == Constants.AppearanceSettingsPage2 )
            {
                model = _editProfileRepository.EditProfileAppearanceSettingsPage2Update (model, formCollection,
                    SelectedYourEthnicityIds,SelectedMyEthnicityIds,_ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileAppearanceStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileAppearanceSettings", "EditProfile", new { ViewIndex = Constants.AppearanceSettingsPage3, fulledit = true });

             
            }

              if (ViewIndex == Constants.AppearanceSettingsPage3 )
            {
                model = _editProfileRepository.EditProfileAppearanceSettingsPage3Update (model, formCollection,SelectedYourEyeColorIds
                    ,SelectedYourHairColorIds ,  _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileAppearanceStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileAppearanceSettings", "EditProfile", new { ViewIndex = Constants.AppearanceSettingsPage4, fulledit = true });

            }


              if (ViewIndex == Constants.AppearanceSettingsPage4 )
            {
                model = _editProfileRepository.EditProfileAppearanceSettingsPage4Update (model, formCollection,
                    SelectedYourHotFeatureIds,SelectedMyHotFeatureIds,_ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileAppearanceStatus"] = "Your Profile has been Succesfully updated!";


                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileLifeStyleSettings", "EditProfile", new { ViewIndex = Constants.LifeStyleSettingsPage1, fulledit = true });

            }





            return View(model);

            
         }



        //Lifestyle views are activated here for edit profile
        //Logic here is load the inital view for edit profile which is the first page and not a partial view
        //When they hit next or clicked on
        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult EditProfileLifeStyleSettings(int? ViewIndex, bool? fulledit)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

            //View index determines what view we are looking at 
            EditProfileSettingsViewModel ViewModel = new EditProfileSettingsViewModel();
            //populate model
            // EditProfileRepository editprofilerepo = new EditProfileRepository();
            var mapper = new ViewModelMapper();
            var membersmodel = mapper.MapMember(_ProfileID);


            //get users session data
            //TO DO change this to a new query against user name (DONE)
           // membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));

            //requery the profileData
            //do it in one query 
            //membersmodel.profiledata = _membersrepository.GetProfileDataByProfileID(membersmodel.profiledata.ProfileID);
            //update the saved model in ram
            CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID);

            //changed this to get the value from the database not from the session in case we forgot to update it
            EditProfileLifeStyleSettingsModel model = new EditProfileLifeStyleSettingsModel(membersmodel.profiledata);

            //setup the searchModel part
           // membersmodel.profiledata.SearchSettings.Add(_membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID));
           // SearchSetting searchmodel = _db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();           
            SearchModelLifeStyleSettings searchmodellifestylesettings = new SearchModelLifeStyleSettings(membersmodel.profiledata.SearchSettings.FirstOrDefault());

            ViewData["Status"] = "Describe what makes up you. What makes you one of a kind, incomparable, unrivaled. What are you looking for in" +
            "that special person? What makes your heart pound with excitement? What qualities you feel best compliment your personality?";

            //combine the models to the viewmodel
            ViewModel.LifeStyleSettings = model;
            ViewModel.LifeStyleSearchSettings = searchmodellifestylesettings;
            ViewModel.IsFullEditing = fulledit == null ? true : fulledit.GetValueOrDefault();
            ViewModel.ViewIndex =    ViewIndex.GetValueOrDefault();

            return View(ViewModel);
        }


        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        public ActionResult EditProfileLifeStyleSettings(EditProfileSettingsViewModel model,
            int? ViewIndex, FormCollection formCollection, int?[] SelectedYourMaritalStatusIds,
            int?[] SelectedYourLivingSituationIds,
            int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, 
            int?[] SelectedYourHaveKidsIds, int?[] SelectedYourWantsKidsIds, 
            int?[] SelectedYourEmploymentStatusIds, int?[] SelectedYourIncomeLevelIds,
            int?[] SelectedYourEducationLevelIds, int?[] SelectedYourProfessionIds,
            bool? fulledit)
        {

            //reset viewmodel errors for now
            model.CurrentErrors.Clear();
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //repopulate checkboxes for postback



            if (ModelState.IsValid != true)
            {
                //restore the model state
                return View(model);
            }


            if (ViewIndex == Constants.LifeStyleSettingsPage1)
            {
                model = _editProfileRepository.EditProfileLifeStyleSettingsPage1Update(model, formCollection,
                    SelectedYourMaritalStatusIds,  SelectedYourLivingSituationIds,
             SelectedYourLookingForIds,  SelectedMyLookingForIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();


                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                //no errors here
                ViewData["EditProfileLifeStyleStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileLifeStyleSettings", "EditProfile", new { ViewIndex = Constants.LifeStyleSettingsPage2, fulledit = true });


            }

            if (ViewIndex == Constants.LifeStyleSettingsPage2)
            {
                model = _editProfileRepository.EditProfileLifeStyleSettingsPage2Update(model, formCollection,
                    SelectedYourHaveKidsIds, SelectedYourWantsKidsIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileLifeStyleStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileLifeStyleSettings", "EditProfile", new { ViewIndex = Constants.LifeStyleSettingsPage3, fulledit = true });

                //else go back to page we just edited so add view

            }

            if (ViewIndex == Constants.LifeStyleSettingsPage3)
            {
                model = _editProfileRepository.EditProfileLifeStyleSettingsPage3Update(model, formCollection,
                      SelectedYourEmploymentStatusIds,  SelectedYourIncomeLevelIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();


                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileLifeStyleStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileLifeStyleSettings", "EditProfile", new { ViewIndex = Constants.LifeStyleSettingsPage4, fulledit = true });

            }


            if (ViewIndex == Constants.LifeStyleSettingsPage4)
            {
                model = _editProfileRepository.EditProfileLifeStyleSettingsPage4Update(model, formCollection,
                  SelectedYourEducationLevelIds,  SelectedYourProfessionIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileLifeStyleStatus"] = "Your Profile has been Succesfully updated!";


                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileCharacterSettings", "EditProfile", new { ViewIndex = Constants.CharacterSettingsPage1 , fulledit = true });

            }





            return View(model);


        }




        //Logic here is load the inital view for edit profile which is the first page and not a partial view
        //When they hit next or clicked on
        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult EditProfileCharacterSettings(int? ViewIndex, bool? fulledit)
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

            //View index determines what view we are looking at 
            EditProfileSettingsViewModel ViewModel = new EditProfileSettingsViewModel();
            //populate model
            // EditProfileRepository editprofilerepo = new EditProfileRepository();
            var mapper = new ViewModelMapper();
            var membersmodel = mapper.MapMember(_ProfileID);

            //get users session data
            //TO DO change this to a new query against user name (DONE)
          //  membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));

            //requery the profileData
            //do it in one query 
           // membersmodel.profiledata = _membersrepository.GetProfileDataByProfileID(membersmodel.profiledata.ProfileID);
            //update the saved model in ram
            CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID);

            //changed this to get the value from the database not from the session in case we forgot to update it
            EditProfileCharacterSettingsModel model = new EditProfileCharacterSettingsModel(membersmodel.profiledata);

            //setup the searchModel part
           // membersmodel.profiledata.SearchSettings.Add(_membersrepository.GetPerFectMatchSearchSettingsByProfileID(membersmodel.profiledata.ProfileID));

            SearchModelCharacterSettings searchmodelcharactersettings = new SearchModelCharacterSettings(membersmodel.profiledata.SearchSettings.FirstOrDefault());

            ViewData["Status"] = "Describe what makes up you. What makes you one of a kind, incomparable, unrivaled. What are you looking for in" +
            "that special person? What makes your heart pound with excitement? What qualities you feel best compliment your personality?";

            //combine the models to the viewmodel
            ViewModel.CharacterSettings = model;
            ViewModel.CharacterSearchSettings = searchmodelcharactersettings;
            ViewModel.IsFullEditing = fulledit == null ? true : fulledit.GetValueOrDefault();
            ViewModel.ViewIndex = ViewIndex.GetValueOrDefault();

            return View(ViewModel);
        }


        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        public ActionResult EditProfileCharacterSettings(EditProfileSettingsViewModel model,
            int? ViewIndex, FormCollection formCollection, int?[] SelectedYourDietIds, int?[] SelectedYourDrinksIds,
           int?[] SelectedYourExerciseIds,int?[] SelectedYourSmokesIds,int?[] SelectedYourHobbyIds,
           int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds, int?[] SelectedYourReligionIds,
            int?[] SelectedYourReligiousAttendanceIds, int?[] SelectedYourPoliticalViewIds,
           int?[] SelectedYourHumorIds, bool? fulledit)
        {

            //reset viewmodel errors for now
            model.CurrentErrors.Clear();
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //repopulate checkboxes for postback

      

            if (ModelState.IsValid != true)
            {
                //restore the model state
                return View(model);
            }


            if (ViewIndex == Constants.CharacterSettingsPage1)
            {
                model = _editProfileRepository.EditProfileCharacterSettingsPage1Update(model, formCollection,
                    SelectedYourDietIds, SelectedYourDrinksIds,
            SelectedYourExerciseIds, SelectedYourSmokesIds, _ProfileID);
                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();


                if (model.CurrentErrors.Count() > 0)

                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                //no errors here
                ViewData["EditProfileCharacterStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileCharacterSettings", "EditProfile", new { ViewIndex = Constants.CharacterSettingsPage2, fulledit = true });


            }

            if (ViewIndex == Constants.CharacterSettingsPage2)
            {
                model = _editProfileRepository.EditProfileCharacterSettingsPage2Update(model, formCollection,
                    SelectedYourHobbyIds, SelectedMyHobbyIds, SelectedYourSignIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();

                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileCharacterStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileCharacterSettings", "EditProfile", new { ViewIndex = Constants.CharacterSettingsPage3, fulledit = true });


            }

            if (ViewIndex == Constants.CharacterSettingsPage3)
            {
                model = _editProfileRepository.EditProfileCharacterSettingsPage3Update(model, formCollection, 
                    SelectedYourReligionIds,
                     SelectedYourReligiousAttendanceIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();


                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileCharacterStatus"] = "Your Profile has been Succesfully updated!";

                //redirect them to next page test code should work
                if (fulledit.GetValueOrDefault() == true) return RedirectToAction("EditProfileCharacterSettings", "EditProfile", new { ViewIndex = Constants.CharacterSettingsPage4, fulledit = true });

            }


            if (ViewIndex == Constants.CharacterSettingsPage4)
            {
                model = _editProfileRepository.EditProfileCharacterSettingsPage4Update(model, formCollection,
                     SelectedYourPoliticalViewIds,  SelectedYourHumorIds, _ProfileID);

                //reset viewindex
                model.ViewIndex = ViewIndex.GetValueOrDefault();
                if (model.CurrentErrors.Count() > 0)
                {
                    //build the errors list'
                    foreach (string error in model.CurrentErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }
                ViewData["EditProfileCharacterStatus"] = "Your Profile has been Succesfully updated!";



            }

            //TO do show jquery popop redirecting to advanced search or home page



            return View(model);


        }



        [HttpPost]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult EditProfileQuickStats( EditProfileBasicSettingsModel model)
        {

            //This model error thing is to handle the   
            // TempData["ModelErrors"] = "True";
            return View("EditProfile");
        }


        //Logic here is load the inital view for edit profile which is the first page and not a partial view
        //When they hit next or clicked on
             [Authorize]
        [HttpGet]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        public ActionResult EditProfileVisibilitySettings(string ProfileID)
        {
            var _profileID = (ProfileID == null)?  CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name): ProfileID; 
            //View index determines what view we are looking at 


     
            MembersViewModel membersmodel = new MembersViewModel();

           var model = new ProfileVisiblitySetting();

            //get users session data
            //TO DO change this to a new query against user name (DONE)

            membersmodel =  CachingFactory.MembersViewModelHelper.GetMemberData(_profileID);
//dpnt get this data in disconnectd mode
#if !DISCONECTED
              model = _membersrepository.GetProfileVisibilitySettingsByProfileID(_profileID);
#endif

            //populate model
            // EditProfileRepository editprofilerepo = new EditProfileRepository();

            //requery the profileData
            //do it in one query 
           // membersmodel.profiledata = _membersrepository.GetProfileDataByProfileID(membersmodel.profiledata.ProfileID);
            //update the saved model in ram
           // CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.profiledata.ProfileID);

           //get default settings for visibility settings , this is called after document ready so we are good.
           // EditProfileVisibilitySettingsModel model = new EditProfileVisibilitySettingsModel();
            //set defaults 

            //Profile visiblity should always be set if the user has changed these settings , otherwise just use the defauts
            if (model == null)
                model = new ProfileVisiblitySetting
            {
               AgeMaxVisibility = -1,  AgeMinVisibility  = -1, ChatVisiblityToInterests = true,  ChatVisiblityToLikes  = true,
                ChatVisiblityToMatches  = true,   ChatVisiblityToPeeks   = true, ChatVisiblityToSearch  = true,
             CountryID  = -1 , GenderID   = -1, LastUpdateDate   = DateTime.Now,MailChatRequest =true, MailIntrests  =true ,
                     MailLikes = true, MailNews = true,  MailPeeks = true , ProfileVisiblity  = true,  SaveOfflineChat   = false ,
                         SteathPeeks   = true ,ProfileID =_profileID            
             };


            ViewData["Status"] = "Edit your Visibility Settings, as well as what types of Activity Updates you want to recive from AnewLuv.com";




            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        public ActionResult EditProfileVisibilitySettings(ProfileVisiblitySetting model)
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //create a model to store the selected country and other persient values as well
            MembersViewModel Returnmodel = new MembersViewModel();
            //map the Register default values             
            //Returnmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);
            //update the account peice of members view model wih the account data posted back
            Returnmodel.ProfileVisiblity = model;

           


            //if model state is valid hanlde save
            //TO DO add saving logic to editprocidle model
            if (ModelState.IsValid)
            {



                _editProfileRepository.UpdateProfileVisibilitySettings(model);


               // MembershipService.UpdateUser(u);



                ViewData["Status"] = "Your Visibility Settings have been updated !";

                return View(Returnmodel);


            }
            else
            {
                ModelState.AddModelError("", "Please fix your errors");
                return View(Returnmodel);
            }
        }

 
        #region "Photo Code"

        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult PhotoEditQuick(EditProfilePhotosViewModel model)
        {
            if (model.IsUploading == true)
                return View(model);

            int page = 1;
            int ps = 12;
            var MyPhotos = _editProfileRepository.MyPhotos(this.HttpContext.User.Identity.Name);

            var Approved = _editProfileRepository.GetApproved(MyPhotos, "Yes", page, ps);
            var NotApproved = _editProfileRepository.GetApproved(MyPhotos, "No", page, ps);
            var Private = _editProfileRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);

            model = _editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
            return View(model);

        }

        [HttpGet]
        public ActionResult PhotoEditView(Guid photoid)
        {
            //var src = s.GetAll().Where(o => o.Name != null && o.Name.ToLower().Contains(name.ToLower()));
            //var rows = this.RenderView("rows", src.Skip((page - 1) * ps).Take(ps));           
            //return Json(new {rows, more = src.Count() > page * ps });


            // Retrieve updated single photo profile by PhotoId
            EditProfilePhotoModel Photo = _editProfileRepository.GetSingleProfilePhotoByphotoID(photoid);
            var src = Photo;

            return View("PhotoEditView", src);
            //return PartialView("PhotoEditView", src);
            // return Json(src);
        }

        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "UpdatePhotoAction", Value = "Update")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult EditProfile_UpdatePhotos(EditProfilePhotoModel Photo1)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value

            if (ModelState.IsValid)
            {
                // Retrieve single value from photos table
                photo PhotoModify = _db.photos.Single(p => p.PhotoID == Photo1.PhotoID);

                #region "Verify Approved Gallery Exists"


                /// <summary>
                /// Determines whether an approved gallery image exists and auto changes the value of ProfileImageType accordingly.
                /// </summary>
                /// 

                // Update photo.ProfileImageType value based on the value of the checkbox


                if (Photo1.Aproved == "Yes")
                {
                    if (Photo1.checkedPrimary == true)
                    {
                        Photo1.ProfileImageType = "Gallery";
                    }
                    else
                    {
                        Photo1.ProfileImageType = "NoStatus";
                    } // Changes ProfileImageType from "Nostatus" to "Gallery" 

                }
                else { Photo1.ProfileImageType = "NoStatus"; } // Keeps ProfileImageType = "NoStatus"


                #endregion

                // Update or attach values from AdminPhoto to PhotoModify model
                PhotoModify.ProfileImageType = Photo1.ProfileImageType;
                PhotoModify.ImageCaption = Photo1.ImageCaption;


                // _db.photos.Attach(PhotoModify); already attached
                // Update database
                _db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                _db.SaveChanges();

                // Retrieve updated single photo profile by PhotoId
                EditProfilePhotoModel src = _editProfileRepository.GetSingleProfilePhotoByphotoID(PhotoModify.PhotoID);
                var models = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                    "Yes", "No", PhotoStatusID, page, ps);
                models.SingleProfilePhoto = src;


                //ModelState.Clear();
                return View(models);

            }
            else
            {

                return this.View();


            }

        }

        #region "Photo Tabs"

        // **************************************
        // URL: /PhotoEditQuick
        // **************************************

        //this handles the photo tab data loads. 


        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "NotApprovedDelete", Value = "Delete Selected Values")]
        public ActionResult IndexPost(Guid[] deleteInputs)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            var model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);


            if (deleteInputs == null)
            {

                ModelState.AddModelError("NotApprovedError", "None of the reconds has been selected for delete action !");
                return View(model);
            }

            foreach (var item in deleteInputs)
            {

                try
                {
                    //return Content("not approved deleted");
                    _editProfileRepository.DeletedUserPhoto(item);
                    model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

                }
                catch (Exception err)
                {

                    ModelState.AddModelError("NotApprovedError", "Failed On Id " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

            // messageRepository.Save();
            ModelState.AddModelError("NotApprovedError", deleteInputs.Count().ToString() + " record(s) has been successfully deleted!");

            return View(model);
        }


        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "PrivateDelete", Value = "Delete Selected Values")]
        public ActionResult PhotoPrivatePost_Delete(Guid[] privateInputs)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            var model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);


            if (privateInputs == null)
            {

                ModelState.AddModelError("PrivateError", "None of the reconds has been selected for delete action !");
                return View(model);
            }

            foreach (var item in privateInputs)
            {

                try
                {

                    //return Content("private deleted");
                    _editProfileRepository.DeletedUserPhoto(item);
                    model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

                }
                catch (Exception err)
                {

                    ModelState.AddModelError("PrivateError", "Failed On Id " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

            // messageRepository.Save();
            ModelState.AddModelError("PrivateError", privateInputs.Count().ToString() + " record(s) has been successfully deleted!");

            return View(model);
        }

        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "PrivatePublic", Value = "Make Public")]
        public ActionResult PhotoPrivatePost_MakePublic(Guid[] privateInputs)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            var model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);


            if (privateInputs == null)
            {

                ModelState.AddModelError("PrivateError", "None of the reconds has been selected for public photo move !");
                return View(model);
            }

            foreach (var item in privateInputs)
            {

                try
                {

                    //return Content("private deleted");
                    _editProfileRepository.MakeUserPhoto_Public(item);
                    model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

                }
                catch (Exception err)
                {

                    ModelState.AddModelError("PrivateError", "Failed On Id " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

            // messageRepository.Save();
            ModelState.AddModelError("PrivateError", privateInputs.Count().ToString() + " record(s) has been successfully moved!");

            return View(model);
        }

        
        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "PublicDelete", Value = "Delete Selected Values")]
        public ActionResult PhotoPublicPost_Delete(Guid[] publicInputs)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            var model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);


            if (publicInputs == null)
            {

                ModelState.AddModelError("PublicError", "None of the reconds has been selected for delete action !");
                return View(model);
            }

            foreach (var item in publicInputs)
            {

                try
                {

                    //return Content("private deleted");
                    _editProfileRepository.DeletedUserPhoto(item);
                    model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

                }
                catch (Exception err)
                {

                    ModelState.AddModelError("PublicError", "Failed On Id " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

            // messageRepository.Save();
            ModelState.AddModelError("PublicError", publicInputs.Count().ToString() + " record(s) has been successfully deleted!");

            return View(model);
        }

        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "PublicPrivate", Value = "Make Private")]
        public ActionResult PhotoPublicPost_MakePublic(Guid[] publicInputs)
        {

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            var model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);


            if (publicInputs == null)
            {

                ModelState.AddModelError("PublicError", "None of the reconds has been selected for private photo move !");
                return View(model);
            }

            foreach (var item in publicInputs)
            {

                try
                {

                    //return Content("private deleted");
                    _editProfileRepository.MakeUserPhoto_Private(item);
                    model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

                }
                catch (Exception err)
                {

                    ModelState.AddModelError("PublicError", "Failed On Id " + item.ToString() + " : " + err.Message);
                    return View(model);
                }
            }

            // messageRepository.Save();
            ModelState.AddModelError("PublicError", publicInputs.Count().ToString() + " record(s) has been successfully moved!");

            return View(model);
        }

        #endregion

        #region "Upload Photos"

        // **************************************
        // URL: /PhotoEditQuick
        // **************************************

        //this handles the photo tab data loads. 

        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "ShowUploadButton", Value = "Add New Photo")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult ShowPhotoUpload(EditProfilePhotosViewModel model)
        {

            //var MyPhotos = _editProfileRepository.MyPhotos(this.HttpContext.User.Identity.Name);

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

            model.IsUploading = true;

            return View(model);
        }


        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "Uploadbutton", Value = "submit")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult EditUploadPhoto(PhotoViewModel model)
        {

            var membersmodel = new MembersViewModel();

            membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));


            EditProfilePhotosViewModel _model = new EditProfilePhotosViewModel();

            //check if any photo's exist in either the photoviewmodel or the membersmodel 
            if (ModelState.IsValid && membersmodel.MyPhotos != null)
            {

                foreach (Photo photo in membersmodel.MyPhotos)
                {
                    model.AddPhoto(photo, membersmodel.Profile.ProfileID);
                }

                //send  the emails for photos uploaded out
                var Email = new EmailModel();

                Email.ProfileID = membersmodel.Profile.ProfileID;
                Email.UserName = membersmodel.Profile.UserName;
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfilePhotoUploaded);
                localEmailService.SendEmailMessage(Email);
                //emails are sent yay

                ModelState.Clear();


                int page = 1;
                int ps = 12;
                int PhotoStatusID = 3; // Private photostatusId value
                _model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                    "Yes", "No", PhotoStatusID, page, ps);

                _model.IsUploading = false;

                //return View(model);
                return this.RedirectToAction(c => c.PhotoEditQuick(_model));
                //return RedirectToAction("PhotoEditQuick", "EditProfile");
                //return Content("works - model is valid");

            }
            else
            {
                ModelState.AddModelError("UploadPhotoError", "No photos were selected! , please select a photo to continue");

                int page = 1;
                int ps = 12;
                int PhotoStatusID = 3; // Private photostatusId value
                _model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                    "Yes", "No", PhotoStatusID, page, ps);

                _model.IsUploading = true;

                return this.RedirectToAction(c => c.PhotoEditQuick(_model));

                // return Content("works - model is not valid valid");

                // return RedirectToAction("PhotoEditQuick", "EditProfile");

            }

        }

        [HttpPost, ActionName("PhotoEditQuick")]
        [AcceptParameter(Name = "CancelUploadbutton", Value = "cancel")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult CancelUploadPhoto(PhotoViewModel model)
        {
            var membersmodel = new MembersViewModel();

            membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));

            // Clear any existing previous model values
            membersmodel.MyPhotos = null;


            ModelState.Clear();
            EditProfilePhotosViewModel _model = new EditProfilePhotosViewModel();

            int page = 1;
            int ps = 12;
            int PhotoStatusID = 3; // Private photostatusId value
            _model = _editProfileRepository.GetEditPhotoModel(this.HttpContext.User.Identity.Name,
                                                                "Yes", "No", PhotoStatusID, page, ps);

            _model.IsUploading = false;

            //return View(model);
            return this.RedirectToAction(c => c.PhotoEditQuick(_model));

            //return RedirectToAction("PhotoEditQuick", "EditProfile");

        }

        #endregion

        #endregion

    }
}
