using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_EmploymentStatusMap : EntityTypeConfiguration<CriteriaLife_EmploymentStatus>
    {
        public CriteriaLife_EmploymentStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.EmploymentSatusID);

            // Properties
            this.Property(t => t.EmploymentStatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_EmploymentStatus");
            this.Property(t => t.EmploymentSatusID).HasColumnName("EmploymentSatusID");
            this.Property(t => t.EmploymentStatusName).HasColumnName("EmploymentStatusName");
        }
    }
}
