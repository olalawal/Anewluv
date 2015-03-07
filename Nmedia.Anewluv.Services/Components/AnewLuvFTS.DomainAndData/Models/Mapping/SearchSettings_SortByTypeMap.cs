using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_SortByTypeMap : EntityTypeConfiguration<SearchSettings_SortByType>
    {
        public SearchSettings_SortByTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_SortByType1);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_SortByType");
            this.Property(t => t.SearchSettings_SortByType1).HasColumnName("SearchSettings_SortByType");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.SortByTypeID).HasColumnName("SortByTypeID");

            // Relationships
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_SortByType)
                .HasForeignKey(d => d.SearchSettingsID);
            this.HasOptional(t => t.SortByType)
                .WithMany(t => t.SearchSettings_SortByType)
                .HasForeignKey(d => d.SortByTypeID);

        }
    }
}
