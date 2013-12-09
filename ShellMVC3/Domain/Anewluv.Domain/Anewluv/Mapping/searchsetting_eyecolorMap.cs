using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_eyecolorMap : EntityTypeConfiguration<searchsetting_eyecolor>
    {
        public searchsetting_eyecolorMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_eyecolor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.eyecolor_id).HasColumnName("eyecolor_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_eyecolor)
                .WithMany(t => t.searchsetting_eyecolor)
                .HasForeignKey(d => d.eyecolor_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_eyecolor)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
