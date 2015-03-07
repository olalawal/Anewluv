using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileRatingMap : EntityTypeConfiguration<ProfileRating>
    {
        public ProfileRatingMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileRatingID);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileRatings");
            this.Property(t => t.ProfileRatingID).HasColumnName("ProfileRatingID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.RatingID).HasColumnName("RatingID");
            this.Property(t => t.AverageRating).HasColumnName("AverageRating");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileRatings)
                .HasForeignKey(d => d.ProfileID);
            this.HasOptional(t => t.Rating)
                .WithMany(t => t.ProfileRatings)
                .HasForeignKey(d => d.RatingID);
            this.HasOptional(t => t.Rating1)
                .WithMany(t => t.ProfileRatings1)
                .HasForeignKey(d => d.RatingID);

        }
    }
}
