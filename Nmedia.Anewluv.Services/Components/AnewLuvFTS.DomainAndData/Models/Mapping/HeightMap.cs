using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class HeightMap : EntityTypeConfiguration<Height>
    {
        public HeightMap()
        {
            // Primary Key
            this.HasKey(t => t.HeightID);

            // Properties
            this.Property(t => t.HeightValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Height");
            this.Property(t => t.HeightID).HasColumnName("HeightID");
            this.Property(t => t.HeightValue).HasColumnName("HeightValue");
        }
    }
}
