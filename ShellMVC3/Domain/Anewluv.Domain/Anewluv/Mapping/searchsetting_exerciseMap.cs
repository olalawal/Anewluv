using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_exerciseMap : EntityTypeConfiguration<searchsetting_exercise>
    {
        public searchsetting_exerciseMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_exercise");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.exercise_id).HasColumnName("exercise_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_exercise)
                .WithMany(t => t.searchsetting_exercise)
                .HasForeignKey(d => d.exercise_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_exercise)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
