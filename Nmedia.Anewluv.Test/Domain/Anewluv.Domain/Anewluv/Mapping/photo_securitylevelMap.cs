using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photo_securitylevelMap : EntityTypeConfiguration<photo_securitylevel>
    {
        public photo_securitylevelMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photo_securitylevel");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.photo_id).HasColumnName("photo_id");
            this.Property(t => t.securityleveltype_id).HasColumnName("securityleveltype_id");

            // Relationships
            this.HasOptional(t => t.lu_securityleveltype)
                .WithMany(t => t.photo_securitylevel)
                .HasForeignKey(d => d.securityleveltype_id);
            this.HasRequired(t => t.photo)
                .WithMany(t => t.photo_securitylevel)
                .HasForeignKey(d => d.photo_id);

        }
    }
}
