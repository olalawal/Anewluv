using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileData
    {
        public ProfileData()
        {
            this.abusereports = new List<abusereport>();
            this.favorites = new List<favorite>();
            this.Friends = new List<Friend>();
            this.Hotlists = new List<Hotlist>();
            this.Interests = new List<Interest>();
            this.Likes = new List<Like>();
            this.Mailboxblocks = new List<Mailboxblock>();
            this.MailboxFolders = new List<MailboxFolder>();
            this.MembersInRoles = new List<MembersInRole>();
            this.photos = new List<photo>();
            this.photos1 = new List<photo>();
            this.PhotoAlbums = new List<PhotoAlbum>();
            this.ProfileData_Ethnicity = new List<ProfileData_Ethnicity>();
            this.ProfileData_Hobby = new List<ProfileData_Hobby>();
            this.ProfileData_HotFeature = new List<ProfileData_HotFeature>();
            this.ProfileData_LookingFor = new List<ProfileData_LookingFor>();
            this.ProfileEmailUpdateFreqencies = new List<ProfileEmailUpdateFreqency>();
            this.ProfileRatings = new List<ProfileRating>();
            this.ProfileViews = new List<ProfileView>();
            this.SearchSettings = new List<SearchSetting>();
            this.SearchSettings1 = new List<SearchSetting>();
            this.User_Logtime = new List<User_Logtime>();
        }

        public string ProfileID { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string State_Province { get; set; }
        public string Country_Region { get; set; }
        public string PostalCode { get; set; }
        public int CountryID { get; set; }
        public string City { get; set; }
        public int GenderID { get; set; }
        public Nullable<int> Age { get; set; }
        public System.DateTime Birthdate { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> BodyTypeID { get; set; }
        public Nullable<int> EyeColorID { get; set; }
        public Nullable<int> HairColorID { get; set; }
        public Nullable<int> ExerciseID { get; set; }
        public Nullable<int> ReligionID { get; set; }
        public Nullable<int> ReligiousAttendanceID { get; set; }
        public Nullable<int> DrinksID { get; set; }
        public Nullable<int> SmokesID { get; set; }
        public Nullable<int> HumorID { get; set; }
        public Nullable<int> PoliticalViewID { get; set; }
        public Nullable<int> DietID { get; set; }
        public Nullable<int> SignID { get; set; }
        public Nullable<int> IncomeLevelID { get; set; }
        public Nullable<int> HaveKidsId { get; set; }
        public Nullable<int> WantsKidsID { get; set; }
        public Nullable<int> EmploymentSatusID { get; set; }
        public Nullable<int> EducationLevelID { get; set; }
        public Nullable<int> ProfessionID { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public Nullable<int> LivingSituationID { get; set; }
        public Nullable<int> NigerianStateID { get; set; }
        public Nullable<int> TribeID { get; set; }
        public string AboutMe { get; set; }
        public string Phone { get; set; }
        public string MyCatchyIntroLine { get; set; }
        public virtual abuser abuser { get; set; }
        public virtual ICollection<abusereport> abusereports { get; set; }
        public virtual CriteriaAppearance_Bodytypes CriteriaAppearance_Bodytypes { get; set; }
        public virtual CriteriaAppearance_EyeColor CriteriaAppearance_EyeColor { get; set; }
        public virtual CriteriaAppearance_HairColor CriteriaAppearance_HairColor { get; set; }
        public virtual CriteriaCharacter_Diet CriteriaCharacter_Diet { get; set; }
        public virtual CriteriaCharacter_Drinks CriteriaCharacter_Drinks { get; set; }
        public virtual CriteriaCharacter_Exercise CriteriaCharacter_Exercise { get; set; }
        public virtual CriteriaCharacter_Humor CriteriaCharacter_Humor { get; set; }
        public virtual CriteriaCharacter_PoliticalView CriteriaCharacter_PoliticalView { get; set; }
        public virtual CriteriaCharacter_Religion CriteriaCharacter_Religion { get; set; }
        public virtual CriteriaCharacter_ReligiousAttendance CriteriaCharacter_ReligiousAttendance { get; set; }
        public virtual CriteriaCharacter_Sign CriteriaCharacter_Sign { get; set; }
        public virtual CriteriaCharacter_Smokes CriteriaCharacter_Smokes { get; set; }
        public virtual CriteriaLife_EducationLevel CriteriaLife_EducationLevel { get; set; }
        public virtual CriteriaLife_EmploymentStatus CriteriaLife_EmploymentStatus { get; set; }
        public virtual CriteriaLife_HaveKids CriteriaLife_HaveKids { get; set; }
        public virtual CriteriaLife_IncomeLevel CriteriaLife_IncomeLevel { get; set; }
        public virtual CriteriaLife_LivingSituation CriteriaLife_LivingSituation { get; set; }
        public virtual CriteriaLife_MaritalStatus CriteriaLife_MaritalStatus { get; set; }
        public virtual CriteriaLife_Profession CriteriaLife_Profession { get; set; }
        public virtual CriteriaLife_WantsKids CriteriaLife_WantsKids { get; set; }
        public virtual ICollection<favorite> favorites { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual gender gender { get; set; }
        public virtual ICollection<Hotlist> Hotlists { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Mailboxblock> Mailboxblocks { get; set; }
        public virtual ICollection<MailboxFolder> MailboxFolders { get; set; }
        public virtual ICollection<MembersInRole> MembersInRoles { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public virtual ICollection<photo> photos1 { get; set; }
        public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
        public virtual ICollection<ProfileData_Ethnicity> ProfileData_Ethnicity { get; set; }
        public virtual ICollection<ProfileData_Hobby> ProfileData_Hobby { get; set; }
        public virtual ICollection<ProfileData_HotFeature> ProfileData_HotFeature { get; set; }
        public virtual ICollection<ProfileData_LookingFor> ProfileData_LookingFor { get; set; }
        public virtual ICollection<ProfileEmailUpdateFreqency> ProfileEmailUpdateFreqencies { get; set; }
        public virtual ICollection<ProfileRating> ProfileRatings { get; set; }
        public virtual profile profile { get; set; }
        public virtual ICollection<ProfileView> ProfileViews { get; set; }
        public virtual ProfileVisiblitySetting ProfileVisiblitySetting { get; set; }
        public virtual ICollection<SearchSetting> SearchSettings { get; set; }
        public virtual ICollection<SearchSetting> SearchSettings1 { get; set; }
        public virtual ICollection<User_Logtime> User_Logtime { get; set; }
    }
}
