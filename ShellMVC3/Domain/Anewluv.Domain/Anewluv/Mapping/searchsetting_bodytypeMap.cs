using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_bodytypeMap : EntityTypeConfiguration<searchsetting_bodytype>
    {
        public searchsetting_bodytypeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_bodytype");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.bodytype_id).HasColumnName("bodytype_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_bodytype)
                .WithMany(t => t.searchsetting_bodytype)
                .HasForeignKey(d => d.bodytype_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_bodytype)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
