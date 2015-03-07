using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class User_LogtimeMap : EntityTypeConfiguration<User_Logtime>
    {
        public User_LogtimeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.SessionID)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User_Logtime");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.LoginTime).HasColumnName("LoginTime");
            this.Property(t => t.LogoutTime).HasColumnName("LogoutTime");
            this.Property(t => t.Offline).HasColumnName("Offline");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.User_Logtime)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
