using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_smokesMap : EntityTypeConfiguration<searchsetting_smokes>
    {
        public searchsetting_smokesMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_smokes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.smoke_id).HasColumnName("smoke_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_smokes)
                .WithMany(t => t.searchsetting_smokes)
                .HasForeignKey(d => d.smoke_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_smokes)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
