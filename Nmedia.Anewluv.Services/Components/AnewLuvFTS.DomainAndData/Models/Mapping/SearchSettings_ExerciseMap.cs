using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SearchSettings_ExerciseMap : EntityTypeConfiguration<SearchSettings_Exercise>
    {
        public SearchSettings_ExerciseMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchSettings_ExerciseID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SearchSettings_Exercise");
            this.Property(t => t.SearchSettings_ExerciseID).HasColumnName("SearchSettings_ExerciseID");
            this.Property(t => t.SearchSettingsID).HasColumnName("SearchSettingsID");
            this.Property(t => t.ExerciseID).HasColumnName("ExerciseID");

            // Relationships
            this.HasOptional(t => t.CriteriaCharacter_Exercise)
                .WithMany(t => t.SearchSettings_Exercise)
                .HasForeignKey(d => d.ExerciseID);
            this.HasOptional(t => t.SearchSetting)
                .WithMany(t => t.SearchSettings_Exercise)
                .HasForeignKey(d => d.SearchSettingsID);

        }
    }
}
