using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_hotfeatureMap : EntityTypeConfiguration<searchsetting_hotfeature>
    {
        public searchsetting_hotfeatureMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_hotfeature");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.hotfeature_id).HasColumnName("hotfeature_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_hotfeature)
                .WithMany(t => t.searchsetting_hotfeature)
                .HasForeignKey(d => d.hotfeature_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_hotfeature)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
