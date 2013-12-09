using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_employmentstatusMap : EntityTypeConfiguration<searchsetting_employmentstatus>
    {
        public searchsetting_employmentstatusMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_employmentstatus");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.employmentstatus_id).HasColumnName("employmentstatus_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_employmentstatus)
                .WithMany(t => t.searchsetting_employmentstatus)
                .HasForeignKey(d => d.employmentstatus_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_employmentstatus)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
