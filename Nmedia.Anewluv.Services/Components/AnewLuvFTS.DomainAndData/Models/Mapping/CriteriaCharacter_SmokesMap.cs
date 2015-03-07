using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_SmokesMap : EntityTypeConfiguration<CriteriaCharacter_Smokes>
    {
        public CriteriaCharacter_SmokesMap()
        {
            // Primary Key
            this.HasKey(t => t.SmokesID);

            // Properties
            this.Property(t => t.SmokesName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Smokes");
            this.Property(t => t.SmokesID).HasColumnName("SmokesID");
            this.Property(t => t.SmokesName).HasColumnName("SmokesName");
        }
    }
}
