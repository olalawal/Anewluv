using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_ethnicityMap : EntityTypeConfiguration<searchsetting_ethnicity>
    {
        public searchsetting_ethnicityMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_ethnicity");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ethnicity_id).HasColumnName("ethnicity_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_ethnicity)
                .WithMany(t => t.searchsetting_ethnicity)
                .HasForeignKey(d => d.ethnicity_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_ethnicity)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
