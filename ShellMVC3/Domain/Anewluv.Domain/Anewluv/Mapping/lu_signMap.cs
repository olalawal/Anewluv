using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_signMap : EntityTypeConfiguration<lu_sign>
    {
        public lu_signMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_sign");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.month).HasColumnName("month");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
