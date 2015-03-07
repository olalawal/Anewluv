using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaAppearance_EthnicityMap : EntityTypeConfiguration<CriteriaAppearance_Ethnicity>
    {
        public CriteriaAppearance_EthnicityMap()
        {
            // Primary Key
            this.HasKey(t => t.EthnicityID);

            // Properties
            this.Property(t => t.EthnicityName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaAppearance_Ethnicity");
            this.Property(t => t.EthnicityID).HasColumnName("EthnicityID");
            this.Property(t => t.EthnicityName).HasColumnName("EthnicityName");
        }
    }
}
