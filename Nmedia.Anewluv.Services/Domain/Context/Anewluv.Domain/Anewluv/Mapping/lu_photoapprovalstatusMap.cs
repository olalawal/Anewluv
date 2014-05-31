using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_photoapprovalstatusMap : EntityTypeConfiguration<lu_photoapprovalstatus>
    {
        public lu_photoapprovalstatusMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_photoapprovalstatus");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
