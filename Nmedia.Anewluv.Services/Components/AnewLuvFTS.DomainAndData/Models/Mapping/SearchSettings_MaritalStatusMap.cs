using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_MaritalStatusMap : EntityTypeConfiguration<SearchSettings_MaritalStatus>
    {
        public SearchSettings_MaritalStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_MaritalStatusID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_MaritalStatus");
            this.Property(t => t.SearchSettings_MaritalStatusID).HasColumnName("SearchSettings_MaritalStatusID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.MaritalStatusID).HasColumnName("MaritalStatusID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_MaritalStatus)
                .WithMany(t => t.SearchSettings_MaritalStatus)
                .HasForeignKey(d => d.MaritalStatusID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_MaritalStatus)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
