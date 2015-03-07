using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class FriendMap : EntityTypeConfiguration<Friend>
    {
        public FriendMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FriendID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Friends");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.FriendID).HasColumnName("FriendID");
            this.Property(t => t.MutualFriend).HasColumnName("MutualFriend");
            this.Property(t => t.FriendDate).HasColumnName("FriendDate");
            this.Property(t => t.FriendViewed).HasColumnName("FriendViewed");
            this.Property(t => t.FriendViewedDate).HasColumnName("FriendViewedDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.Friends)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
