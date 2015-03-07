using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileData_LookingForMap : EntityTypeConfiguration<ProfileData_LookingFor>
    {
        public ProfileData_LookingForMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileData_LookingFor1);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileData_LookingFor");
            this.Property(t => t.ProfileData_LookingFor1).HasColumnName("ProfileData_LookingFor");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.LookingForID).HasColumnName("LookingForID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_LookingFor)
                .WithMany(t => t.ProfileData_LookingFor)
                .HasForeignKey(d => d.LookingForID);
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileData_LookingFor)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
