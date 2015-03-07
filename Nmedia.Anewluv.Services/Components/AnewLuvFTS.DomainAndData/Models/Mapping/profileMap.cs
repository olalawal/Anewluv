using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class profileMap : EntityTypeConfiguration<profile>
    {
        public profileMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileID);

            // Properties
            this.Property(t => t.ProfileIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ScreenName)
                .HasMaxLength(50);

            this.Property(t => t.SecurityAnswer)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("profiles");
            this.Property(t => t.ProfileIndex).HasColumnName("ProfileIndex");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.ForwardMessages).HasColumnName("ForwardMessages");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.ModificationDate).HasColumnName("ModificationDate");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");
            this.Property(t => t.ActivationCode).HasColumnName("ActivationCode");
            this.Property(t => t.ProfileStatusID).HasColumnName("ProfileStatusID");
            this.Property(t => t.ScreenName).HasColumnName("ScreenName");
            this.Property(t => t.SecurityQuestionID).HasColumnName("SecurityQuestionID");
            this.Property(t => t.SecurityAnswer).HasColumnName("SecurityAnswer");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.salt).HasColumnName("salt");
            this.Property(t => t.PasswordChangedDate).HasColumnName("PasswordChangedDate");
            this.Property(t => t.PasswordChangeAttempts).HasColumnName("PasswordChangeAttempts");
            this.Property(t => t.PasswordChangedCount).HasColumnName("PasswordChangedCount");
            this.Property(t => t.ReadTemsOfUse).HasColumnName("ReadTemsOfUse");
            this.Property(t => t.ReadPrivacyStatement).HasColumnName("ReadPrivacyStatement");
            this.Property(t => t.DailySentEmailQuota).HasColumnName("DailySentEmailQuota");
            this.Property(t => t.DailSentMessageQuota).HasColumnName("DailSentMessageQuota");
            this.Property(t => t.SentEmailQuotaHitCount).HasColumnName("SentEmailQuotaHitCount");
            this.Property(t => t.SentMessageQuotaHitCount).HasColumnName("SentMessageQuotaHitCount");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithOptional(t => t.profile);
            this.HasRequired(t => t.profilestatus)
                .WithMany(t => t.profiles)
                .HasForeignKey(d => d.ProfileStatusID);
            this.HasOptional(t => t.SecurityQuestion)
                .WithMany(t => t.profiles)
                .HasForeignKey(d => d.SecurityQuestionID);

        }
    }
}
