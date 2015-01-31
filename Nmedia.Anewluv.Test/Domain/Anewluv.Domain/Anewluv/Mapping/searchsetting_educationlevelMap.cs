using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_educationlevelMap : EntityTypeConfiguration<searchsetting_educationlevel>
    {
        public searchsetting_educationlevelMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_educationlevel");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.educationlevel_id).HasColumnName("educationlevel_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_educationlevel)
                .WithMany(t => t.searchsetting_educationlevel)
                .HasForeignKey(d => d.educationlevel_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_educationlevel)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
