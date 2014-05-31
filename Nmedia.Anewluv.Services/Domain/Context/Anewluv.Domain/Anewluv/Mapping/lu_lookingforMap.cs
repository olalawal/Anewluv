using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_lookingforMap : EntityTypeConfiguration<lu_lookingfor>
    {
        public lu_lookingforMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_lookingfor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
