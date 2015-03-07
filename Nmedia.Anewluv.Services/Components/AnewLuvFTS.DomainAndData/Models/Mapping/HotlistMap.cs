using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class HotlistMap : EntityTypeConfiguration<Hotlist>
    {
        public HotlistMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.HotlistID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Hotlist");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.HotlistID).HasColumnName("HotlistID");
            this.Property(t => t.MutualHotlist).HasColumnName("MutualHotlist");
            this.Property(t => t.HotlistDate).HasColumnName("HotlistDate");
            this.Property(t => t.HotlistViewed).HasColumnName("HotlistViewed");
            this.Property(t => t.HotlistViewedDate).HasColumnName("HotlistViewedDate");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.Hotlists)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
