using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_LocationMap : EntityTypeConfiguration<SearchSettings_Location>
    {
        public SearchSettings_LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_Location1);

            // Properties
            this.Property(t => t.City)
                .HasMaxLength(100);

            this.Property(t => t.PostalCode)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("SearchSettings_Location");
            this.Property(t => t.SearchSettings_Location1).HasColumnName("SearchSettings_Location");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");

            // Relationships
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Location)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
