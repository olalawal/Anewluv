using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_LivingStituationMap : EntityTypeConfiguration<SearchSettings_LivingStituation>
    {
        public SearchSettings_LivingStituationMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_LivingStituationID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_LivingStituation");
            this.Property(t => t.SearchSettings_LivingStituationID).HasColumnName("SearchSettings_LivingStituationID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.LivingStituationID).HasColumnName("LivingStituationID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_LivingSituation)
                .WithMany(t => t.SearchSettings_LivingStituation)
                .HasForeignKey(d => d.LivingStituationID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_LivingStituation)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
