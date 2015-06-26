using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_iconformatMap : EntityTypeConfiguration<lu_iconformat>
    {
        public lu_iconformatMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_iconformat");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.iconImagersizerformat_id).HasColumnName("iconImagersizerformat_id");
            this.Property(t => t.iconImageresizerformat_id).HasColumnName("iconImageresizerformat_id");

            // Relationships
            this.HasOptional(t => t.lu_iconImagersizerformat)
                .WithMany(t => t.lu_iconformat)
                .HasForeignKey(d => d.iconImageresizerformat_id);

        }
    }
}
