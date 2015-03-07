using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_EthnicityMap : EntityTypeConfiguration<SearchSettings_Ethnicity>
    {
        public SearchSettings_EthnicityMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_EthnicitiesID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Ethnicity");
            this.Property(t => t.SearchSettings_EthnicitiesID).HasColumnName("SearchSettings_EthnicitiesID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.EthicityID).HasColumnName("EthicityID");

            // Relationships
            this.HasOptional(t => t.CriteriaAppearance_Ethnicity)
                .WithMany(t => t.SearchSettings_Ethnicity)
                .HasForeignKey(d => d.EthicityID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Ethnicity)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
