using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_PoliticalViewMap : EntityTypeConfiguration<CriteriaCharacter_PoliticalView>
    {
        public CriteriaCharacter_PoliticalViewMap()
        {
            // Primary Key
            this.HasKey(t => t.PoliticalViewID);

            // Properties
            this.Property(t => t.PoliticalViewName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_PoliticalView");
            this.Property(t => t.PoliticalViewID).HasColumnName("PoliticalViewID");
            this.Property(t => t.PoliticalViewName).HasColumnName("PoliticalViewName");
        }
    }
}
