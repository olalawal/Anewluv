using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_ReligionMap : EntityTypeConfiguration<CriteriaCharacter_Religion>
    {
        public CriteriaCharacter_ReligionMap()
        {
            // Primary Key
            this.HasKey(t => t.religionID);

            // Properties
            this.Property(t => t.religionName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Religion");
            this.Property(t => t.religionID).HasColumnName("religionID");
            this.Property(t => t.religionName).HasColumnName("religionName");
        }
    }
}
