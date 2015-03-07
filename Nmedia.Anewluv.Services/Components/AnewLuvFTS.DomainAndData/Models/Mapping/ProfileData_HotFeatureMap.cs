using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileData_HotFeatureMap : EntityTypeConfiguration<ProfileData_HotFeature>
    {
        public ProfileData_HotFeatureMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileData_HotFeature1);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileData_HotFeature");
            this.Property(t => t.ProfileData_HotFeature1).HasColumnName("ProfileData_HotFeature");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.HotFeatureID).HasColumnName("HotFeatureID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_HotFeature)
                .WithMany(t => t.ProfileData_HotFeature)
                .HasForeignKey(d => d.HotFeatureID);
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileData_HotFeature)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
