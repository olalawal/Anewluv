using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_EyeColorMap : EntityTypeConfiguration<SearchSettings_EyeColor>
    {
        public SearchSettings_EyeColorMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_EyeColorID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_EyeColor");
            this.Property(t => t.SearchSettings_EyeColorID).HasColumnName("SearchSettings_EyeColorID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.EyeColorID).HasColumnName("EyeColorID");

            // Relationships
            this.HasOptional(t => t.CriteriaAppearance_EyeColor)
                .WithMany(t => t.SearchSettings_EyeColor)
                .HasForeignKey(d => d.EyeColorID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_EyeColor)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
