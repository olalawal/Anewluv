using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_EmploymentStatusMap : EntityTypeConfiguration<SearchSettings_EmploymentStatus>
    {
        public SearchSettings_EmploymentStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_EmploymentStatusID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_EmploymentStatus");
            this.Property(t => t.SearchSettings_EmploymentStatusID).HasColumnName("SearchSettings_EmploymentStatusID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.EmploymentStatusID).HasColumnName("EmploymentStatusID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_EmploymentStatus)
                .WithMany(t => t.SearchSettings_EmploymentStatus)
                .HasForeignKey(d => d.EmploymentStatusID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_EmploymentStatus)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
