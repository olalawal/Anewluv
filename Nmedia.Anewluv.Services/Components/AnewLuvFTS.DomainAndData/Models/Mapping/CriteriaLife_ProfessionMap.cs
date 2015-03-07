using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_ProfessionMap : EntityTypeConfiguration<CriteriaLife_Profession>
    {
        public CriteriaLife_ProfessionMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfessionID);

            // Properties
            this.Property(t => t.ProfiessionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_Profession");
            this.Property(t => t.ProfessionID).HasColumnName("ProfessionID");
            this.Property(t => t.ProfiessionName).HasColumnName("ProfiessionName");
        }
    }
}
