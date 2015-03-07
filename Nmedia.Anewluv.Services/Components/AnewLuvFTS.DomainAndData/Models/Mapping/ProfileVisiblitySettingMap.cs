using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileVisiblitySettingMap : EntityTypeConfiguration<ProfileVisiblitySetting>
    {
        public ProfileVisiblitySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileVisiblitySettings");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.MailPeeks).HasColumnName("MailPeeks");
            this.Property(t => t.MailIntrests).HasColumnName("MailIntrests");
            this.Property(t => t.MailLikes).HasColumnName("MailLikes");
            this.Property(t => t.MailMatches).HasColumnName("MailMatches");
            this.Property(t => t.MailNews).HasColumnName("MailNews");
            this.Property(t => t.ProfileVisiblity).HasColumnName("ProfileVisiblity");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.SteathPeeks).HasColumnName("SteathPeeks");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
            this.Property(t => t.ChatVisiblityToLikes).HasColumnName("ChatVisiblityToLikes");
            this.Property(t => t.ChatVisiblityToInterests).HasColumnName("ChatVisiblityToInterests");
            this.Property(t => t.ChatVisiblityToMatches).HasColumnName("ChatVisiblityToMatches");
            this.Property(t => t.ChatVisiblityToPeeks).HasColumnName("ChatVisiblityToPeeks");
            this.Property(t => t.ChatVisiblityToSearch).HasColumnName("ChatVisiblityToSearch");
            this.Property(t => t.MailChatRequest).HasColumnName("MailChatRequest");
            this.Property(t => t.SaveOfflineChat).HasColumnName("SaveOfflineChat");
            this.Property(t => t.AgeMinVisibility).HasColumnName("AgeMinVisibility");
            this.Property(t => t.AgeMaxVisibility).HasColumnName("AgeMaxVisibility");

            // Relationships
            this.HasOptional(t => t.gender)
                .WithMany(t => t.ProfileVisiblitySettings)
                .HasForeignKey(d => d.GenderID);
            this.HasRequired(t => t.ProfileData)
                .WithOptional(t => t.ProfileVisiblitySetting);

        }
    }
}
