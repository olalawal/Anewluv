using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GeoData.Domain.Models.Mapping
{
    public class NigeriaMap : EntityTypeConfiguration<Nigeria>
    {
        public NigeriaMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.Country_Code)
                .HasMaxLength(50);

            this.Property(t => t.PostalCode)
                .HasMaxLength(50);

            this.Property(t => t.City)
                .HasMaxLength(50);

            this.Property(t => t.State_Province)
                .HasMaxLength(50);

            this.Property(t => t.State_Province_Code)
                .HasMaxLength(50);

            this.Property(t => t.County_Province)
                .HasMaxLength(50);

            this.Property(t => t.Empty1)
                .HasMaxLength(50);

            this.Property(t => t.Empty2)
                .HasMaxLength(50);

            this.Property(t => t.LATITUDE)
                .HasMaxLength(50);

            this.Property(t => t.LONGITUDE)
                .HasMaxLength(50);

            this.Property(t => t.Empty3)
                .HasMaxLength(50);

            this.Property(t => t.Country_Region)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Nigeria");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.Country_Code).HasColumnName("Country_Code");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State_Province).HasColumnName("State_Province");
            this.Property(t => t.State_Province_Code).HasColumnName("State_Province_Code");
            this.Property(t => t.County_Province).HasColumnName("County_Province");
            this.Property(t => t.Empty1).HasColumnName("Empty1");
            this.Property(t => t.Empty2).HasColumnName("Empty2");
            this.Property(t => t.LATITUDE).HasColumnName("LATITUDE");
            this.Property(t => t.LONGITUDE).HasColumnName("LONGITUDE");
            this.Property(t => t.Empty3).HasColumnName("Empty3");
            this.Property(t => t.Country_Region).HasColumnName("Country_Region");
        }
    }
}
