using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_livingstituationMap : EntityTypeConfiguration<searchsetting_livingstituation>
    {
        public searchsetting_livingstituationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_livingstituation");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.livingsituation_id).HasColumnName("livingsituation_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_livingsituation)
                .WithMany(t => t.searchsetting_livingstituation)
                .HasForeignKey(d => d.livingsituation_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_livingstituation)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
