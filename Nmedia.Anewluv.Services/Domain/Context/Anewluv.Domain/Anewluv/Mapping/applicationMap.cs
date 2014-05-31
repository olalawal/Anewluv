using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class applicationMap : EntityTypeConfiguration<application>
    {
        public applicationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("applications");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.deactivationdate).HasColumnName("deactivationdate");
            this.Property(t => t.applicationtype_id).HasColumnName("applicationtype_id");

            // Relationships
            this.HasOptional(t => t.lu_applicationtype)
                .WithMany(t => t.applications)
                .HasForeignKey(d => d.applicationtype_id);

        }
    }
}
