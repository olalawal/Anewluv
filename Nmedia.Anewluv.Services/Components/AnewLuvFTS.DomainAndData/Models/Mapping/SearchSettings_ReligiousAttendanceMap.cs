using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_ReligiousAttendanceMap : EntityTypeConfiguration<SearchSettings_ReligiousAttendance>
    {
        public SearchSettings_ReligiousAttendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_ReligiousAttendanceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_ReligiousAttendance");
            this.Property(t => t.SearchSettings_ReligiousAttendanceID).HasColumnName("SearchSettings_ReligiousAttendanceID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ReligiousAttendanceID).HasColumnName("ReligiousAttendanceID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_ReligiousAttendance)
                .WithMany(t => t.SearchSettings_ReligiousAttendance)
                .HasForeignKey(d => d.ReligiousAttendanceID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_ReligiousAttendance)
                .HasForeignKey(d => d.SearchSettingsID);
            this.HasOptional(t => t.SearchSetting1)
                .WithMany(t => t.SearchSettings_ReligiousAttendance1)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
