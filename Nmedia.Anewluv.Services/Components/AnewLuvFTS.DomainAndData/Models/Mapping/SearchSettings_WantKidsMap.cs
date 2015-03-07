using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_WantKidsMap : EntityTypeConfiguration<SearchSettings_WantKids>
    {
        public SearchSettings_WantKidsMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_WantKidsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_WantKids");
            this.Property(t => t.SearchSettings_WantKidsID).HasColumnName("SearchSettings_WantKidsID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.WantKidsID).HasColumnName("WantKidsID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_WantsKids)
                .WithMany(t => t.SearchSettings_WantKids)
                .HasForeignKey(d => d.WantKidsID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_WantKids)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
