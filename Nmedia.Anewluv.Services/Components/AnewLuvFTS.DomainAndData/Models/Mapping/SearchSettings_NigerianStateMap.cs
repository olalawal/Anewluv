using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_NigerianStateMap : EntityTypeConfiguration<SearchSettings_NigerianState>
    {
        public SearchSettings_NigerianStateMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_NigerianStateID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_NigerianState");
            this.Property(t => t.SearchSettings_NigerianStateID).HasColumnName("SearchSettings_NigerianStateID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.NigerianStateID).HasColumnName("NigerianStateID");

            // Relationships
            this.HasOptional(t => t.NigerianState)
                .WithMany(t => t.SearchSettings_NigerianState)
                .HasForeignKey(d => d.NigerianStateID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_NigerianState)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
