using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photoreviewMap : EntityTypeConfiguration<photoreview>
    {
        public photoreviewMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photoreviews");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.notes).HasColumnName("notes");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.reviewerprofile_id).HasColumnName("reviewerprofile_id");
            this.Property(t => t.photo_id).HasColumnName("photo_id");

            // Relationships
            this.HasRequired(t => t.photo)
                .WithMany(t => t.photoreviews)
                .HasForeignKey(d => d.photo_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.photoreviews)
                .HasForeignKey(d => d.reviewerprofile_id).WillCascadeOnDelete(false); ;

        }
    }
}
