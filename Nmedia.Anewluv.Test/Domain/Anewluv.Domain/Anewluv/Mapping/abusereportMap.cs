using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class abusereportMap : EntityTypeConfiguration<abusereport>
    {
        public abusereportMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("abusereports");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.abusereporter_id).HasColumnName("abusereporter_id");
            this.Property(t => t.abuser_id).HasColumnName("abuser_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.abusetype_id).HasColumnName("abusetype_id");

            // Relationships
            this.HasRequired(t => t.lu_abusetype)
                .WithMany(t => t.abusereports)
                .HasForeignKey(d => d.abusetype_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.abusereports)
                .HasForeignKey(d => d.abuser_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.abusereports1)
                .HasForeignKey(d => d.abusereporter_id);

        }
    }
}
