using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photoconversionMap : EntityTypeConfiguration<photoconversion>
    {
        public photoconversionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photoconversions");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.photo_id).HasColumnName("photo_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.image).HasColumnName("image");
            this.Property(t => t.size).HasColumnName("size");
            this.Property(t => t.formattype_id).HasColumnName("formattype_id");

            // Relationships
            this.HasRequired(t => t.lu_photoformat)
                .WithMany(t => t.photoconversions)
                .HasForeignKey(d => d.formattype_id);
            this.HasRequired(t => t.photo)
                .WithMany(t => t.photoconversions)
                .HasForeignKey(d => d.photo_id);

        }
    }
}
