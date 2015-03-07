using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AnewLuvFTS.DomainAndData.Models.Mapping;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class AnewluvFTSContext : DbContext
    {
        static AnewluvFTSContext()
        {
            Database.SetInitializer<AnewluvFTSContext>(null);
        }

        public AnewluvFTSContext()
            : base("Name=AnewluvFTSContext")
        {
        }

        public DbSet<abuser> abusers { get; set; }
        public DbSet<abusereport> abusereports { get; set; }
        public DbSet<abusetype> abusetypes { get; set; }
        public DbSet<CommunicationQuota> CommunicationQuotas { get; set; }
        public DbSet<CriteriaAppearance_Bodytypes> CriteriaAppearance_Bodytypes { get; set; }
        public DbSet<CriteriaAppearance_Ethnicity> CriteriaAppearance_Ethnicity { get; set; }
        public DbSet<CriteriaAppearance_EyeColor> CriteriaAppearance_EyeColor { get; set; }
        public DbSet<CriteriaAppearance_HairColor> CriteriaAppearance_HairColor { get; set; }
        public DbSet<CriteriaCharacter_Diet> CriteriaCharacter_Diet { get; set; }
        public DbSet<CriteriaCharacter_Drinks> CriteriaCharacter_Drinks { get; set; }
        public DbSet<CriteriaCharacter_Exercise> CriteriaCharacter_Exercise { get; set; }
        public DbSet<CriteriaCharacter_Hobby> CriteriaCharacter_Hobby { get; set; }
        public DbSet<CriteriaCharacter_HotFeature> CriteriaCharacter_HotFeature { get; set; }
        public DbSet<CriteriaCharacter_Humor> CriteriaCharacter_Humor { get; set; }
        public DbSet<CriteriaCharacter_PoliticalView> CriteriaCharacter_PoliticalView { get; set; }
        public DbSet<CriteriaCharacter_Religion> CriteriaCharacter_Religion { get; set; }
        public DbSet<CriteriaCharacter_ReligiousAttendance> CriteriaCharacter_ReligiousAttendance { get; set; }
        public DbSet<CriteriaCharacter_Sign> CriteriaCharacter_Sign { get; set; }
        public DbSet<CriteriaCharacter_Smokes> CriteriaCharacter_Smokes { get; set; }
        public DbSet<CriteriaLife_EducationLevel> CriteriaLife_EducationLevel { get; set; }
        public DbSet<CriteriaLife_EmploymentStatus> CriteriaLife_EmploymentStatus { get; set; }
        public DbSet<CriteriaLife_HaveKids> CriteriaLife_HaveKids { get; set; }
        public DbSet<CriteriaLife_IncomeLevel> CriteriaLife_IncomeLevel { get; set; }
        public DbSet<CriteriaLife_LivingSituation> CriteriaLife_LivingSituation { get; set; }
        public DbSet<CriteriaLife_LookingFor> CriteriaLife_LookingFor { get; set; }
        public DbSet<CriteriaLife_MaritalStatus> CriteriaLife_MaritalStatus { get; set; }
        public DbSet<CriteriaLife_Profession> CriteriaLife_Profession { get; set; }
        public DbSet<CriteriaLife_WantsKids> CriteriaLife_WantsKids { get; set; }
        public DbSet<databaseerror> databaseerrors { get; set; }
        public DbSet<emailerror> emailerrors { get; set; }
        public DbSet<favorite> favorites { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<gender> genders { get; set; }
        public DbSet<Height> Heights { get; set; }
        public DbSet<Hotlist> Hotlists { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Mailboxblock> Mailboxblocks { get; set; }
        public DbSet<MailboxFolder> MailboxFolders { get; set; }
        public DbSet<MailboxFolderType> MailboxFolderTypes { get; set; }
        public DbSet<MailboxMessage> MailboxMessages { get; set; }
        public DbSet<MailboxMessagesFolder> MailboxMessagesFolders { get; set; }
        public DbSet<MembersInRole> MembersInRoles { get; set; }
        public DbSet<NigerianState> NigerianStates { get; set; }
        public DbSet<photo> photos { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<PhotoRejectionReason> PhotoRejectionReasons { get; set; }
        public DbSet<PhotoReviewStatu> PhotoReviewStatus { get; set; }
        public DbSet<PhotoStatu> PhotoStatus { get; set; }
        public DbSet<PhotoType> PhotoTypes { get; set; }
        public DbSet<ProfileData> ProfileDatas { get; set; }
        public DbSet<ProfileData_Ethnicity> ProfileData_Ethnicity { get; set; }
        public DbSet<ProfileData_Hobby> ProfileData_Hobby { get; set; }
        public DbSet<ProfileData_HotFeature> ProfileData_HotFeature { get; set; }
        public DbSet<ProfileData_LookingFor> ProfileData_LookingFor { get; set; }
        public DbSet<ProfileEmailUpdateFreqency> ProfileEmailUpdateFreqencies { get; set; }
        public DbSet<ProfileGeoDataLogger> ProfileGeoDataLoggers { get; set; }
        public DbSet<profileOpenIDStore> profileOpenIDStores { get; set; }
        public DbSet<ProfileRating> ProfileRatings { get; set; }
        public DbSet<ProfileRatingTracker> ProfileRatingTrackers { get; set; }
        public DbSet<profile> profiles { get; set; }
        public DbSet<profilestatus> profilestatuses { get; set; }
        public DbSet<ProfileView> ProfileViews { get; set; }
        public DbSet<ProfileVisiblitySetting> ProfileVisiblitySettings { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SearchSetting> SearchSettings { get; set; }
        public DbSet<SearchSettings_BodyTypes> SearchSettings_BodyTypes { get; set; }
        public DbSet<SearchSettings_Diet> SearchSettings_Diet { get; set; }
        public DbSet<SearchSettings_Drinks> SearchSettings_Drinks { get; set; }
        public DbSet<SearchSettings_EducationLevel> SearchSettings_EducationLevel { get; set; }
        public DbSet<SearchSettings_EmploymentStatus> SearchSettings_EmploymentStatus { get; set; }
        public DbSet<SearchSettings_Ethnicity> SearchSettings_Ethnicity { get; set; }
        public DbSet<SearchSettings_Exercise> SearchSettings_Exercise { get; set; }
        public DbSet<SearchSettings_EyeColor> SearchSettings_EyeColor { get; set; }
        public DbSet<SearchSettings_Genders> SearchSettings_Genders { get; set; }
        public DbSet<SearchSettings_HairColor> SearchSettings_HairColor { get; set; }
        public DbSet<SearchSettings_HaveKids> SearchSettings_HaveKids { get; set; }
        public DbSet<SearchSettings_Hobby> SearchSettings_Hobby { get; set; }
        public DbSet<SearchSettings_HotFeature> SearchSettings_HotFeature { get; set; }
        public DbSet<SearchSettings_Humor> SearchSettings_Humor { get; set; }
        public DbSet<SearchSettings_IncomeLevel> SearchSettings_IncomeLevel { get; set; }
        public DbSet<SearchSettings_LivingStituation> SearchSettings_LivingStituation { get; set; }
        public DbSet<SearchSettings_Location> SearchSettings_Location { get; set; }
        public DbSet<SearchSettings_LookingFor> SearchSettings_LookingFor { get; set; }
        public DbSet<SearchSettings_MaritalStatus> SearchSettings_MaritalStatus { get; set; }
        public DbSet<SearchSettings_NigerianState> SearchSettings_NigerianState { get; set; }
        public DbSet<SearchSettings_PoliticalView> SearchSettings_PoliticalView { get; set; }
        public DbSet<SearchSettings_Profession> SearchSettings_Profession { get; set; }
        public DbSet<SearchSettings_Religion> SearchSettings_Religion { get; set; }
        public DbSet<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance { get; set; }
        public DbSet<SearchSettings_ShowMe> SearchSettings_ShowMe { get; set; }
        public DbSet<SearchSettings_Sign> SearchSettings_Sign { get; set; }
        public DbSet<SearchSettings_Smokes> SearchSettings_Smokes { get; set; }
        public DbSet<SearchSettings_SortByType> SearchSettings_SortByType { get; set; }
        public DbSet<SearchSettings_Tribe> SearchSettings_Tribe { get; set; }
        public DbSet<SearchSettings_WantKids> SearchSettings_WantKids { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<ShowMe> ShowMes { get; set; }
        public DbSet<SortByType> SortByTypes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<SystemImage> SystemImages { get; set; }
        public DbSet<SystemPageSetting> SystemPageSettings { get; set; }
        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<User_Logtime> User_Logtime { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new abuserMap());
            modelBuilder.Configurations.Add(new abusereportMap());
            modelBuilder.Configurations.Add(new abusetypeMap());
            modelBuilder.Configurations.Add(new CommunicationQuotaMap());
            modelBuilder.Configurations.Add(new CriteriaAppearance_BodytypesMap());
            modelBuilder.Configurations.Add(new CriteriaAppearance_EthnicityMap());
            modelBuilder.Configurations.Add(new CriteriaAppearance_EyeColorMap());
            modelBuilder.Configurations.Add(new CriteriaAppearance_HairColorMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_DietMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_DrinksMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_ExerciseMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_HobbyMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_HotFeatureMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_HumorMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_PoliticalViewMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_ReligionMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_ReligiousAttendanceMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_SignMap());
            modelBuilder.Configurations.Add(new CriteriaCharacter_SmokesMap());
            modelBuilder.Configurations.Add(new CriteriaLife_EducationLevelMap());
            modelBuilder.Configurations.Add(new CriteriaLife_EmploymentStatusMap());
            modelBuilder.Configurations.Add(new CriteriaLife_HaveKidsMap());
            modelBuilder.Configurations.Add(new CriteriaLife_IncomeLevelMap());
            modelBuilder.Configurations.Add(new CriteriaLife_LivingSituationMap());
            modelBuilder.Configurations.Add(new CriteriaLife_LookingForMap());
            modelBuilder.Configurations.Add(new CriteriaLife_MaritalStatusMap());
            modelBuilder.Configurations.Add(new CriteriaLife_ProfessionMap());
            modelBuilder.Configurations.Add(new CriteriaLife_WantsKidsMap());
            modelBuilder.Configurations.Add(new databaseerrorMap());
            modelBuilder.Configurations.Add(new emailerrorMap());
            modelBuilder.Configurations.Add(new favoriteMap());
            modelBuilder.Configurations.Add(new FriendMap());
            modelBuilder.Configurations.Add(new genderMap());
            modelBuilder.Configurations.Add(new HeightMap());
            modelBuilder.Configurations.Add(new HotlistMap());
            modelBuilder.Configurations.Add(new InterestMap());
            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new MailboxblockMap());
            modelBuilder.Configurations.Add(new MailboxFolderMap());
            modelBuilder.Configurations.Add(new MailboxFolderTypeMap());
            modelBuilder.Configurations.Add(new MailboxMessageMap());
            modelBuilder.Configurations.Add(new MailboxMessagesFolderMap());
            modelBuilder.Configurations.Add(new MembersInRoleMap());
            modelBuilder.Configurations.Add(new NigerianStateMap());
            modelBuilder.Configurations.Add(new photoMap());
            modelBuilder.Configurations.Add(new PhotoAlbumMap());
            modelBuilder.Configurations.Add(new PhotoRejectionReasonMap());
            modelBuilder.Configurations.Add(new PhotoReviewStatuMap());
            modelBuilder.Configurations.Add(new PhotoStatuMap());
            modelBuilder.Configurations.Add(new PhotoTypeMap());
            modelBuilder.Configurations.Add(new ProfileDataMap());
            modelBuilder.Configurations.Add(new ProfileData_EthnicityMap());
            modelBuilder.Configurations.Add(new ProfileData_HobbyMap());
            modelBuilder.Configurations.Add(new ProfileData_HotFeatureMap());
            modelBuilder.Configurations.Add(new ProfileData_LookingForMap());
            modelBuilder.Configurations.Add(new ProfileEmailUpdateFreqencyMap());
            modelBuilder.Configurations.Add(new ProfileGeoDataLoggerMap());
            modelBuilder.Configurations.Add(new profileOpenIDStoreMap());
            modelBuilder.Configurations.Add(new ProfileRatingMap());
            modelBuilder.Configurations.Add(new ProfileRatingTrackerMap());
            modelBuilder.Configurations.Add(new profileMap());
            modelBuilder.Configurations.Add(new profilestatusMap());
            modelBuilder.Configurations.Add(new ProfileViewMap());
            modelBuilder.Configurations.Add(new ProfileVisiblitySettingMap());
            modelBuilder.Configurations.Add(new RatingMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SearchSettingMap());
            modelBuilder.Configurations.Add(new SearchSettings_BodyTypesMap());
            modelBuilder.Configurations.Add(new SearchSettings_DietMap());
            modelBuilder.Configurations.Add(new SearchSettings_DrinksMap());
            modelBuilder.Configurations.Add(new SearchSettings_EducationLevelMap());
            modelBuilder.Configurations.Add(new SearchSettings_EmploymentStatusMap());
            modelBuilder.Configurations.Add(new SearchSettings_EthnicityMap());
            modelBuilder.Configurations.Add(new SearchSettings_ExerciseMap());
            modelBuilder.Configurations.Add(new SearchSettings_EyeColorMap());
            modelBuilder.Configurations.Add(new SearchSettings_GendersMap());
            modelBuilder.Configurations.Add(new SearchSettings_HairColorMap());
            modelBuilder.Configurations.Add(new SearchSettings_HaveKidsMap());
            modelBuilder.Configurations.Add(new SearchSettings_HobbyMap());
            modelBuilder.Configurations.Add(new SearchSettings_HotFeatureMap());
            modelBuilder.Configurations.Add(new SearchSettings_HumorMap());
            modelBuilder.Configurations.Add(new SearchSettings_IncomeLevelMap());
            modelBuilder.Configurations.Add(new SearchSettings_LivingStituationMap());
            modelBuilder.Configurations.Add(new SearchSettings_LocationMap());
            modelBuilder.Configurations.Add(new SearchSettings_LookingForMap());
            modelBuilder.Configurations.Add(new SearchSettings_MaritalStatusMap());
            modelBuilder.Configurations.Add(new SearchSettings_NigerianStateMap());
            modelBuilder.Configurations.Add(new SearchSettings_PoliticalViewMap());
            modelBuilder.Configurations.Add(new SearchSettings_ProfessionMap());
            modelBuilder.Configurations.Add(new SearchSettings_ReligionMap());
            modelBuilder.Configurations.Add(new SearchSettings_ReligiousAttendanceMap());
            modelBuilder.Configurations.Add(new SearchSettings_ShowMeMap());
            modelBuilder.Configurations.Add(new SearchSettings_SignMap());
            modelBuilder.Configurations.Add(new SearchSettings_SmokesMap());
            modelBuilder.Configurations.Add(new SearchSettings_SortByTypeMap());
            modelBuilder.Configurations.Add(new SearchSettings_TribeMap());
            modelBuilder.Configurations.Add(new SearchSettings_WantKidsMap());
            modelBuilder.Configurations.Add(new SecurityQuestionMap());
            modelBuilder.Configurations.Add(new ShowMeMap());
            modelBuilder.Configurations.Add(new SortByTypeMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new SystemImageMap());
            modelBuilder.Configurations.Add(new SystemPageSettingMap());
            modelBuilder.Configurations.Add(new TribeMap());
            modelBuilder.Configurations.Add(new User_LogtimeMap());
        }
    }
}
