using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_IncomeLevelMap : EntityTypeConfiguration<CriteriaLife_IncomeLevel>
    {
        public CriteriaLife_IncomeLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.IncomeLevelID);

            // Properties
            this.Property(t => t.IncomeLevelName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_IncomeLevel");
            this.Property(t => t.IncomeLevelID).HasColumnName("IncomeLevelID");
            this.Property(t => t.IncomeLevelName).HasColumnName("IncomeLevelName");
        }
    }
}
