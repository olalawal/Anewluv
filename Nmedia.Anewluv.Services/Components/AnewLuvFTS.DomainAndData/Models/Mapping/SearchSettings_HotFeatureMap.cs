using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_HotFeatureMap : EntityTypeConfiguration<SearchSettings_HotFeature>
    {
        public SearchSettings_HotFeatureMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_HotFeatureID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_HotFeature");
            this.Property(t => t.SearchSettings_HotFeatureID).HasColumnName("SearchSettings_HotFeatureID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.HotFeatureID).HasColumnName("HotFeatureID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_HotFeature)
                .WithMany(t => t.SearchSettings_HotFeature)
                .HasForeignKey(d => d.HotFeatureID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_HotFeature)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
