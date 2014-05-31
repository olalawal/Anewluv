using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_havekidsMap : EntityTypeConfiguration<searchsetting_havekids>
    {
        public searchsetting_havekidsMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_havekids");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.havekids_id).HasColumnName("havekids_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_havekids)
                .WithMany(t => t.searchsetting_havekids)
                .HasForeignKey(d => d.havekids_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_havekids)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
