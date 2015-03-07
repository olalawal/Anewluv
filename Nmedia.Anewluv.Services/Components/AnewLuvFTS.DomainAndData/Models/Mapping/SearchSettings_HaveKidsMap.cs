using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_HaveKidsMap : EntityTypeConfiguration<SearchSettings_HaveKids>
    {
        public SearchSettings_HaveKidsMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_HaveKidsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_HaveKids");
            this.Property(t => t.SearchSettings_HaveKidsID).HasColumnName("SearchSettings_HaveKidsID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.HaveKidsID).HasColumnName("HaveKidsID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_HaveKids)
                .WithMany(t => t.SearchSettings_HaveKids)
                .HasForeignKey(d => d.HaveKidsID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_HaveKids)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
