using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class LikeMap : EntityTypeConfiguration<Like>
    {
        public LikeMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.LikeID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Like");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.LikeID).HasColumnName("LikeID");
            this.Property(t => t.LikeDate).HasColumnName("LikeDate");
            this.Property(t => t.MutuaLikes).HasColumnName("MutuaLikes");
            this.Property(t => t.LikeViewed).HasColumnName("LikeViewed");
            this.Property(t => t.LikeViewedDate).HasColumnName("LikeViewedDate");
            this.Property(t => t.DeletedByProfileID).HasColumnName("DeletedByProfileID");
            this.Property(t => t.DeletedByProfileIDDate).HasColumnName("DeletedByProfileIDDate");
            this.Property(t => t.DeletedByLikeID).HasColumnName("DeletedByLikeID");
            this.Property(t => t.DeletedByLikeIDDate).HasColumnName("DeletedByLikeIDDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.Likes)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
