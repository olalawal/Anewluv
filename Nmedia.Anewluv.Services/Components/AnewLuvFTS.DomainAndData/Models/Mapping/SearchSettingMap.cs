using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettingMap : EntityTypeConfiguration<SearchSetting>
    {
        public SearchSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettingsID);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.SearchName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SearchSettings");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.SearchName).HasColumnName("SearchName");
            this.Property(t => t.SearchRank).HasColumnName("SearchRank");
            this.Property(t => t.DistanceFromMe).HasColumnName("DistanceFromMe");
            this.Property(t => t.AgeMin).HasColumnName("AgeMin");
            this.Property(t => t.AgeMax).HasColumnName("AgeMax");
            this.Property(t => t.HeightMin).HasColumnName("HeightMin");
            this.Property(t => t.HeightMax).HasColumnName("HeightMax");
            this.Property(t => t.MyPerfectMatch).HasColumnName("MyPerfectMatch");
            this.Property(t => t.SystemMatch).HasColumnName("SystemMatch");
            this.Property(t => t.SavedSearch).HasColumnName("SavedSearch");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.SearchSettings)
                .HasForeignKey(d => d.ProfileID);
            this.HasOptional(t => t.ProfileData1)
                .WithMany(t => t.SearchSettings1)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
