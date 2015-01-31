using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_wantkidsMap : EntityTypeConfiguration<searchsetting_wantkids>
    {
        public searchsetting_wantkidsMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_wantkids");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.wantskids_id).HasColumnName("wantskids_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_wantskids)
                .WithMany(t => t.searchsetting_wantkids)
                .HasForeignKey(d => d.wantskids_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_wantkids)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
