using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_ReligiousAttendanceMap : EntityTypeConfiguration<CriteriaCharacter_ReligiousAttendance>
    {
        public CriteriaCharacter_ReligiousAttendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.ReligiousAttendanceID);

            // Properties
            this.Property(t => t.ReligiousAttendanceName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_ReligiousAttendance");
            this.Property(t => t.ReligiousAttendanceID).HasColumnName("ReligiousAttendanceID");
            this.Property(t => t.ReligiousAttendanceName).HasColumnName("ReligiousAttendanceName");
        }
    }
}
