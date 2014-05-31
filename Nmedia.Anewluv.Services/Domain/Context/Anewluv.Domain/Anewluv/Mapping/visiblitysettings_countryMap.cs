using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class visiblitysettings_countryMap : EntityTypeConfiguration<visiblitysettings_country>
    {
        public visiblitysettings_countryMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("visiblitysettings_country");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.countryId).HasColumnName("countryId");
            this.Property(t => t.countryname).HasColumnName("countryname");
            this.Property(t => t.visiblitysetting_id).HasColumnName("visiblitysetting_id");

            // Relationships
            this.HasRequired(t => t.visiblitysetting)
                .WithMany(t => t.visiblitysettings_country)
                .HasForeignKey(d => d.visiblitysetting_id);

        }
    }
}
