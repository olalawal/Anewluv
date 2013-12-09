using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_professionMap : EntityTypeConfiguration<searchsetting_profession>
    {
        public searchsetting_professionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_profession");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profession_id).HasColumnName("profession_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_profession)
                .WithMany(t => t.searchsetting_profession)
                .HasForeignKey(d => d.profession_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_profession)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
