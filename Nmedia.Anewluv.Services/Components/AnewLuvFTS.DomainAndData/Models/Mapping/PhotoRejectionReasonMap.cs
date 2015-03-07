using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class PhotoRejectionReasonMap : EntityTypeConfiguration<PhotoRejectionReason>
    {
        public PhotoRejectionReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoRejectionReasonID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PhotoRejectionReasons");
            this.Property(t => t.PhotoRejectionReasonID).HasColumnName("PhotoRejectionReasonID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.UserMessage).HasColumnName("UserMessage");
        }
    }
}
