using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_DrinksMap : EntityTypeConfiguration<CriteriaCharacter_Drinks>
    {
        public CriteriaCharacter_DrinksMap()
        {
            // Primary Key
            this.HasKey(t => t.DrinksID);

            // Properties
            this.Property(t => t.DrinksName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Drinks");
            this.Property(t => t.DrinksID).HasColumnName("DrinksID");
            this.Property(t => t.DrinksName).HasColumnName("DrinksName");
        }
    }
}
