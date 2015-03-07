using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_HairColorMap : EntityTypeConfiguration<SearchSettings_HairColor>
    {
        public SearchSettings_HairColorMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_HairColorID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_HairColor");
            this.Property(t => t.SearchSettings_HairColorID).HasColumnName("SearchSettings_HairColorID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.HairColorID).HasColumnName("HairColorID");

            // Relationships
            this.HasOptional(t => t.CriteriaAppearance_HairColor)
                .WithMany(t => t.SearchSettings_HairColor)
                .HasForeignKey(d => d.HairColorID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_HairColor)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
