using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_livingsituationMap : EntityTypeConfiguration<lu_livingsituation>
    {
        public lu_livingsituationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_livingsituation");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
