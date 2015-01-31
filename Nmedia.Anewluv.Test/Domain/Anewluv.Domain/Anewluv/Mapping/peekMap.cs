using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class peekMap : EntityTypeConfiguration<peek>
    {
        public peekMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("peeks");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.peekprofile_id).HasColumnName("peekprofile_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.viewdate).HasColumnName("viewdate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.deletedbymemberdate).HasColumnName("deletedbymemberdate");
            this.Property(t => t.deletedbypeekdate).HasColumnName("deletedbypeekdate");
            this.Property(t => t.mutual).HasColumnName("mutual");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.peeks)
                .HasForeignKey(d => d.peekprofile_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.peeks1)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
