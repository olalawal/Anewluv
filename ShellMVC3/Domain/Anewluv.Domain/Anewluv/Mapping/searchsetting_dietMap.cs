using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_dietMap : EntityTypeConfiguration<searchsetting_diet>
    {
        public searchsetting_dietMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_diet");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.diet_id).HasColumnName("diet_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_diet)
                .WithMany(t => t.searchsetting_diet)
                .HasForeignKey(d => d.diet_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_diet)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
