using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class ratingvalueMap : EntityTypeConfiguration<ratingvalue>
    {
        public ratingvalueMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ratingvalues");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.rating_id).HasColumnName("rating_id");
            this.Property(t => t.raterprofile_id).HasColumnName("raterprofile_id");
            this.Property(t => t.rateeprofile_id).HasColumnName("rateeprofile_id");
            this.Property(t => t.date).HasColumnName("date");
            this.Property(t => t.value).HasColumnName("value");

            // Relationships
            this.HasRequired(t => t.raterprofilemetadata)
                .WithMany(t => t.raterratingvalues)
                .HasForeignKey(d => d.raterprofile_id);
          
            this.HasRequired(t => t.rateeprofilemetadata)
                .WithMany(t => t.rateeratingvalues)
                .HasForeignKey(d => d.rateeprofile_id);

            this.HasRequired(t => t.rating)
                .WithMany(t => t.ratingvalues)
                .HasForeignKey(d => d.rating_id);

        }
    }
}
