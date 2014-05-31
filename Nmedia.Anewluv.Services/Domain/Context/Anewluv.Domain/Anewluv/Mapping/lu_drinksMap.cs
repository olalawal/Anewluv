using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_drinksMap : EntityTypeConfiguration<lu_drinks>
    {
        public lu_drinksMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_drinks");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
