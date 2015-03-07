using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaAppearance_HairColorMap : EntityTypeConfiguration<CriteriaAppearance_HairColor>
    {
        public CriteriaAppearance_HairColorMap()
        {
            // Primary Key
            this.HasKey(t => t.HairColorID);

            // Properties
            this.Property(t => t.HairColorName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaAppearance_HairColor");
            this.Property(t => t.HairColorID).HasColumnName("HairColorID");
            this.Property(t => t.HairColorName).HasColumnName("HairColorName");
        }
    }
}
