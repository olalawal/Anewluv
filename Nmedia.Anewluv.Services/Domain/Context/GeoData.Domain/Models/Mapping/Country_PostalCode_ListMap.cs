using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GeoData.Domain.Models.Mapping
{
    public class Country_PostalCode_ListMap : EntityTypeConfiguration<Country_PostalCode_List>
    {
        public Country_PostalCode_ListMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.Country_Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CountryName)
                .HasMaxLength(50);

            this.Property(t => t.Country_Region)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Country_PostalCode_List");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.Country_Code).HasColumnName("Country_Code");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.Country_Region).HasColumnName("Country_Region");
            this.Property(t => t.PostalCodes).HasColumnName("PostalCodes");
            this.Property(t => t.CountryCustomRegionID).HasColumnName("CountryCustomRegionID");
        }
    }
}
