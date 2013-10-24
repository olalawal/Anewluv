using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GeoData.Domain.Models.Mapping
{
    public class Country_PostalCode_RegionMap : EntityTypeConfiguration<Country_PostalCode_Region>
    {
        public Country_PostalCode_RegionMap()
        {
            // Primary Key
            this.HasKey(t => t.Country_PostalCode_RegionID);

            // Properties
            this.Property(t => t.Country_Region)
                .HasMaxLength(10);

            this.Property(t => t.Country_Region_Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Country_PostalCode_Region");
            this.Property(t => t.Country_PostalCode_RegionID).HasColumnName("Country_PostalCode_RegionID");
            this.Property(t => t.Country_Region).HasColumnName("Country_Region");
            this.Property(t => t.Country_Region_Description).HasColumnName("Country_Region_Description");
        }
    }
}
