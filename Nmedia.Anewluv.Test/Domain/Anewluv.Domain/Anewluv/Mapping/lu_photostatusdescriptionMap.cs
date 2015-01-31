using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_photostatusdescriptionMap : EntityTypeConfiguration<lu_photostatusdescription>
    {
        public lu_photostatusdescriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_photostatusdescription");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.photostatus_id).HasColumnName("photostatus_id");

            // Relationships
            this.HasOptional(t => t.lu_photostatus)
                .WithMany(t => t.lu_photostatusdescription)
                .HasForeignKey(d => d.photostatus_id);

        }
    }
}
