using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_IncomeLevelMap : EntityTypeConfiguration<SearchSettings_IncomeLevel>
    {
        public SearchSettings_IncomeLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_IncomeLevelID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_IncomeLevel");
            this.Property(t => t.SearchSettings_IncomeLevelID).HasColumnName("SearchSettings_IncomeLevelID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ImcomeLevelID).HasColumnName("ImcomeLevelID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_IncomeLevel)
                .WithMany(t => t.SearchSettings_IncomeLevel)
                .HasForeignKey(d => d.ImcomeLevelID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_IncomeLevel)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
