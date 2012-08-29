using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

////using RiaServicesContrib.Mvc;
////using RiaServicesContrib.Mvc.Services;

using System.Text;

using System.Web.Mvc;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Linq.Expressions;

namespace Shell.MVC2.Models
{
    public class ProfileRepository
    {




        //TO DO
        //Get these initalized
        DatingService datingservicecontext = new DatingService().Initialize();
        PostalDataService postaldataservicecontext = new PostalDataService().Initialize();


        AnewLuvFTSEntities db = new AnewLuvFTSEntities();
        PostalData2Entities postaldb = new PostalData2Entities();


        public ProfileBrowseModel GetProfileBrowseModelGuest(string ProfileId)
        {

            var editprofilerepo = new EditProfileRepository();


            var NewProfileBrowseModel = new ProfileBrowseModel
            {

                ViewerProfileDetails = null,
                ProfileDetails = new MemberSearchViewModel(ProfileId)


            };

            //add in the ProfileCritera
            NewProfileBrowseModel.ProfileCriteria = new ProfileCriteriaModel(NewProfileBrowseModel.ProfileDetails);
            NewProfileBrowseModel.ViewerProfileCriteria = new ProfileCriteriaModel(null);




            return NewProfileBrowseModel;

        }

        //public ProfileBrowseModel GetProfileBrowseModelMembers(string ProfileId,MembersViewModel model)
        //{

        //    var editprofilerepo = new EditProfileRepository();


        //    var NewProfileBrowseModel = new ProfileBrowseModel
        //    {
        //        //TO Do user a mapper instead of a contructur and map it from the service
        //        //Move all this to a service
        //        ViewerProfileDetails = new MemberSearchViewModel(model),
        //        ProfileDetails = new MemberSearchViewModel(ProfileId  )



        //    };

        //    //add in the ProfileCritera
        //    NewProfileBrowseModel.ProfileCriteria = new ProfileCriteriaModel(NewProfileBrowseModel.ProfileDetails);
        //    NewProfileBrowseModel.ViewerProfileCriteria = new ProfileCriteriaModel(NewProfileBrowseModel.ViewerProfileDetails );




        //    return NewProfileBrowseModel;

        //}

        /// <summary>
        /// Think this is old not used anymore
        /// </summary>
        /// <param name="ProfileID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        //public MemberSearchViewModel GetMemberSearchModelByProfileID(string ProfileID ,MembersViewModel model)
        //{ 

        //MemberSearchViewModel MemberSearchViewmodel;






        //    MemberSearchViewmodel = (from x in db.ProfileDatas.Where(p => p.profile.ProfileID == ProfileID)                  
        //                 join f in db.profiles on x.ProfileID equals f.ProfileID

        //                 select new MemberSearchViewModel
        //                 {                             
        //                     MyCatchyIntroLineQuickSearch = x.AboutMe,
        //                     ProfileID = x.ProfileID,
        //                     State_Province = x.State_Province,
        //                     PostalCode = x.PostalCode,
        //                     CountryID = x.CountryID,                          
        //                     GenderID = x.GenderID,
        //                     Birthdate = x.Birthdate,                            
        //                     profile = f,
        //                     Longitude = (double)x.Longitude,
        //                     Latitude = (double)x.Latitude,
        //                     HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType =="Gallery") select p.ProfileImageType).FirstOrDefault(),
        //                     CreationDate = f.CreationDate,
        //                     City = db.fnTruncateString(x.City,11),
        //                     lastloggedonString = db.fnGetLastLoggedOnTime(f.LoginDate),
        //                     LastLoginDate = f.LoginDate,
        //                     Online = db.fnGetUserOlineStatus(x.ProfileID),
        //                     DistanceFromMe = db.fnGetDistance((double)x.Latitude, (double)x.Longitude, model.MyQuickSearch.MySelectedLatitude, model.MyQuickSearch.MySelectedLongitude, "Miles")


        //                 }).FirstOrDefault();



        // return  MemberSearchViewmodel;

        //}



        //public IEnumerable<MemberSearchViewModel> GetQuickSearch(int intAgeFrom, int intAgeTo,
        //                                     string strLookingForSelectedGenderName, int intSelectedCountryId,
        //                                     string strSelectedCity, string strSelectedStateProvince)
        //{
        //    List<MemberSearchViewModel> model;
        //    if (strSelectedCity == "ALL" | strSelectedCity == "")
        //    {
        //        model = (from x in db.ProfileDatas
        //         .Where(p => p.gender.GenderName == strLookingForSelectedGenderName
        //             && p.CountryID == intSelectedCountryId)
        //                 join f in db.profiles
        //                 on x.ProfileID equals f.ProfileID
        //                 join z in db.photos  on x.ProfileID  equals z.ProfileID 
        //                 select new MemberSearchViewModel
        //                 {
        //                    // MyCatchyIntroLineQuickSearch = x.AboutMe,
        //                     ProfileID = x.ProfileID,
        //                     State_Province = x.State_Province,
        //                     PostalCode = x.PostalCode,
        //                     CountryID = x.CountryID,
        //                     City = x.City,
        //                     GenderID = x.GenderID,
        //                     Birthdate = x.Birthdate,                         
        //                     profile = f,
        //                     CreationDate = f.CreationDate,                            
        //                     HasGalleryPhoto = z.ProfileImageType  //check if the photo included gallery
        //                 }).Take(1000).OrderBy(p=>p.HasGalleryPhoto).ToList();;
        //    }
        //    else
        //    {
        //        model = (from x in db.ProfileDatas
        //            .Where(p => p.gender.GenderName == strLookingForSelectedGenderName
        //                && p.CountryID == intSelectedCountryId
        //                && p.City == strSelectedCity && p.State_Province == strSelectedStateProvince)
        //                 join f in db.profiles on x.ProfileID equals f.ProfileID
        //                 join z in db.photos  on x.ProfileID  equals z.ProfileID 
        //                 select new MemberSearchViewModel
        //                 {
        //                     MyCatchyIntroLineQuickSearch = x.AboutMe,
        //                     ProfileID = x.ProfileID,
        //                     State_Province = x.State_Province,
        //                     PostalCode = x.PostalCode,
        //                     CountryID = x.CountryID,
        //                     City = x.City,
        //                     GenderID = x.GenderID,
        //                     Birthdate = x.Birthdate,                             
        //                     profile = f,
        //                     HasGalleryPhoto = z.ProfileImageType  //check if the photo included gallery


        //                 }).OrderBy(p=>p.HasGalleryPhoto).ToList();
        //    }


        //    // final query: query against required age range
        //    var Profiles = from q in model.Where(a => a.Age <= intAgeTo && a.Age >= intAgeFrom) select q;
        //    return Profiles;

        //}

        //updated do not use the data from the members stuff since we are correctly populating values now , just find the lat long for the city and country entered
        public List<MemberSearchViewModel> GetQuickSearchMembers(int intAgeFrom, int intAgeTo,
                                                    string strLookingForSelectedGenderName, int intSelectedCountryId,
                                                    string strSelectedCity, string strSelectedStateProvince, double MaxDistanceFromMe, bool HasPhoto, MembersViewModel model)
        {
            List<MemberSearchViewModel> MemberSearchViewmodels;


            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);

            // var years = employee.Where(e => e.DOB != null && e.DOB > min && e.DOB <= max); 


            //if selected sity is all or empty lets just pull the top 100 members, that macth the rest of the ce

            //if selected sity is all or empty lets just pull the top 100 members, that macth the rest of the ce           
            //TO DO
            //make this more efficnet and resitrcted later maybe only vip members
            //  where (strSelectedCity != "ALL" || x.City == strSelectedCity)
            //(x.Age >= intAgeFrom && x.Age <= intAgeTo  )
            //let Age = System.Data.Objects.SqlClient.SqlFunctions.DateDiff("y", x.Birthdate, DateTime.Now)  
            //from uir in Aspnet_UsersInRoles 
            //            from r in Aspnet_Roles.Where( _r => _r.RoleId == uir.RoleId).DefaultIfEmpty() 
            //            group r.RoleName by uir.UserId into gr 
            //            join u in Aspnet_Users on gr.Key equals u.UserId         
            //            from up in UserProfiles.Where( _up => _up.UserId == u.UserId ).DefaultIfEmpty() 
            //            orderby  up.FirstName, up.LastName, u.UserName 
            //            select new 
            //            { 

            // ObjectSet <ProfileData> products = db.ProfileDatas ;  


            //******** visiblitysettings test code ************************

            //// test all the values you are pulling here
            //var TestModel = (from x in db.ProfileDatas.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.Birthdate > min && p.Birthdate <= max)
            //                      select x).FirstOrDefault();
            //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault() + 1));
            // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.ProfileData.GenderID) ? true : false;

            // var testmodel2 = (from x in db.ProfileDatas.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.Birthdate > min && p.Birthdate <= max)
            //                     .Where(z=>z.ProfileVisiblitySetting !=null || z.ProfileVisiblitySetting.ProfileVisiblity == true)
            //                     select x).FirstOrDefault();

            // Expression<Func<ProfileData, bool>> MyWhereExpr = default(Expression<Func<ProfileData,  bool>>);



            MemberSearchViewmodels = (from x in db.ProfileDatas.Include("ProfileVisiblitySetting").Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.Birthdate > min && p.Birthdate <= max)
                                         .WhereIf(strSelectedCity == "ALL", z => z.CountryID == intSelectedCountryId)
                                      //conditonal query, bascially if the chose all cities or none
                                      join f in db.profiles on x.ProfileID equals f.ProfileID
                                      // from fp in f  where(x.CountryID == intSelectedCountryId)
                                      //join z in db.photos on x.ProfileID equals z.ProfileID
                                      select new MemberSearchViewModel
                                      {
                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          ProfileID = x.ProfileID,
                                          State_Province = x.State_Province,
                                          PostalCode = x.PostalCode,
                                          CountryID = x.CountryID,
                                          GenderID = x.GenderID,
                                          Birthdate = x.Birthdate,
                                          profile = f,
                                          Longitude = (double)x.Longitude,
                                          Latitude = (double)x.Latitude,
                                          HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                                          CreationDate = f.CreationDate,
                                          City = db.fnTruncateString(x.City, 11),
                                          lastloggedonString = db.fnGetLastLoggedOnTime(f.LoginDate),
                                          LastLoginDate = f.LoginDate,
                                          Online = db.fnGetUserOlineStatus(x.ProfileID),
                                          DistanceFromMe = db.fnGetDistance((double)x.Latitude, (double)x.Longitude, model.MyQuickSearch.MySelectedLatitude.Value, model.MyQuickSearch.MySelectedLongitude.Value, "Miles"),
                                          ProfileVisibility = x.ProfileVisiblitySetting.ProfileVisiblity

                                      }).ToList();


            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (MaxDistanceFromMe > 0 && strSelectedCity != "ALL") ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= MaxDistanceFromMe) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.CountryID  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.LastLoginDate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.HasGalleryPhoto == "Gallery").ThenByDescending(p => p.CreationDate).ThenByDescending(p => p.DistanceFromMe);


            //5-15-2012 filter out by visiblity settings and other visiblity stuff
            Profiles = Profiles.Where(x => x.ProfileVisibility != false);


            return Profiles.ToList();

        }








    }


  
  
}





