using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_TribeMap : EntityTypeConfiguration<SearchSettings_Tribe>
    {
        public SearchSettings_TribeMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_TribeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Tribe");
            this.Property(t => t.SearchSettings_TribeID).HasColumnName("SearchSettings_TribeID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.TribeID).HasColumnName("TribeID");

            // Relationships
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Tribe)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
