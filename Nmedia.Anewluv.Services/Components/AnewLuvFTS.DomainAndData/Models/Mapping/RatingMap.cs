using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class RatingMap : EntityTypeConfiguration<Rating>
    {
        public RatingMap()
        {
            // Primary Key
            this.HasKey(t => t.RatingID);

            // Properties
            this.Property(t => t.RatingDescription)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Ratings");
            this.Property(t => t.RatingID).HasColumnName("RatingID");
            this.Property(t => t.RatingDescription).HasColumnName("RatingDescription");
            this.Property(t => t.RatingMaxValue).HasColumnName("RatingMaxValue");
            this.Property(t => t.RatingWeight).HasColumnName("RatingWeight");
        }
    }
}
