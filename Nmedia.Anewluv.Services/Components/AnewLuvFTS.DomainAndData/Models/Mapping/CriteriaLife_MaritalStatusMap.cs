using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_MaritalStatusMap : EntityTypeConfiguration<CriteriaLife_MaritalStatus>
    {
        public CriteriaLife_MaritalStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.MaritalStatusID);

            // Properties
            this.Property(t => t.MaritalStatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_MaritalStatus");
            this.Property(t => t.MaritalStatusID).HasColumnName("MaritalStatusID");
            this.Property(t => t.MaritalStatusName).HasColumnName("MaritalStatusName");
        }
    }
}
