using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class ratingMap : EntityTypeConfiguration<rating>
    {
        public ratingMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ratings");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.ratingmaxvalue).HasColumnName("ratingmaxvalue");
            this.Property(t => t.ratingweight).HasColumnName("ratingweight");
            this.Property(t => t.increment).HasColumnName("increment");
        }
    }
}
