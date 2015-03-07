using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_GendersMap : EntityTypeConfiguration<SearchSettings_Genders>
    {
        public SearchSettings_GendersMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_GenderID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Genders");
            this.Property(t => t.SearchSettings_GenderID).HasColumnName("SearchSettings_GenderID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.GenderID).HasColumnName("GenderID");

            // Relationships
            this.HasOptional(t => t.gender)
                .WithMany(t => t.SearchSettings_Genders)
                .HasForeignKey(d => d.GenderID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Genders)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
