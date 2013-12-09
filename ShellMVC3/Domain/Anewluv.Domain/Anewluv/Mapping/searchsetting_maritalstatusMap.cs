using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_maritalstatusMap : EntityTypeConfiguration<searchsetting_maritalstatus>
    {
        public searchsetting_maritalstatusMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_maritalstatus");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.maritalstatus_id).HasColumnName("maritalstatus_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_maritalstatus)
                .WithMany(t => t.searchsetting_maritalstatus)
                .HasForeignKey(d => d.maritalstatus_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_maritalstatus)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
