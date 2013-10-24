using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GeoData.Domain.Models.Mapping
{
    public class CountryCodeMap : EntityTypeConfiguration<CountryCode>
    {
        public CountryCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.CountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CountryName)
                .HasMaxLength(50);

            this.Property(t => t.CountryCode1)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CountryCodes");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.CountryCode1).HasColumnName("CountryCode");
        }
    }
}
