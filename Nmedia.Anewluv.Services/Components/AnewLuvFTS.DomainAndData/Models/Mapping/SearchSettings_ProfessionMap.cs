using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_ProfessionMap : EntityTypeConfiguration<SearchSettings_Profession>
    {
        public SearchSettings_ProfessionMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_ProfessionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Profession");
            this.Property(t => t.SearchSettings_ProfessionID).HasColumnName("SearchSettings_ProfessionID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ProfessionID).HasColumnName("ProfessionID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_Profession)
                .WithMany(t => t.SearchSettings_Profession)
                .HasForeignKey(d => d.ProfessionID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Profession)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
