using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_drinkMap : EntityTypeConfiguration<searchsetting_drink>
    {
        public searchsetting_drinkMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_drink");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.drink_id).HasColumnName("drink_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_drinks)
                .WithMany(t => t.searchsetting_drink)
                .HasForeignKey(d => d.drink_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_drink)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
