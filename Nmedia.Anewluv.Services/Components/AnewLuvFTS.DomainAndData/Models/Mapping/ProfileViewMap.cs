using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileViewMap : EntityTypeConfiguration<ProfileView>
    {
        public ProfileViewMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ProfileViewerID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileViews");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.ProfileViewerID).HasColumnName("ProfileViewerID");
            this.Property(t => t.ProfileViewDate).HasColumnName("ProfileViewDate");
            this.Property(t => t.MutualViews).HasColumnName("MutualViews");
            this.Property(t => t.ProfileViewViewed).HasColumnName("ProfileViewViewed");
            this.Property(t => t.ProfileViewViewedDate).HasColumnName("ProfileViewViewedDate");
            this.Property(t => t.DeletedByProfileID).HasColumnName("DeletedByProfileID");
            this.Property(t => t.DeletedByProfileIDDate).HasColumnName("DeletedByProfileIDDate");
            this.Property(t => t.DeletedByProfileViewerID).HasColumnName("DeletedByProfileViewerID");
            this.Property(t => t.DeletedByProfileViewerIDDate).HasColumnName("DeletedByProfileViewerIDDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.ProfileViews)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
