using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_HumorMap : EntityTypeConfiguration<SearchSettings_Humor>
    {
        public SearchSettings_HumorMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_HumorID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Humor");
            this.Property(t => t.SearchSettings_HumorID).HasColumnName("SearchSettings_HumorID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.HumorID).HasColumnName("HumorID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Humor)
                .WithMany(t => t.SearchSettings_Humor)
                .HasForeignKey(d => d.HumorID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Humor)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
