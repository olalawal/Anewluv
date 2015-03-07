using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_BodyTypesMap : EntityTypeConfiguration<SearchSettings_BodyTypes>
    {
        public SearchSettings_BodyTypesMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_BodyTypeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_BodyTypes");
            this.Property(t => t.SearchSettings_BodyTypeID).HasColumnName("SearchSettings_BodyTypeID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.BodyTypesID).HasColumnName("BodyTypesID");

            // Relationships
            this.HasOptional(t => t.CriteriaAppearance_Bodytypes)
                .WithMany(t => t.SearchSettings_BodyTypes)
                .HasForeignKey(d => d.BodyTypesID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_BodyTypes)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
