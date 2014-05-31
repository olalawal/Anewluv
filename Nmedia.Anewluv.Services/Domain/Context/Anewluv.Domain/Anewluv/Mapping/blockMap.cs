using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class blockMap : EntityTypeConfiguration<block>
    {
        public blockMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("blocks");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.blockprofile_id).HasColumnName("blockprofile_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.removedate).HasColumnName("removedate");
            this.Property(t => t.mutual).HasColumnName("mutual");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.blocks)
                .HasForeignKey(d => d.blockprofile_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.blocks1)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
