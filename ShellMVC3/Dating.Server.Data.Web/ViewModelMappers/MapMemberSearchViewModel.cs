using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using Dating.Server.Data.ViewModels;

namespace Dating.Server.Data.ViewModelMappers
{
    class MapMemberSearchViewModel
    {


         // constructor
        public MemberSearchViewModelTemp MapMemberSearchViewModel(string ProfileId)
        {
            if (ProfileId  != null)
            {

                MemberSearchViewModelTemp model = new MemberSearchViewModelTemp();    
                //TO DO change to use Ninject maybe
                DatingService db = new DatingService(); 
               //  MembersRepository membersrepo=  new MembersRepository();
                ProfileData   CurrentProfileData = db.GetProfileDataByProfileID(ProfileId) ; //db.ProfileDatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
               //  EditProfileRepository editProfileRepository = new EditProfileRepository();



                model.ProfileID  = CurrentProfileData.ProfileID;
                model. profiledata = CurrentProfileData;
                model.profile = CurrentProfileData.profile;
                model.State_Province = CurrentProfileData.State_Province;
                model.PostalCode = CurrentProfileData.PostalCode;
                model.CountryID = CurrentProfileData.CountryID;
                model.GenderID = CurrentProfileData.GenderID;
                model.Birthdate = CurrentProfileData.Birthdate;
               // modelprofile = CurrentProfileData.profile;
                model.Longitude = (double)CurrentProfileData.Longitude;
                model.Latitude = (double)CurrentProfileData.Latitude;
               //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.CreationDate = CurrentProfileData.profile.CreationDate;
                model.City = Extensions.ReduceStringLength(CurrentProfileData.City, 11);
                model.LastLoginDate = CurrentProfileData.profile.LoginDate;
                model.lastloggedonString = model.lastloggedonString = db.GetLastLoggedInString(model.LastLoginDate);
                model.Online = db.GetUserOnlineStatus(CurrentProfileData.ProfileID);
               // PerfectMatchSettings = CurrentProfileData.SearchSettings.First();
                //DistanceFromMe = 0  get distance from somwhere else
                //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                //or instead just have the photo the select zoom up
                int page = 1;
                int ps = 12;
                var MyPhotos = editProfileRepository.MyPhotos(model.profile.UserName);
                var Approved = editProfileRepository.GetApproved(MyPhotos, "Yes", page, ps);
                var NotApproved = editProfileRepository.GetApproved(MyPhotos, "No", page, ps);
                var Private = editProfileRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                ProfilePhotos = editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);

                
            }

        }


    }
}
