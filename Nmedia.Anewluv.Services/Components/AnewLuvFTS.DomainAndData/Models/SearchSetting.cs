using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSetting
    {
        public SearchSetting()
        {
            this.SearchSettings_BodyTypes = new List<SearchSettings_BodyTypes>();
            this.SearchSettings_Diet = new List<SearchSettings_Diet>();
            this.SearchSettings_Drinks = new List<SearchSettings_Drinks>();
            this.SearchSettings_EducationLevel = new List<SearchSettings_EducationLevel>();
            this.SearchSettings_EmploymentStatus = new List<SearchSettings_EmploymentStatus>();
            this.SearchSettings_Ethnicity = new List<SearchSettings_Ethnicity>();
            this.SearchSettings_Exercise = new List<SearchSettings_Exercise>();
            this.SearchSettings_EyeColor = new List<SearchSettings_EyeColor>();
            this.SearchSettings_Genders = new List<SearchSettings_Genders>();
            this.SearchSettings_HairColor = new List<SearchSettings_HairColor>();
            this.SearchSettings_HaveKids = new List<SearchSettings_HaveKids>();
            this.SearchSettings_Hobby = new List<SearchSettings_Hobby>();
            this.SearchSettings_HotFeature = new List<SearchSettings_HotFeature>();
            this.SearchSettings_Humor = new List<SearchSettings_Humor>();
            this.SearchSettings_IncomeLevel = new List<SearchSettings_IncomeLevel>();
            this.SearchSettings_LivingStituation = new List<SearchSettings_LivingStituation>();
            this.SearchSettings_Location = new List<SearchSettings_Location>();
            this.SearchSettings_LookingFor = new List<SearchSettings_LookingFor>();
            this.SearchSettings_MaritalStatus = new List<SearchSettings_MaritalStatus>();
            this.SearchSettings_NigerianState = new List<SearchSettings_NigerianState>();
            this.SearchSettings_PoliticalView = new List<SearchSettings_PoliticalView>();
            this.SearchSettings_Profession = new List<SearchSettings_Profession>();
            this.SearchSettings_Religion = new List<SearchSettings_Religion>();
            this.SearchSettings_ReligiousAttendance = new List<SearchSettings_ReligiousAttendance>();
            this.SearchSettings_ReligiousAttendance1 = new List<SearchSettings_ReligiousAttendance>();
            this.SearchSettings_ShowMe = new List<SearchSettings_ShowMe>();
            this.SearchSettings_Sign = new List<SearchSettings_Sign>();
            this.SearchSettings_Smokes = new List<SearchSettings_Smokes>();
            this.SearchSettings_SortByType = new List<SearchSettings_SortByType>();
            this.SearchSettings_Tribe = new List<SearchSettings_Tribe>();
            this.SearchSettings_WantKids = new List<SearchSettings_WantKids>();
        }

        public int SearchSettingsID { get; set; }
        public string ProfileID { get; set; }
        public string SearchName { get; set; }
        public Nullable<int> SearchRank { get; set; }
        public Nullable<int> DistanceFromMe { get; set; }
        public Nullable<int> AgeMin { get; set; }
        public Nullable<int> AgeMax { get; set; }
        public Nullable<int> HeightMin { get; set; }
        public Nullable<int> HeightMax { get; set; }
        public Nullable<bool> MyPerfectMatch { get; set; }
        public Nullable<bool> SystemMatch { get; set; }
        public Nullable<bool> SavedSearch { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual ProfileData ProfileData1 { get; set; }
        public virtual ICollection<SearchSettings_BodyTypes> SearchSettings_BodyTypes { get; set; }
        public virtual ICollection<SearchSettings_Diet> SearchSettings_Diet { get; set; }
        public virtual ICollection<SearchSettings_Drinks> SearchSettings_Drinks { get; set; }
        public virtual ICollection<SearchSettings_EducationLevel> SearchSettings_EducationLevel { get; set; }
        public virtual ICollection<SearchSettings_EmploymentStatus> SearchSettings_EmploymentStatus { get; set; }
        public virtual ICollection<SearchSettings_Ethnicity> SearchSettings_Ethnicity { get; set; }
        public virtual ICollection<SearchSettings_Exercise> SearchSettings_Exercise { get; set; }
        public virtual ICollection<SearchSettings_EyeColor> SearchSettings_EyeColor { get; set; }
        public virtual ICollection<SearchSettings_Genders> SearchSettings_Genders { get; set; }
        public virtual ICollection<SearchSettings_HairColor> SearchSettings_HairColor { get; set; }
        public virtual ICollection<SearchSettings_HaveKids> SearchSettings_HaveKids { get; set; }
        public virtual ICollection<SearchSettings_Hobby> SearchSettings_Hobby { get; set; }
        public virtual ICollection<SearchSettings_HotFeature> SearchSettings_HotFeature { get; set; }
        public virtual ICollection<SearchSettings_Humor> SearchSettings_Humor { get; set; }
        public virtual ICollection<SearchSettings_IncomeLevel> SearchSettings_IncomeLevel { get; set; }
        public virtual ICollection<SearchSettings_LivingStituation> SearchSettings_LivingStituation { get; set; }
        public virtual ICollection<SearchSettings_Location> SearchSettings_Location { get; set; }
        public virtual ICollection<SearchSettings_LookingFor> SearchSettings_LookingFor { get; set; }
        public virtual ICollection<SearchSettings_MaritalStatus> SearchSettings_MaritalStatus { get; set; }
        public virtual ICollection<SearchSettings_NigerianState> SearchSettings_NigerianState { get; set; }
        public virtual ICollection<SearchSettings_PoliticalView> SearchSettings_PoliticalView { get; set; }
        public virtual ICollection<SearchSettings_Profession> SearchSettings_Profession { get; set; }
        public virtual ICollection<SearchSettings_Religion> SearchSettings_Religion { get; set; }
        public virtual ICollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance { get; set; }
        public virtual ICollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance1 { get; set; }
        public virtual ICollection<SearchSettings_ShowMe> SearchSettings_ShowMe { get; set; }
        public virtual ICollection<SearchSettings_Sign> SearchSettings_Sign { get; set; }
        public virtual ICollection<SearchSettings_Smokes> SearchSettings_Smokes { get; set; }
        public virtual ICollection<SearchSettings_SortByType> SearchSettings_SortByType { get; set; }
        public virtual ICollection<SearchSettings_Tribe> SearchSettings_Tribe { get; set; }
        public virtual ICollection<SearchSettings_WantKids> SearchSettings_WantKids { get; set; }
    }
}
