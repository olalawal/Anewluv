using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_religiousattendanceMap : EntityTypeConfiguration<searchsetting_religiousattendance>
    {
        public searchsetting_religiousattendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_religiousattendance");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.religiousattendance_id).HasColumnName("religiousattendance_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_religiousattendance)
                .WithMany(t => t.searchsetting_religiousattendance)
                .HasForeignKey(d => d.religiousattendance_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_religiousattendance)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
