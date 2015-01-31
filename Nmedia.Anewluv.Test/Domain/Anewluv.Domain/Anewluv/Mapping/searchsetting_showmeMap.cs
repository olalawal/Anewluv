using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_showmeMap : EntityTypeConfiguration<searchsetting_showme>
    {
        public searchsetting_showmeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_showme");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");
            this.Property(t => t.showme_id).HasColumnName("showme_id");

            // Relationships
            this.HasOptional(t => t.lu_showme)
                .WithMany(t => t.searchsetting_showme)
                .HasForeignKey(d => d.showme_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_showme)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
