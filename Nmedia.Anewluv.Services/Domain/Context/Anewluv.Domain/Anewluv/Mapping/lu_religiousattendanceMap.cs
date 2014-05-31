using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_religiousattendanceMap : EntityTypeConfiguration<lu_religiousattendance>
    {
        public lu_religiousattendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_religiousattendance");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
