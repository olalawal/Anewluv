using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_HaveKidsMap : EntityTypeConfiguration<CriteriaLife_HaveKids>
    {
        public CriteriaLife_HaveKidsMap()
        {
            // Primary Key
            this.HasKey(t => t.HaveKidsId);

            // Properties
            this.Property(t => t.HaveKidsName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_HaveKids");
            this.Property(t => t.HaveKidsId).HasColumnName("HaveKidsId");
            this.Property(t => t.HaveKidsName).HasColumnName("HaveKidsName");
        }
    }
}
