using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_HotFeatureMap : EntityTypeConfiguration<CriteriaCharacter_HotFeature>
    {
        public CriteriaCharacter_HotFeatureMap()
        {
            // Primary Key
            this.HasKey(t => t.HotFeatureID);

            // Properties
            this.Property(t => t.HotFeatureName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_HotFeature");
            this.Property(t => t.HotFeatureID).HasColumnName("HotFeatureID");
            this.Property(t => t.HotFeatureName).HasColumnName("HotFeatureName");
        }
    }
}
