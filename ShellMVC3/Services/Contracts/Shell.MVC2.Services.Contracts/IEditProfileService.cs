namespace Shell.MVC2.Controllers
{
    using System;
    using System.ServiceModel.Web;
    using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


    public interface IEditProfileService
    {
        string EditProfile();

        string EditProfileBasicSettings(int? ViewIndex, bool? fulledit);

        string EditProfileBasicSettings( EditProfileBasicSettingsModel   model,
                                                               int? ViewIndex, int?[] SelectedGenderIds, int?[] SelectedShowMeIds, int?[] SelectedSortByIds, bool? fulledit);

        string EditProfileAppearanceSettings(int? ViewIndex, bool? fulledit);

        string EditProfileAppearanceSettings(EditProfileBasicSettingsModel model,
                                                                   int? ViewIndex, int?[] SelectedYourBodyTypesIds,
                                                                   int?[] SelectedYourEthnicityIds,int?[] SelectedMyEthnicityIds,int?[] SelectedYourEyeColorIds,
                                                                   int?[] SelectedYourHairColorIds,int?[] SelectedYourHotFeatureIds, int?[] SelectedMyHotFeatureIds,bool? fulledit);

        string EditProfileLifeStyleSettings(int? ViewIndex, bool? fulledit);

        string EditProfileLifeStyleSettings(EditProfileBasicSettingsModel model,
                                                                  int? ViewIndex,  int?[] SelectedYourMaritalStatusIds,
                                                                  int?[] SelectedYourLivingSituationIds,
                                                                  int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, 
                                                                  int?[] SelectedYourHaveKidsIds, int?[] SelectedYourWantsKidsIds, 
                                                                  int?[] SelectedYourEmploymentStatusIds, int?[] SelectedYourIncomeLevelIds,
                                                                  int?[] SelectedYourEducationLevelIds, int?[] SelectedYourProfessionIds,
                                                                  bool? fulledit);

        string EditProfileCharacterSettings(int? ViewIndex, bool? fulledit);

        string EditProfileCharacterSettings(EditProfileBasicSettingsModel model,
                                                                  int? ViewIndex, int?[] SelectedYourDietIds, int?[] SelectedYourDrinksIds,
                                                                  int?[] SelectedYourExerciseIds,int?[] SelectedYourSmokesIds,int?[] SelectedYourHobbyIds,
                                                                  int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds, int?[] SelectedYourReligionIds,
                                                                  int?[] SelectedYourReligiousAttendanceIds, int?[] SelectedYourPoliticalViewIds,
                                                                  int?[] SelectedYourHumorIds, bool? fulledit);

        string EditProfileQuickStats( EditProfileBasicSettingsModel model);

        string EditProfileVisibilitySettings(string ProfileID);

        string EditProfileVisibilitySettings(ProfileVisibilitySettingsModel model);

        string PhotoEditQuick(EditProfilePhotosViewModel model);

        string PhotoEditView(Guid photoid);

        string EditProfile_UpdatePhotos(EditProfilePhotoModel Photo1);

        string IndexPost(Guid[] deleteInputs);

        string PhotoPrivatePost_Delete(Guid[] privateInputs);

        string PhotoPrivatePost_MakePublic(Guid[] privateInputs);

        string PhotoPublicPost_Delete(Guid[] publicInputs);

        string PhotoPublicPost_MakePublic(Guid[] publicInputs);

        string ShowPhotoUpload(EditProfilePhotosViewModel model);

        string EditUploadPhoto(PhotoViewModel model);

        string CancelUploadPhoto(PhotoViewModel model);
    }
}