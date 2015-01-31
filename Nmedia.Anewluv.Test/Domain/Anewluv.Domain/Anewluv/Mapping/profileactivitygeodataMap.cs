using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profileactivitygeodataMap : EntityTypeConfiguration<profileactivitygeodata>
    {
        public profileactivitygeodataMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profileactivitygeodatas");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.regionname).HasColumnName("regionname");
            this.Property(t => t.continent).HasColumnName("continent");
            this.Property(t => t.countryId).HasColumnName("countryId");
            this.Property(t => t.countrycode).HasColumnName("countrycode");
            this.Property(t => t.countryname).HasColumnName("countryname");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.lattitude).HasColumnName("lattitude");
            this.Property(t => t.longitude).HasColumnName("longitude");
            this.Property(t => t.activity_id).HasColumnName("activity_id");

            // Relationships
            this.HasRequired(t => t.activity).WithMany()
                .HasForeignKey(u => u.activity_id);
            ;



        }
    }
}
