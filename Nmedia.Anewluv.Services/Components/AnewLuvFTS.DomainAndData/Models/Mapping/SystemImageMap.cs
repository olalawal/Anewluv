using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SystemImageMap : EntityTypeConfiguration<SystemImage>
    {
        public SystemImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageID);

            // Properties
            this.Property(t => t.ImageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProfileID)
                .HasMaxLength(50);

            this.Property(t => t.ImageCaption)
                .HasMaxLength(255);

            this.Property(t => t.ImageType)
                .HasMaxLength(255);

            this.Property(t => t.ImageStatus)
                .HasMaxLength(25);

            this.Property(t => t.AproveStatus)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SystemImages");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.ImageCaption).HasColumnName("ImageCaption");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.ImageType).HasColumnName("ImageType");
            this.Property(t => t.ImageStatus).HasColumnName("ImageStatus");
            this.Property(t => t.AproveStatus).HasColumnName("AproveStatus");
        }
    }
}
