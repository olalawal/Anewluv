namespace Shell.MVC2.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public interface IEditProfileService
    {
        EditProfileSettingsViewModel EditProfileBasicSettingsPage1Update(EditProfileSettingsViewModel model,
                                                                                         FormCollection formCollection, int?[] SelectedGenderIds, string _ProfileID);

        EditProfileSettingsViewModel EditProfileBasicSettingsPage2Update(EditProfileSettingsViewModel model,
                                                                                         FormCollection formCollection,
                                                                                         int?[] SelectedShowMeIds, int?[] SelectedSortByIds, string _ProfileID);

        EditProfileSettingsViewModel EditProfileAppearanceSettingsPage1Update(EditProfileSettingsViewModel model,
                                                                                              FormCollection formCollection, int?[] SelectedYourBodyTypesID,  string _ProfileID);

        EditProfileSettingsViewModel EditProfileAppearanceSettingsPage2Update(EditProfileSettingsViewModel model,
                                                                                              FormCollection formCollection, int?[] SelectedYourEthnicityIds, int?[] SelectedMyEthnicityIds, string _ProfileID
            );

        EditProfileSettingsViewModel EditProfileAppearanceSettingsPage3Update(EditProfileSettingsViewModel model,
                                                                                              FormCollection formCollection, int?[] SelectedYourEyeColorIds, 
                                                                                              int?[] SelectedYourHairColorIds,
                                                                                              string _ProfileID);

        EditProfileSettingsViewModel EditProfileAppearanceSettingsPage4Update(EditProfileSettingsViewModel model,
                                                                                              FormCollection formCollection, int?[] SelectedYourHotFeatureIds, int?[] SelectedMyHotFeatureIds, string _ProfileID);

        EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage1Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourMaritalStatusIds, int?[] SelectedYourLivingSituationIds,
                                                                                             int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, string _ProfileID);

        EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage2Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourHaveKidsIds, int?[] SelectedYourWantsKidsIds,
                                                                                             string _ProfileID);

        EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage3Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourEmploymentStatusIds, int?[] SelectedYourIncomeLevelIds,
                                                                                             string _ProfileID);

        EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage4Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourEducationLevelIds, int?[] SelectedYourProfessionIds,
                                                                                             string _ProfileID);

        EditProfileSettingsViewModel EditProfileCharacterSettingsPage1Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourDietIds, int?[] SelectedYourDrinksIds,
                                                                                             int?[] SelectedYourExerciseIds,int?[] SelectedYourSmokesIds,  string _ProfileID);

        EditProfileSettingsViewModel EditProfileCharacterSettingsPage2Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourHobbyIds,int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds,
                                                                                             string _ProfileID);

        EditProfileSettingsViewModel EditProfileCharacterSettingsPage3Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourReligionIds, int?[] SelectedYourReligiousAttendanceIds,
                                                                                             string _ProfileID);

        EditProfileSettingsViewModel EditProfileCharacterSettingsPage4Update(EditProfileSettingsViewModel model,
                                                                                             FormCollection formCollection, int?[] SelectedYourPoliticalViewIds, int?[] SelectedYourHumorIds,
                                                                                             string _ProfileID);

        bool UpdateProfileVisibilitySettings(ProfileVisiblitySetting model);

        IQueryable<photo> MyPhotos(string username);

        IEnumerable<EditProfileViewPhotoModel> GetApproved(IQueryable<photo> MyPhotos, string approved,
                                                                           int page, int pagesize);

        IEnumerable<EditProfileViewPhotoModel> GetApprovedMinusGallery(IQueryable<photo> MyPhotos, string approved,
                                                                                       int page, int pagesize);

        IEnumerable<EditProfileViewPhotoModel> GetPhotoByStatusID(IQueryable<photo> MyPhotos, int photoStatusID,
                                                                                  int page, int pagesize);

        EditProfilePhotosViewModel GetPhotoViewModel(IEnumerable<EditProfileViewPhotoModel> Approved,
                                                                     IEnumerable<EditProfileViewPhotoModel> NotApproved,
                                                                     IEnumerable<EditProfileViewPhotoModel> Private,
                                                                     IQueryable<photo> model);

        EditProfilePhotoModel GetSingleProfilePhotoByphotoID(Guid photoid);

        EditProfilePhotosViewModel GetEditPhotoModel(string UserName, string ApprovedYes,string NotApprovedNo,
                                                                     int photoStatusID, int page, int pagesize);

        void DeletedUserPhoto(Guid PhotoID);

        void MakeUserPhoto_Private(Guid PhotoID);

        void MakeUserPhoto_Public(Guid PhotoID);
    }
}