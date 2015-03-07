using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class PhotoAlbumMap : EntityTypeConfiguration<PhotoAlbum>
    {
        public PhotoAlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoAlbumID);

            // Properties
            this.Property(t => t.PhotoAlbumID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.PhotoAlbumDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PhotoAlbums");
            this.Property(t => t.PhotoAlbumID).HasColumnName("PhotoAlbumID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.PhotoAlbumDescription).HasColumnName("PhotoAlbumDescription");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.PhotoAlbums)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
