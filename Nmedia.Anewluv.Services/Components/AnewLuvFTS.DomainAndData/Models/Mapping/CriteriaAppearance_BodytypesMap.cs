using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaAppearance_BodytypesMap : EntityTypeConfiguration<CriteriaAppearance_Bodytypes>
    {
        public CriteriaAppearance_BodytypesMap()
        {
            // Primary Key
            this.HasKey(t => t.BodyTypesID);

            // Properties
            this.Property(t => t.BodyTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaAppearance_Bodytypes");
            this.Property(t => t.BodyTypesID).HasColumnName("BodyTypesID");
            this.Property(t => t.BodyTypeName).HasColumnName("BodyTypeName");
        }
    }
}
