using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_ReligionMap : EntityTypeConfiguration<SearchSettings_Religion>
    {
        public SearchSettings_ReligionMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_ReligionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Religion");
            this.Property(t => t.SearchSettings_ReligionID).HasColumnName("SearchSettings_ReligionID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ReligionID).HasColumnName("ReligionID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Religion)
                .WithMany(t => t.SearchSettings_Religion)
                .HasForeignKey(d => d.ReligionID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Religion)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
