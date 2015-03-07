using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_LivingSituationMap : EntityTypeConfiguration<CriteriaLife_LivingSituation>
    {
        public CriteriaLife_LivingSituationMap()
        {
            // Primary Key
            this.HasKey(t => t.LivingSituationID);

            // Properties
            this.Property(t => t.LivingSituationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_LivingSituation");
            this.Property(t => t.LivingSituationID).HasColumnName("LivingSituationID");
            this.Property(t => t.LivingSituationName).HasColumnName("LivingSituationName");
        }
    }
}
