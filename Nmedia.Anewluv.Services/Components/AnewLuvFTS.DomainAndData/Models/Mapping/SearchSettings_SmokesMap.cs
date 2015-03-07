using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_SmokesMap : EntityTypeConfiguration<SearchSettings_Smokes>
    {
        public SearchSettings_SmokesMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_SmokesID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Smokes");
            this.Property(t => t.SearchSettings_SmokesID).HasColumnName("SearchSettings_SmokesID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.SmokesID).HasColumnName("SmokesID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Smokes)
                .WithMany(t => t.SearchSettings_Smokes)
                .HasForeignKey(d => d.SmokesID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Smokes)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
