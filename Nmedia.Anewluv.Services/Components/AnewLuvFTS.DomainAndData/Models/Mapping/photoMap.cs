using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class photoMap : EntityTypeConfiguration<photo>
    {
        public photoMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ImageCaption)
                .HasMaxLength(255);

            this.Property(t => t.ProfileImageType)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Aproved)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PhotoReviewerID)
                .HasMaxLength(50);

            this.Property(t => t.ProfileImage)
                .IsRequired();

            this.Property(t => t.PhotoProviderName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("photo");
            this.Property(t => t.PhotoID).HasColumnName("PhotoID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.ImageCaption).HasColumnName("ImageCaption");
            this.Property(t => t.ProfileImageType).HasColumnName("ProfileImageType");
            this.Property(t => t.PhotoStatusID).HasColumnName("PhotoStatusID");
            this.Property(t => t.Aproved).HasColumnName("Aproved");
            this.Property(t => t.PhotoSize).HasColumnName("PhotoSize");
            this.Property(t => t.PhotoDate).HasColumnName("PhotoDate");
            this.Property(t => t.PhotoAlbumID).HasColumnName("PhotoAlbumID");
            this.Property(t => t.PhotoReviewStatusID).HasColumnName("PhotoReviewStatusID");
            this.Property(t => t.PhotoReviewerID).HasColumnName("PhotoReviewerID");
            this.Property(t => t.PhotoReviewDate).HasColumnName("PhotoReviewDate");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.PhotoRejectionReasonID).HasColumnName("PhotoRejectionReasonID");
            this.Property(t => t.PhotoUniqueID).HasColumnName("PhotoUniqueID");
            this.Property(t => t.PhotoTypeID).HasColumnName("PhotoTypeID");
            this.Property(t => t.PhotoProviderName).HasColumnName("PhotoProviderName");

            // Relationships
            this.HasOptional(t => t.PhotoRejectionReason)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.PhotoRejectionReasonID);
            this.HasOptional(t => t.PhotoType)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.PhotoTypeID);
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.ProfileID);
            this.HasRequired(t => t.ProfileData1)
                .WithMany(t => t.photos1)
                .HasForeignKey(d => d.ProfileID);
            this.HasOptional(t => t.PhotoAlbum)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.PhotoAlbumID);
            this.HasOptional(t => t.PhotoReviewStatu)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.PhotoReviewStatusID);
            this.HasRequired(t => t.PhotoStatu)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.PhotoStatusID);

        }
    }
}
