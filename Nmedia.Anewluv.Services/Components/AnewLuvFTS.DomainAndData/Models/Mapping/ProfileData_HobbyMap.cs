using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileData_HobbyMap : EntityTypeConfiguration<ProfileData_Hobby>
    {
        public ProfileData_HobbyMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileData_Hobby1);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileData_Hobby");
            this.Property(t => t.ProfileData_Hobby1).HasColumnName("ProfileData_Hobby");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.HobbyID).HasColumnName("HobbyID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Hobby)
                .WithMany(t => t.ProfileData_Hobby)
                .HasForeignKey(d => d.HobbyID);
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileData_Hobby)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
