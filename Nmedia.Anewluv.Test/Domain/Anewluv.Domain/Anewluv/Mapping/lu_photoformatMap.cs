using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_photoformatMap : EntityTypeConfiguration<lu_photoformat>
    {
        public lu_photoformatMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_photoformat");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.photoImagersizerformat_id).HasColumnName("photoImagersizerformat_id");

            // Relationships
            this.HasRequired(t => t.lu_photoImagersizerformat)
                .WithMany(t => t.lu_photoformat)
                .HasForeignKey(d => d.photoImagersizerformat_id);

        }
    }
}
