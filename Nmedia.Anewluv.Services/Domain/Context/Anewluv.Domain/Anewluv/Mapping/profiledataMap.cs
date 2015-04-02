using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profiledataMap : EntityTypeConfiguration<profiledata>
    {
        public profiledataMap()
        {
            // Primary Key
            this.HasKey(t => t.profile_id);

            // Properties
            this.Property(t => t.profile_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("profiledatas"); //to do change to singular
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.age).HasColumnName("age");
            this.Property(t => t.birthdate).HasColumnName("birthdate");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.countryregion).HasColumnName("countryregion");
            this.Property(t => t.stateprovince).HasColumnName("stateprovince");
            this.Property(t => t.countryid).HasColumnName("countryid");
            this.Property(t => t.longitude).HasColumnName("longitude");
            this.Property(t => t.latitude).HasColumnName("latitude");
            this.Property(t => t.aboutme).HasColumnName("aboutme");
            this.Property(t => t.height).HasColumnName("height");
            this.Property(t => t.mycatchyintroLine).HasColumnName("mycatchyintroLine");
            this.Property(t => t.phone).HasColumnName("phone");
            this.Property(t => t.postalcode).HasColumnName("postalcode");
            this.Property(t => t.profilemetadata_profile_id).HasColumnName("profilemetadata_profile_id");
            this.Property(t => t.visibilitysettings_id).HasColumnName("visibilitysettings_id");
            this.Property(t => t.gender_id).HasColumnName("gender_id");
            this.Property(t => t.bodytype_id).HasColumnName("bodytype_id");
            this.Property(t => t.eyecolor_id).HasColumnName("eyecolor_id");
            this.Property(t => t.haircolor_id).HasColumnName("haircolor_id");
            this.Property(t => t.diet_id).HasColumnName("diet_id");
            this.Property(t => t.drinking_id).HasColumnName("drinking_id");
            this.Property(t => t.exercise_id).HasColumnName("exercise_id");
            this.Property(t => t.humor_id).HasColumnName("humor_id");
            this.Property(t => t.politicalview_id).HasColumnName("politicalview_id");
            this.Property(t => t.religion_id).HasColumnName("religion_id");
            this.Property(t => t.religiousattendance_id).HasColumnName("religiousattendance_id");
            this.Property(t => t.sign_id).HasColumnName("sign_id");
            this.Property(t => t.smoking_id).HasColumnName("smoking_id");
            this.Property(t => t.educationlevel_id).HasColumnName("educationlevel_id");
            this.Property(t => t.employmentstatus_id).HasColumnName("employmentstatus_id");
            this.Property(t => t.kidstatus_id).HasColumnName("kidstatus_id");
            this.Property(t => t.incomelevel_id).HasColumnName("incomelevel_id");
            this.Property(t => t.livingsituation_id).HasColumnName("livingsituation_id");
            this.Property(t => t.maritalstatus_id).HasColumnName("maritalstatus_id");
            this.Property(t => t.profession_id).HasColumnName("profession_id");
            this.Property(t => t.wantsKidstatus_id).HasColumnName("wantsKidstatus_id");

           // this.HasRequired(t => t.profilemetadata).WithRequiredDependent(z => z.profiledata);
            
            this.HasRequired(t => t.profile).WithOptional(z => z.profiledata);

            this.HasMany(t => t.visiblitysettings).WithRequired
             (t => t.profiledata)
             .HasForeignKey(d => d.profile_id);


            //   .(t => t.profiledatas)
            //    .HasForeignKey(d => d.profilemetadata_profile_id);

            // Relationships
            this.HasOptional(t => t.lu_bodytype)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.bodytype_id);
            this.HasOptional(t => t.lu_diet)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.diet_id);
            this.HasOptional(t => t.lu_drinks)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.drinking_id);
            this.HasOptional(t => t.lu_educationlevel)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.educationlevel_id);
            this.HasOptional(t => t.lu_employmentstatus)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.employmentstatus_id);
            this.HasOptional(t => t.lu_exercise)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.exercise_id);
            this.HasOptional(t => t.lu_eyecolor)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.eyecolor_id);
            this.HasOptional(t => t.lu_gender)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.gender_id);
            this.HasOptional(t => t.lu_haircolor)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.haircolor_id);
            this.HasOptional(t => t.lu_havekids)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.kidstatus_id);
            this.HasOptional(t => t.lu_humor)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.humor_id);
            this.HasOptional(t => t.lu_incomelevel)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.incomelevel_id);
            this.HasOptional(t => t.lu_livingsituation)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.livingsituation_id);
            this.HasOptional(t => t.lu_maritalstatus)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.maritalstatus_id);
            this.HasOptional(t => t.lu_politicalview)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.politicalview_id);
            this.HasOptional(t => t.lu_profession)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.profession_id);
            this.HasOptional(t => t.lu_religion)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.religion_id);
            this.HasOptional(t => t.lu_religiousattendance)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.religiousattendance_id);
            this.HasOptional(t => t.lu_sign)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.sign_id);
            this.HasOptional(t => t.lu_smokes)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.smoking_id);
            this.HasOptional(t => t.lu_wantskids)
                .WithMany(t => t.profiledatas)
                .HasForeignKey(d => d.wantsKidstatus_id);
       
         
         

        }
    }
}
