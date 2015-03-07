using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class PhotoStatuMap : EntityTypeConfiguration<PhotoStatu>
    {
        public PhotoStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.PhotoStatusID);

            // Properties
            this.Property(t => t.PhotoStatusValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PhotoStatus");
            this.Property(t => t.PhotoStatusID).HasColumnName("PhotoStatusID");
            this.Property(t => t.PhotoStatusValue).HasColumnName("PhotoStatusValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
