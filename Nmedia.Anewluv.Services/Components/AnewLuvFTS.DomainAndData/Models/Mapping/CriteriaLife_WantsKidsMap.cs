using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_WantsKidsMap : EntityTypeConfiguration<CriteriaLife_WantsKids>
    {
        public CriteriaLife_WantsKidsMap()
        {
            // Primary Key
            this.HasKey(t => t.WantsKidsID);

            // Properties
            this.Property(t => t.WantsKidsName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_WantsKids");
            this.Property(t => t.WantsKidsID).HasColumnName("WantsKidsID");
            this.Property(t => t.WantsKidsName).HasColumnName("WantsKidsName");
        }
    }
}
