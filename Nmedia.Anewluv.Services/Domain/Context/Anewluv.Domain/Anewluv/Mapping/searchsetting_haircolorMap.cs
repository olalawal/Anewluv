using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_haircolorMap : EntityTypeConfiguration<searchsetting_haircolor>
    {
        public searchsetting_haircolorMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_haircolor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.haircolor_id).HasColumnName("haircolor_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_haircolor)
                .WithMany(t => t.searchsetting_haircolor)
                .HasForeignKey(d => d.haircolor_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_haircolor)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
