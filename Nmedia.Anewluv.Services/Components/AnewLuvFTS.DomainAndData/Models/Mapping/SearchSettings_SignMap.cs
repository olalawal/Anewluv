using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_SignMap : EntityTypeConfiguration<SearchSettings_Sign>
    {
        public SearchSettings_SignMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_SignID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Sign");
            this.Property(t => t.SearchSettings_SignID).HasColumnName("SearchSettings_SignID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.SignID).HasColumnName("SignID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Sign)
                .WithMany(t => t.SearchSettings_Sign)
                .HasForeignKey(d => d.SignID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Sign)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
