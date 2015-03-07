using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class PhotoTypeMap : EntityTypeConfiguration<PhotoType>
    {
        public PhotoTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoTypeID);

            // Properties
            this.Property(t => t.PhotoTypeName)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.PhotoTypeDescription)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PhotoType");
            this.Property(t => t.PhotoTypeID).HasColumnName("PhotoTypeID");
            this.Property(t => t.PhotoTypeName).HasColumnName("PhotoTypeName");
            this.Property(t => t.PhotoTypeDescription).HasColumnName("PhotoTypeDescription");
        }
    }
}
