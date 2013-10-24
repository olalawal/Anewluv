using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GeoData.Domain.Models.Mapping
{
    public class CustomRegionMap : EntityTypeConfiguration<CustomRegion>
    {
        public CustomRegionMap()
        {
            // Primary Key
            this.HasKey(t => t.RegionID);

            // Properties
            this.Property(t => t.CustomRegionName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CustomRegions");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.CustomRegionName).HasColumnName("CustomRegionName");
            this.Property(t => t.CustomRegionDescription).HasColumnName("CustomRegionDescription");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
        }
    }
}
