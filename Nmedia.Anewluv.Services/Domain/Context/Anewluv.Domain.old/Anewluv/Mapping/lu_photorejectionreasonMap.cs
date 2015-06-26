using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_photorejectionreasonMap : EntityTypeConfiguration<lu_photorejectionreason>
    {
        public lu_photorejectionreasonMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_photorejectionreason");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.userMessage).HasColumnName("userMessage");
        }
    }
}
