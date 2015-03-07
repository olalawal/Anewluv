using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class InterestMap : EntityTypeConfiguration<Interest>
    {
        public InterestMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.InterestID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Interest");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.MutualInterest).HasColumnName("MutualInterest");
            this.Property(t => t.InterestDate).HasColumnName("InterestDate");
            this.Property(t => t.InterestID).HasColumnName("InterestID");
            this.Property(t => t.IntrestViewed).HasColumnName("IntrestViewed");
            this.Property(t => t.IntrestViewedDate).HasColumnName("IntrestViewedDate");
            this.Property(t => t.DeletedByProfileID).HasColumnName("DeletedByProfileID");
            this.Property(t => t.DeletedByProfileIDDate).HasColumnName("DeletedByProfileIDDate");
            this.Property(t => t.DeletedByInterestID).HasColumnName("DeletedByInterestID");
            this.Property(t => t.DeletedByInterestIDDate).HasColumnName("DeletedByInterestIDDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.Interests)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
