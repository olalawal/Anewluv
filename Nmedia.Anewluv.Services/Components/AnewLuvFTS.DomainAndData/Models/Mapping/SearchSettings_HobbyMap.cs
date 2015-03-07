using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_HobbyMap : EntityTypeConfiguration<SearchSettings_Hobby>
    {
        public SearchSettings_HobbyMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_HobbyID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Hobby");
            this.Property(t => t.SearchSettings_HobbyID).HasColumnName("SearchSettings_HobbyID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.HobbyID).HasColumnName("HobbyID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Hobby)
                .WithMany(t => t.SearchSettings_Hobby)
                .HasForeignKey(d => d.HobbyID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Hobby)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
