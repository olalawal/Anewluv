using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_LookingForMap : EntityTypeConfiguration<SearchSettings_LookingFor>
    {
        public SearchSettings_LookingForMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_LookingFor1);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_LookingFor");
            this.Property(t => t.SearchSettings_LookingFor1).HasColumnName("SearchSettings_LookingFor");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.LookingForID).HasColumnName("LookingForID");

            // Relationships
            this.HasOptional(t => t.CriteriaLife_LookingFor)
                .WithMany(t => t.SearchSettings_LookingFor)
                .HasForeignKey(d => d.LookingForID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_LookingFor)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
