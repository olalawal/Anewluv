using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_politicalviewMap : EntityTypeConfiguration<searchsetting_politicalview>
    {
        public searchsetting_politicalviewMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_politicalview");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.politicalview_id).HasColumnName("politicalview_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_politicalview)
                .WithMany(t => t.searchsetting_politicalview)
                .HasForeignKey(d => d.politicalview_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_politicalview)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
