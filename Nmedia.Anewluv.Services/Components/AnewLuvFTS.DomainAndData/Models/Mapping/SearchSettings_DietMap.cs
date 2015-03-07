using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_DietMap : EntityTypeConfiguration<SearchSettings_Diet>
    {
        public SearchSettings_DietMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_DietID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Diet");
            this.Property(t => t.SearchSettings_DietID).HasColumnName("SearchSettings_DietID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.DietID).HasColumnName("DietID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Diet)
                .WithMany(t => t.SearchSettings_Diet)
                .HasForeignKey(d => d.DietID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Diet)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
