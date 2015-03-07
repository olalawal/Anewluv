using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_DietMap : EntityTypeConfiguration<CriteriaCharacter_Diet>
    {
        public CriteriaCharacter_DietMap()
        {
            // Primary Key
            this.HasKey(t => t.DietID);

            // Properties
            this.Property(t => t.DietName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Diet");
            this.Property(t => t.DietID).HasColumnName("DietID");
            this.Property(t => t.DietName).HasColumnName("DietName");
        }
    }
}
