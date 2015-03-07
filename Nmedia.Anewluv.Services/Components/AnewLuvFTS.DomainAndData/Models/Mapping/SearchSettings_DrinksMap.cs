using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_DrinksMap : EntityTypeConfiguration<SearchSettings_Drinks>
    {
        public SearchSettings_DrinksMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_DrinksID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Drinks");
            this.Property(t => t.SearchSettings_DrinksID).HasColumnName("SearchSettings_DrinksID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.DrinksID).HasColumnName("DrinksID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Drinks)
                .WithMany(t => t.SearchSettings_Drinks)
                .HasForeignKey(d => d.DrinksID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Drinks)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
