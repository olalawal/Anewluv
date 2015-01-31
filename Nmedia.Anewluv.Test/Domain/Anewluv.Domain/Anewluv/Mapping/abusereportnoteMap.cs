using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class abusereportnoteMap : EntityTypeConfiguration<abusereportnote>
    {
        public abusereportnoteMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("abusereportnotes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.abusereport_id).HasColumnName("abusereport_id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.note).HasColumnName("note");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.reviewdate).HasColumnName("reviewdate");
            this.Property(t => t.notetype_id).HasColumnName("notetype_id");

            // Relationships
            this.HasRequired(t => t.abusereport)
                .WithMany(t => t.abusereportnotes)
                .HasForeignKey(d => d.abusereport_id);
            this.HasRequired(t => t.lu_notetype)
                .WithMany(t => t.abusereportnotes)
                .HasForeignKey(d => d.notetype_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.abusereportnotes)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
