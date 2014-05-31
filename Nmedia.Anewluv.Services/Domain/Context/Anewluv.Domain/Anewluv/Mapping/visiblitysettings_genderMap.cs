using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class visiblitysettings_genderMap : EntityTypeConfiguration<visiblitysettings_gender>
    {
        public visiblitysettings_genderMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("visiblitysettings_gender");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.visiblitysetting_id).HasColumnName("visiblitysetting_id");
            this.Property(t => t.gender_id).HasColumnName("gender_id");

            // Relationships
            this.HasOptional(t => t.lu_gender)
                .WithMany(t => t.visiblitysettings_gender)
                .HasForeignKey(d => d.gender_id);
            this.HasRequired(t => t.visiblitysetting)
                .WithMany(t => t.visiblitysettings_gender)
                .HasForeignKey(d => d.visiblitysetting_id);

        }
    }
}
