using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_HobbyMap : EntityTypeConfiguration<CriteriaCharacter_Hobby>
    {
        public CriteriaCharacter_HobbyMap()
        {
            // Primary Key
            this.HasKey(t => t.HobbyID);

            // Properties
            this.Property(t => t.HobbyName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Hobby");
            this.Property(t => t.HobbyID).HasColumnName("HobbyID");
            this.Property(t => t.HobbyName).HasColumnName("HobbyName");
        }
    }
}
