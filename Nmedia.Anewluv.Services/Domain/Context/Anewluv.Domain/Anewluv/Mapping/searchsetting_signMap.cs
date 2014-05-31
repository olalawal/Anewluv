using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_signMap : EntityTypeConfiguration<searchsetting_sign>
    {
        public searchsetting_signMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_sign");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.sign_id).HasColumnName("sign_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_sign)
                .WithMany(t => t.searchsetting_sign)
                .HasForeignKey(d => d.sign_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_sign)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
