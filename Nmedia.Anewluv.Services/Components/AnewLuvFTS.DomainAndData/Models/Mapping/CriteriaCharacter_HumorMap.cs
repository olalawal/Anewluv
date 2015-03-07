using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_HumorMap : EntityTypeConfiguration<CriteriaCharacter_Humor>
    {
        public CriteriaCharacter_HumorMap()
        {
            // Primary Key
            this.HasKey(t => t.HumorID);

            // Properties
            this.Property(t => t.HumorName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Humor");
            this.Property(t => t.HumorID).HasColumnName("HumorID");
            this.Property(t => t.HumorName).HasColumnName("HumorName");
        }
    }
}
