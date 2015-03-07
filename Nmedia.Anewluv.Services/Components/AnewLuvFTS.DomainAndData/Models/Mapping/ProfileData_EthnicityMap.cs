using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileData_EthnicityMap : EntityTypeConfiguration<ProfileData_Ethnicity>
    {
        public ProfileData_EthnicityMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileData_Ethnicity1);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileData_Ethnicity");
            this.Property(t => t.ProfileData_Ethnicity1).HasColumnName("ProfileData_Ethnicity");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.EthnicityID).HasColumnName("EthnicityID");

            // Relationships
            this.HasOptional(t => t.CriteriaAppearance_Ethnicity)
                .WithMany(t => t.ProfileData_Ethnicity)
                .HasForeignKey(d => d.EthnicityID);
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileData_Ethnicity)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
