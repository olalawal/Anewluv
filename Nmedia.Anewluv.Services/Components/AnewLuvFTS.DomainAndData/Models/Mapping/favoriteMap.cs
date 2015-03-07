using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class favoriteMap : EntityTypeConfiguration<favorite>
    {
        public favoriteMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FavoriteID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("favorite");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.FavoriteID).HasColumnName("FavoriteID");
            this.Property(t => t.MutualFavorite).HasColumnName("MutualFavorite");
            this.Property(t => t.FavoriteDate).HasColumnName("FavoriteDate");
            this.Property(t => t.FavoriteViewed).HasColumnName("FavoriteViewed");
            this.Property(t => t.FavoriteViewedDate).HasColumnName("FavoriteViewedDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.favorites)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
