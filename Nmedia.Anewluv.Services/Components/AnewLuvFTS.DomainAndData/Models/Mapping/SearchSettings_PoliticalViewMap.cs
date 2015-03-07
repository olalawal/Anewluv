using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_PoliticalViewMap : EntityTypeConfiguration<SearchSettings_PoliticalView>
    {
        public SearchSettings_PoliticalViewMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_PoliticalViewID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_PoliticalView");
            this.Property(t => t.SearchSettings_PoliticalViewID).HasColumnName("SearchSettings_PoliticalViewID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.PoliticalViewID).HasColumnName("PoliticalViewID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_PoliticalView)
                .WithMany(t => t.SearchSettings_PoliticalView)
                .HasForeignKey(d => d.PoliticalViewID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_PoliticalView)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
