using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_humorMap : EntityTypeConfiguration<searchsetting_humor>
    {
        public searchsetting_humorMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_humor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.humor_id).HasColumnName("humor_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_humor)
                .WithMany(t => t.searchsetting_humor)
                .HasForeignKey(d => d.humor_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_humor)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
