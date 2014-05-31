using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_religionMap : EntityTypeConfiguration<searchsetting_religion>
    {
        public searchsetting_religionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_religion");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.religion_id).HasColumnName("religion_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_religion)
                .WithMany(t => t.searchsetting_religion)
                .HasForeignKey(d => d.religion_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_religion)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
