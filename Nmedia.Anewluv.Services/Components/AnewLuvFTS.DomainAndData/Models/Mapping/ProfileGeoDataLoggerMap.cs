using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileGeoDataLoggerMap : EntityTypeConfiguration<ProfileGeoDataLogger>
    {
        public ProfileGeoDataLoggerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.CountryName)
                .HasMaxLength(255);

            this.Property(t => t.CountryCode)
                .HasMaxLength(10);

            this.Property(t => t.RegionName)
                .HasMaxLength(100);

            this.Property(t => t.City)
                .IsFixedLength()
                .HasMaxLength(100);

            this.Property(t => t.UserAgent)
                .HasMaxLength(255);

            this.Property(t => t.Continent)
                .HasMaxLength(50);

            this.Property(t => t.IPaddress)
                .HasMaxLength(255);

            this.Property(t => t.SessionID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileGeoDataLogger");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.RegionName).HasColumnName("RegionName");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Lattitude).HasColumnName("Lattitude");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.UserAgent).HasColumnName("UserAgent");
            this.Property(t => t.Continent).HasColumnName("Continent");
            this.Property(t => t.IPaddress).HasColumnName("IPaddress");
            this.Property(t => t.SessionID).HasColumnName("SessionID");

            // Relationships
            this.HasOptional(t => t.profile)
                .WithMany(t => t.ProfileGeoDataLoggers)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
