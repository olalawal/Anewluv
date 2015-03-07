using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class PhotoReviewStatuMap : EntityTypeConfiguration<PhotoReviewStatu>
    {
        public PhotoReviewStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoReviewStatusID);

            // Properties
            this.Property(t => t.PhotoReviewStatusValue)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PhotoReviewStatus");
            this.Property(t => t.PhotoReviewStatusID).HasColumnName("PhotoReviewStatusID");
            this.Property(t => t.PhotoReviewStatusValue).HasColumnName("PhotoReviewStatusValue");
        }
    }
}
