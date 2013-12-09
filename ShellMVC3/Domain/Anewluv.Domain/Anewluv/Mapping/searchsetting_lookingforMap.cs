using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_lookingforMap : EntityTypeConfiguration<searchsetting_lookingfor>
    {
        public searchsetting_lookingforMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_lookingfor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.lookingfor_id).HasColumnName("lookingfor_id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.lu_lookingfor)
                .WithMany(t => t.searchsetting_lookingfor)
                .HasForeignKey(d => d.lookingfor_id);
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_lookingfor)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
