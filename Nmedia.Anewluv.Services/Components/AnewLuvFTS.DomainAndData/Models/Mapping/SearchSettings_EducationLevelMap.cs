using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_EducationLevelMap : EntityTypeConfiguration<SearchSettings_EducationLevel>
    {
        public SearchSettings_EducationLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_EducationLevelID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_EducationLevel");
            this.Property(t => t.SearchSettings_EducationLevelID).HasColumnName("SearchSettings_EducationLevelID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.EducationLevelID).HasColumnName("EducationLevelID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_EducationLevel)
                .WithMany(t => t.SearchSettings_EducationLevel)
                .HasForeignKey(d => d.EducationLevelID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_EducationLevel)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
