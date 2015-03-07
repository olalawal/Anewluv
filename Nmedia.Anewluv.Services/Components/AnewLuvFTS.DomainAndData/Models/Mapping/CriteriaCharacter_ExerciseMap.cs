using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_ExerciseMap : EntityTypeConfiguration<CriteriaCharacter_Exercise>
    {
        public CriteriaCharacter_ExerciseMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseID);

            // Properties
            this.Property(t => t.ExerciseName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Exercise");
            this.Property(t => t.ExerciseID).HasColumnName("ExerciseID");
            this.Property(t => t.ExerciseName).HasColumnName("ExerciseName");
        }
    }
}
