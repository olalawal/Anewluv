using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_hobbyMap : EntityTypeConfiguration<searchsetting_hobby>
    {
        public searchsetting_hobbyMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_hobby");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.hobby_id).HasColumnName("hobby_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_hobby)
                .WithMany(t => t.searchsetting_hobby)
                .HasForeignKey(d => d.hobby_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_hobby)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
