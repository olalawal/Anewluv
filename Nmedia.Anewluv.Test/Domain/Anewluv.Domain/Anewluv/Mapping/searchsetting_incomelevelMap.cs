using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_incomelevelMap : EntityTypeConfiguration<searchsetting_incomelevel>
    {
        public searchsetting_incomelevelMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_incomelevel");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.incomelevel_id).HasColumnName("incomelevel_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_incomelevel)
                .WithMany(t => t.searchsetting_incomelevel)
                .HasForeignKey(d => d.incomelevel_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_incomelevel)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
