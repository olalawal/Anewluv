using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileDataMap : EntityTypeConfiguration<ProfileData>
    {
        public ProfileDataMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.State_Province)
                .HasMaxLength(255);

            this.Property(t => t.Country_Region)
                .HasMaxLength(50);

            this.Property(t => t.PostalCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.MyCatchyIntroLine)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ProfileData");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.State_Province).HasColumnName("State_Province");
            this.Property(t => t.Country_Region).HasColumnName("Country_Region");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Birthdate).HasColumnName("Birthdate");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.BodyTypeID).HasColumnName("BodyTypeID");
            this.Property(t => t.EyeColorID).HasColumnName("EyeColorID");
            this.Property(t => t.HairColorID).HasColumnName("HairColorID");
            this.Property(t => t.ExerciseID).HasColumnName("ExerciseID");
            this.Property(t => t.ReligionID).HasColumnName("ReligionID");
            this.Property(t => t.ReligiousAttendanceID).HasColumnName("ReligiousAttendanceID");
            this.Property(t => t.DrinksID).HasColumnName("DrinksID");
            this.Property(t => t.SmokesID).HasColumnName("SmokesID");
            this.Property(t => t.HumorID).HasColumnName("HumorID");
            this.Property(t => t.PoliticalViewID).HasColumnName("PoliticalViewID");
            this.Property(t => t.DietID).HasColumnName("DietID");
            this.Property(t => t.SignID).HasColumnName("SignID");
            this.Property(t => t.IncomeLevelID).HasColumnName("IncomeLevelID");
            this.Property(t => t.HaveKidsId).HasColumnName("HaveKidsId");
            this.Property(t => t.WantsKidsID).HasColumnName("WantsKidsID");
            this.Property(t => t.EmploymentSatusID).HasColumnName("EmploymentSatusID");
            this.Property(t => t.EducationLevelID).HasColumnName("EducationLevelID");
            this.Property(t => t.ProfessionID).HasColumnName("ProfessionID");
            this.Property(t => t.MaritalStatusID).HasColumnName("MaritalStatusID");
            this.Property(t => t.LivingSituationID).HasColumnName("LivingSituationID");
            this.Property(t => t.NigerianStateID).HasColumnName("NigerianStateID");
            this.Property(t => t.TribeID).HasColumnName("TribeID");
            this.Property(t => t.AboutMe).HasColumnName("AboutMe");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.MyCatchyIntroLine).HasColumnName("MyCatchyIntroLine");

            // Relationships
            this.HasRequired(t => t.abuser)
                .WithOptional(t => t.ProfileData);
            this.HasOptional(t => t.CriteriaAppearance_Bodytypes)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.BodyTypeID);
            this.HasOptional(t => t.CriteriaAppearance_EyeColor)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.EyeColorID);
            this.HasOptional(t => t.CriteriaAppearance_HairColor)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.HairColorID);
            this.HasOptional(t => t.CriteriaCharacter_Diet)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.DietID);
            this.HasOptional(t => t.CriteriaCharacter_Drinks)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.DrinksID);
            this.HasOptional(t => t.CriteriaCharacter_Exercise)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.ExerciseID);
            this.HasOptional(t => t.CriteriaCharacter_Humor)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.HumorID);
            this.HasOptional(t => t.CriteriaCharacter_PoliticalView)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.PoliticalViewID);
            this.HasOptional(t => t.CriteriaCharacter_Religion)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.ReligionID);
            this.HasOptional(t => t.CriteriaCharacter_ReligiousAttendance)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.ReligiousAttendanceID);
            this.HasOptional(t => t.CriteriaCharacter_Sign)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.SignID);
            this.HasOptional(t => t.CriteriaCharacter_Smokes)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.SmokesID);
            this.HasOptional(t => t.CriteriaLife_EducationLevel)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.EducationLevelID);
            this.HasOptional(t => t.CriteriaLife_EmploymentStatus)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.EmploymentSatusID);
            this.HasOptional(t => t.CriteriaLife_HaveKids)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.HaveKidsId);
            this.HasOptional(t => t.CriteriaLife_IncomeLevel)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.IncomeLevelID);
            this.HasOptional(t => t.CriteriaLife_LivingSituation)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.LivingSituationID);
            this.HasOptional(t => t.CriteriaLife_MaritalStatus)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.MaritalStatusID);
            this.HasOptional(t => t.CriteriaLife_Profession)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.ProfessionID);
            this.HasOptional(t => t.CriteriaLife_WantsKids)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.WantsKidsID);
            this.HasRequired(t => t.gender)
                .WithMany(t => t.ProfileDatas)
                .HasForeignKey(d => d.GenderID);

        }
    }
}
