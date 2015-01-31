using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class favoriteMap : EntityTypeConfiguration<favorite>
    {
        public favoriteMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("favorites");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.favoriteprofile_id).HasColumnName("favoriteprofile_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.viewdate).HasColumnName("viewdate");
            this.Property(t => t.deletedbymemberdate).HasColumnName("deletedbymemberdate");
            this.Property(t => t.deletedbyfavoritedate).HasColumnName("deletedbyfavoritedate");
            this.Property(t => t.mutual).HasColumnName("mutual");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.favorites)
                .HasForeignKey(d => d.favoriteprofile_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.favorites1)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
