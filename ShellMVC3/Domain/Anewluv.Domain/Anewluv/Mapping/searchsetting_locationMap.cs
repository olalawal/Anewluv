using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsetting_locationMap : EntityTypeConfiguration<searchsetting_location>
    {
        public searchsetting_locationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsetting_location");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.countryid).HasColumnName("countryid");
            this.Property(t => t.postalcode).HasColumnName("postalcode");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");

            // Relationships
            this.HasOptional(t => t.searchsetting)
                .WithMany(t => t.searchsetting_location)
                .HasForeignKey(d => d.searchsetting_id);

        }
    }
}
