using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_sortbytypeMap : EntityTypeConfiguration<searchsetting_sortbytype>
    {
        public searchsetting_sortbytypeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_sortbytype");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");
            this.Property(t => t.sortbytype_id).HasColumnName("sortbytype_id");

            // Relationships
            this.HasOptional(t => t.lu_sortbytype)
                .WithMany(t => t.searchsetting_sortbytype)
                .HasForeignKey(d => d.sortbytype_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_sortbytype)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
