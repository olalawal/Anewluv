using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_ShowMeMap : EntityTypeConfiguration<SearchSettings_ShowMe>
    {
        public SearchSettings_ShowMeMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_ShowMeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_ShowMe");
            this.Property(t => t.SearchSettings_ShowMeID).HasColumnName("SearchSettings_ShowMeID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ShowMeID).HasColumnName("ShowMeID");

            // Relationships
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_ShowMe)
                .HasForeignKey(d => d.SearchSettingsID);
            this.HasOptional(t => t.ShowMe)
                .WithMany(t => t.SearchSettings_ShowMe)
                .HasForeignKey(d => d.ShowMeID);

        }
    }
}
