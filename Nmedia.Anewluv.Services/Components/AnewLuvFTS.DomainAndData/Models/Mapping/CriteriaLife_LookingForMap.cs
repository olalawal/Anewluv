using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_LookingForMap : EntityTypeConfiguration<CriteriaLife_LookingFor>
    {
        public CriteriaLife_LookingForMap()
        {
            // Primary Key
            this.HasKey(t => t.LookingForID);

            // Properties
            this.Property(t => t.LookingForName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaLife_LookingFor");
            this.Property(t => t.LookingForID).HasColumnName("LookingForID");
            this.Property(t => t.LookingForName).HasColumnName("LookingForName");
        }
    }
}
