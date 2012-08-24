namespace Dating.Server.Data.Models

{  

using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.Runtime.Serialization;



    // The MetadataTypeAttribute identifies CommunicationQuotaMetadata as the class
    // that carries additional metadata for the CommunicationQuota class.
  
    [MetadataTypeAttribute(typeof(CommunicationQuota.CommunicationQuotaMetadata))]
    public partial class CommunicationQuota
    {
        //TO DO flatten this out and allow for a QuotoaPRofileROle linkage
        // This class allows you to attach custom attributes to properties
        // of the CommunicationQuota class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CommunicationQuotaMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CommunicationQuotaMetadata()
            {
            }

            public Nullable<bool> Active { get; set; }

            public Nullable<DateTime> LastUpdateDate { get; set; }

            public string QuotaDescription { get; set; }

            public int QuotaID { get; set; }

            public string QuotaName { get; set; }

            public Nullable<int> QuotaRoleID { get; set; }

            public Nullable<int> QuotaValue { get; set; }

            public string UpdatedBy { get; set; }
        }
    }


    // The MetadataTypeAttribute identifies ProfileDataMetadata as the class
    // that carries additional metadata for the ProfileData class.
       
    [MetadataTypeAttribute(typeof(ProfileData.ProfileDataMetadata))]
    public partial class ProfileData
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileData class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileDataMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileDataMetadata()
            {
            }

            public string AboutMe { get; set; }

            public abuser abuser { get; set; }

            [Include]
            public EntityCollection<abusereport> abusereports { get; set; }

            public Nullable<int> Age { get; set; }

            public DateTime Birthdate { get; set; }

            public Nullable<int> BodyTypeID { get; set; }

            public string City { get; set; }

            public string Country_Region { get; set; }

            public int CountryID { get; set; }


            [Include]
            public CriteriaAppearance_Bodytypes CriteriaAppearance_Bodytypes { get; set; }
            [Include]
            public CriteriaAppearance_EyeColor CriteriaAppearance_EyeColor { get; set; }
            [Include]
            public CriteriaAppearance_HairColor CriteriaAppearance_HairColor { get; set; }
            [Include]
            public CriteriaCharacter_Diet CriteriaCharacter_Diet { get; set; }
            [Include]
            public CriteriaCharacter_Drinks CriteriaCharacter_Drinks { get; set; }
            [Include]
            public CriteriaCharacter_Exercise CriteriaCharacter_Exercise { get; set; }
            [Include]
            public CriteriaCharacter_Humor CriteriaCharacter_Humor { get; set; }
            [Include]
            public CriteriaCharacter_PoliticalView CriteriaCharacter_PoliticalView { get; set; }
            [Include]
            public CriteriaCharacter_Religion CriteriaCharacter_Religion { get; set; }
            [Include]
            public CriteriaCharacter_ReligiousAttendance CriteriaCharacter_ReligiousAttendance { get; set; }
            [Include]
            public CriteriaCharacter_Sign CriteriaCharacter_Sign { get; set; }
            [Include]
            public CriteriaCharacter_Smokes CriteriaCharacter_Smokes { get; set; }
            [Include]
            public CriteriaLife_EducationLevel CriteriaLife_EducationLevel { get; set; }
            [Include]
            public CriteriaLife_EmploymentStatus CriteriaLife_EmploymentStatus { get; set; }
            [Include]
            public CriteriaLife_HaveKids CriteriaLife_HaveKids { get; set; }
            [Include]
            public CriteriaLife_IncomeLevel CriteriaLife_IncomeLevel { get; set; }
            [Include]
            public CriteriaLife_LivingSituation CriteriaLife_LivingSituation { get; set; }
            [Include]
            public CriteriaLife_MaritalStatus CriteriaLife_MaritalStatus { get; set; }
            [Include]
            public CriteriaLife_Profession CriteriaLife_Profession { get; set; }
            [Include]
            public CriteriaLife_WantsKids CriteriaLife_WantsKids { get; set; }

            public Nullable<int> DietID { get; set; }

            public Nullable<int> DrinksID { get; set; }

            public Nullable<int> EducationLevelID { get; set; }

            public Nullable<int> EmploymentSatusID { get; set; }

            public Nullable<int> ExerciseID { get; set; }

            public Nullable<int> EyeColorID { get; set; }

            [Include]
            public EntityCollection<favorite> favorites { get; set; }

            [Include]
            public EntityCollection<Friend> Friends { get; set; }

            public gender gender { get; set; }

            public int GenderID { get; set; }

            public Nullable<int> HairColorID { get; set; }

            public Nullable<int> HaveKidsId { get; set; }

            public Nullable<int> Height { get; set; }


            public Nullable<int> HumorID { get; set; }

            public Nullable<int> IncomeLevelID { get; set; }

            [Include]
            public EntityCollection<Interest> Interests { get; set; }

            public Nullable<double> Latitude { get; set; }

            [Include]
            public EntityCollection<Like> Likes { get; set; }

            public Nullable<int> LivingSituationID { get; set; }

            public Nullable<double> Longitude { get; set; }

            [Include]
            public EntityCollection<Mailboxblock> Mailboxblocks { get; set; }

            [Include]
            public EntityCollection<MailboxFolder> MailboxFolders { get; set; }

            public Nullable<int> MaritalStatusID { get; set; }

            [Include]
            public EntityCollection<MembersInRole> MembersInRoles { get; set; }

            public string MyCatchyIntroLine { get; set; }

            public Nullable<int> NigerianStateID { get; set; }

            public string Phone { get; set; }

            [Include]
            public EntityCollection<PhotoAlbum> PhotoAlbums { get; set; }

            [Include]
            public EntityCollection<photo> photos { get; set; }
           

            public Nullable<int> PoliticalViewID { get; set; }

            public string PostalCode { get; set; }

            public Nullable<int> ProfessionID { get; set; }

            [Include]
            public profile profile { get; set; }

            [Include]
            public EntityCollection<ProfileData_Ethnicity> ProfileData_Ethnicity { get; set; }

            [Include]
            public EntityCollection<ProfileData_Hobby> ProfileData_Hobby { get; set; }

            [Include]
            public EntityCollection<ProfileData_HotFeature> ProfileData_HotFeature { get; set; }


            [Include]
            public EntityCollection<ProfileData_LookingFor> ProfileData_LookingFor { get; set; }

            [Include]
            public EntityCollection<ProfileEmailUpdateFreqency> ProfileEmailUpdateFreqencies { get; set; }

            public string ProfileID { get; set; }

            [Include]
            public EntityCollection<ProfileRating> ProfileRatings { get; set; }

            [Include]
            public EntityCollection<ProfileView> ProfileViews { get; set; }

            [Include]
            public ProfileVisiblitySetting ProfileVisiblitySetting { get; set; }

            public Nullable<int> ReligionID { get; set; }

            public Nullable<int> ReligiousAttendanceID { get; set; }

            [Include]
            public EntityCollection<SearchSetting> SearchSettings { get; set; }


            public Nullable<int> SignID { get; set; }

            public Nullable<int> SmokesID { get; set; }

            public string State_Province { get; set; }

            public Nullable<int> TribeID { get; set; }

            [Include]
            public EntityCollection<User_Logtime> User_Logtime { get; set; }

            public Nullable<int> WantsKidsID { get; set; }
        }
    }


    // The MetadataTypeAttribute identifies profileMetadata as the class
    // that carries additional metadata for the profile class.
       
    [MetadataTypeAttribute(typeof(profile.profileMetadata))]
    public partial class profile
    {

        // This class allows you to attach custom attributes to properties
        // of the profile class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class profileMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private profileMetadata()
            {
            }

            public string ActivationCode { get; set; }

            public DateTime CreationDate { get; set; }

            public Nullable<int> DailSentMessageQuota { get; set; }

            public Nullable<int> DailySentEmailQuota { get; set; }

            public Nullable<byte> ForwardMessages { get; set; }

            public DateTime LoginDate { get; set; }

            [Include]
            public EntityCollection<MembersInRole> MembersInRoles { get; set; }

            public DateTime ModificationDate { get; set; }

            public string Password { get; set; }

            public Nullable<int> PasswordChangeAttempts { get; set; }

            public Nullable<int> PasswordChangedCount { get; set; }

            public Nullable<DateTime> PasswordChangedDate { get; set; }

             [Include]
            public ProfileData ProfileData { get; set; }

            //[Include]
            //public EntityCollection<ProfileGeoDataLogger> ProfileGeoDataLogger { get; set; }

            public string ProfileID { get; set; }

            public int ProfileIndex { get; set; }

            //[Include]
            //public EntityCollection<profileOpenIDStore> profileOpenIDStore { get; set; }

             [Include]
            public profilestatus profilestatus { get; set; }

            public int ProfileStatusID { get; set; }

            public Nullable<bool> ReadPrivacyStatement { get; set; }

            public Nullable<bool> ReadTemsOfUse { get; set; }

            public string salt { get; set; }

            public string ScreenName { get; set; }

            public string SecurityAnswer { get; set; }

            public SecurityQuestion SecurityQuestion { get; set; }

            public Nullable<byte> SecurityQuestionID { get; set; }

            public Nullable<int> SentEmailQuotaHitCount { get; set; }

            public Nullable<int> SentMessageQuotaHitCount { get; set; }

            public string UserName { get; set; }
        }
    }
    

    // The MetadataTypeAttribute identifies ProfileData_HotFeatureMetadata as the class
    // that carries additional metadata for the ProfileData_HotFeature class.
       
    [MetadataTypeAttribute(typeof(ProfileData_HotFeature.ProfileData_HotFeatureMetadata))]
    public partial class ProfileData_HotFeature
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileData_HotFeature class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileData_HotFeatureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileData_HotFeatureMetadata()
            {
            }

               [Include]
            public CriteriaCharacter_HotFeature CriteriaCharacter_HotFeature { get; set; }

            public Nullable<int> HotFeatureID { get; set; }

            public ProfileData ProfileData { get; set; }

            public int ProfileData_HotFeature1 { get; set; }

            public string ProfileID { get; set; }
        }
    }



       
      [MetadataTypeAttribute(typeof(ProfileData_Ethnicity.ProfileData_EthnicityMetadata))]
    public partial class ProfileData_Ethnicity
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileData_Ethnicity class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileData_EthnicityMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileData_EthnicityMetadata()
            {
            }

            [Include]
            public CriteriaAppearance_Ethnicity CriteriaAppearance_Ethnicity { get; set; }

            public Nullable<int> EthnicityID { get; set; }

            public ProfileData ProfileData { get; set; }

            public int ProfileData_Ethnicity1 { get; set; }

            public string ProfileID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileData_HobbyMetadata as the class
    // that carries additional metadata for the ProfileData_Hobby class.
    [MetadataTypeAttribute(typeof(ProfileData_Hobby.ProfileData_HobbyMetadata))]
    public partial class ProfileData_Hobby
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileData_Hobby class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileData_HobbyMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileData_HobbyMetadata()
            {
            }

              [Include]
            public CriteriaCharacter_Hobby CriteriaCharacter_Hobby { get; set; }

            public Nullable<int> HobbyID { get; set; }

            public ProfileData ProfileData { get; set; }

            public int ProfileData_Hobby1 { get; set; }

            public string ProfileID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileData_LookingForMetadata as the class
    // that carries additional metadata for the ProfileData_LookingFor class.
       [MetadataTypeAttribute(typeof(ProfileData_LookingFor.ProfileData_LookingForMetadata))]
    public partial class ProfileData_LookingFor
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileData_LookingFor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileData_LookingForMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileData_LookingForMetadata()
            {
            }

              [Include]
            public CriteriaLife_LookingFor CriteriaLife_LookingFor { get; set; }

            public Nullable<int> LookingForID { get; set; }

            public ProfileData ProfileData { get; set; }

            public int ProfileData_LookingFor1 { get; set; }

            public string ProfileID { get; set; }
        }
    }









    // The MetadataTypeAttribute identifies photoMetadata as the class
    // that carries additional metadata for the photo class.
       [MetadataTypeAttribute(typeof(photo.photoMetadata))]
    public partial class photo
    {

        // This class allows you to attach custom attributes to properties
        // of the photo class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
           [DataContract]
        internal sealed class photoMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private photoMetadata()
            {
            }

            [DataMember ]
            public string Aproved { get; set; }
               [DataMember]
            public string ImageCaption { get; set; }

            [Include]
            [DataMember]
            public PhotoAlbum PhotoAlbum { get; set; }
               [DataMember]
            public Nullable<int> PhotoAlbumID { get; set; }
               [DataMember]
            public Nullable<DateTime> PhotoDate { get; set; }
               [DataMember]
            public Guid PhotoID { get; set; }

            [Include]
            [DataMember]
            public PhotoRejectionReason PhotoRejectionReason { get; set; }
               [DataMember]
            public Nullable<int> PhotoRejectionReasonID { get; set; }
               [DataMember]
            public Nullable<DateTime> PhotoReviewDate { get; set; }
               [DataMember]
            public string PhotoReviewerID { get; set; }

            [Include]
            [DataMember]
            public PhotoReviewStatu PhotoReviewStatu { get; set; }
               [DataMember]
            public Nullable<int> PhotoReviewStatusID { get; set; }
               [DataMember]
            public Nullable<int> PhotoSize { get; set; }

            [Include]
            [DataMember]
            public PhotoStatu PhotoStatu { get; set; }

               [DataMember]
            public int PhotoStatusID { get; set; }

            [Include]
            [DataMember]
            public PhotoType PhotoType { get; set; }
               [DataMember]
            public Nullable<int> PhotoTypeID { get; set; }
               [DataMember]
            public Nullable<Guid> PhotoUniqueID { get; set; }


            [Include]
            [DataMember]
            public ProfileData ProfileData { get; set; }

            [Include]
            [DataMember]
            public ProfileData ProfileData1 { get; set; }

               [DataMember]
            public string ProfileID { get; set; }

               [DataMember]
            public byte[] ProfileImage { get; set; }

               [DataMember]
            public string ProfileImageType { get; set; }
        }
    }


    // The MetadataTypeAttribute identifies PhotoAlbumMetadata as the class
    // that carries additional metadata for the PhotoAlbum class.
       [MetadataTypeAttribute(typeof(PhotoAlbum.PhotoAlbumMetadata))]
    public partial class PhotoAlbum
    {

        // This class allows you to attach custom attributes to properties
        // of the PhotoAlbum class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PhotoAlbumMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PhotoAlbumMetadata()
            {
            }

            public string PhotoAlbumDescription { get; set; }

            public int PhotoAlbumID { get; set; }

                [Include]
            public EntityCollection<photo> photos { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }
        }
    }




    // The MetadataTypeAttribute identifies SearchSettingMetadata as the class
    // that carries additional metadata for the SearchSetting class.
       [MetadataTypeAttribute(typeof(SearchSetting.SearchSettingMetadata))]
    public partial class SearchSetting
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSetting class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettingMetadata()
            {
            }

            public Nullable<int> AgeMax { get; set; }

            public Nullable<int> AgeMin { get; set; }

            public Nullable<DateTime> CreationDate { get; set; }

            public Nullable<int> DistanceFromMe { get; set; }

            public Nullable<int> HeightMax { get; set; }

            public Nullable<int> HeightMin { get; set; }

            public Nullable<DateTime> LastUpdateDate { get; set; }

            public Nullable<bool> MyPerfectMatch { get; set; }

             [Include]
            public ProfileData ProfileData { get; set; }

            public ProfileData ProfileData1 { get; set; }

            public string ProfileID { get; set; }

            public Nullable<bool> SavedSearch { get; set; }

            public string SearchName { get; set; }

            public Nullable<int> SearchRank { get; set; }


            [Include]
            public EntityCollection<SearchSettings_BodyTypes> SearchSettings_BodyTypes { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Diet> SearchSettings_Diet { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Drinks> SearchSettings_Drinks { get; set; }
            [Include]
            public EntityCollection<SearchSettings_EducationLevel> SearchSettings_EducationLevel { get; set; }
            [Include]
            public EntityCollection<SearchSettings_EmploymentStatus> SearchSettings_EmploymentStatus { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Ethnicity> SearchSettings_Ethnicity { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Exercise> SearchSettings_Exercise { get; set; }           
            [Include]
            public EntityCollection<SearchSettings_EyeColor> SearchSettings_EyeColor { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Genders> SearchSettings_Genders { get; set; }
            [Include]
            public EntityCollection<SearchSettings_HairColor> SearchSettings_HairColor { get; set; }
            [Include]
            public EntityCollection<SearchSettings_HaveKids> SearchSettings_HaveKids { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Hobby> SearchSettings_Hobby { get; set; }
            [Include]
            public EntityCollection<SearchSettings_HotFeature> SearchSettings_HotFeature { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Humor> SearchSettings_Humor { get; set; }
            [Include]
            public EntityCollection<SearchSettings_IncomeLevel> SearchSettings_IncomeLevel { get; set; }
            [Include]
            public EntityCollection<SearchSettings_LivingStituation> SearchSettings_LivingStituation { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Location> SearchSettings_Location { get; set; }


            [Include]
            public EntityCollection<SearchSettings_LookingFor> SearchSettings_LookingFor { get; set; }
            [Include]
            public EntityCollection<SearchSettings_MaritalStatus> SearchSettings_MaritalStatus { get; set; }
            // [Include]
            //public EntityCollection<SearchSettings_NigerianState> SearchSettings_NigerianState { get; set; }
            [Include]
            public EntityCollection<SearchSettings_PoliticalView> SearchSettings_PoliticalView { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Profession> SearchSettings_Profession { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Religion> SearchSettings_Religion { get; set; }
            [Include]
            public EntityCollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance { get; set; }
            [Include]
            public EntityCollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance1 { get; set; }
            [Include]
            public EntityCollection<SearchSettings_ShowMe> SearchSettings_ShowMe { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Sign> SearchSettings_Sign { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Smokes> SearchSettings_Smokes { get; set; }
            [Include]
            public EntityCollection<SearchSettings_SortByType> SearchSettings_SortByType { get; set; }
            [Include]
            public EntityCollection<SearchSettings_Tribe> SearchSettings_Tribe { get; set; }
            [Include]
            public EntityCollection<SearchSettings_WantKids> SearchSettings_WantKids { get; set; }


            public int SearchSettingsID { get; set; }

            public Nullable<bool> SystemMatch { get; set; }
        }
    }



    

}
