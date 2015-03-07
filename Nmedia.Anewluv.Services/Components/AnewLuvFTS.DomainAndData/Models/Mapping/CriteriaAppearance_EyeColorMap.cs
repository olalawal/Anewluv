using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaAppearance_EyeColorMap : EntityTypeConfiguration<CriteriaAppearance_EyeColor>
    {
        public CriteriaAppearance_EyeColorMap()
        {
            // Primary Key
            this.HasKey(t => t.EyeColorID);

            // Properties
            this.Property(t => t.EyeColorName)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CriteriaAppearance_EyeColor");
            this.Property(t => t.EyeColorID).HasColumnName("EyeColorID");
            this.Property(t => t.EyeColorName).HasColumnName("EyeColorName");
        }
    }
}
