using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileRatingTrackerMap : EntityTypeConfiguration<ProfileRatingTracker>
    {
        public ProfileRatingTrackerMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileRatingTrackerID);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileRatingTracker");
            this.Property(t => t.ProfileRatingTrackerID).HasColumnName("ProfileRatingTrackerID");
            this.Property(t => t.ProfileRatingID).HasColumnName("ProfileRatingID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.RatingValue).HasColumnName("RatingValue");
            this.Property(t => t.ProfileRatingTrackerDate).HasColumnName("ProfileRatingTrackerDate");

            // Relationships
            this.HasOptional(t => t.ProfileRating)
                .WithMany(t => t.ProfileRatingTrackers)
                .HasForeignKey(d => d.ProfileRatingID);
            this.HasOptional(t => t.ProfileRating1)
                .WithMany(t => t.ProfileRatingTrackers1)
                .HasForeignKey(d => d.ProfileRatingID);
            this.HasOptional(t => t.ProfileRating2)
                .WithMany(t => t.ProfileRatingTrackers2)
                .HasForeignKey(d => d.ProfileRatingID);

        }
    }
}
