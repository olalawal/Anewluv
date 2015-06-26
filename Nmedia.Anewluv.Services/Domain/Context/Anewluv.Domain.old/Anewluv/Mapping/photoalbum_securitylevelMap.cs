using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photoalbum_securitylevelMap : EntityTypeConfiguration<photoalbum_securitylevel>
    {
        public photoalbum_securitylevelMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photoalbum_securitylevel");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.photoalbum_id).HasColumnName("photoalbum_id");
            this.Property(t => t.securityleveltype_id).HasColumnName("securityleveltype_id");

            // Relationships
            this.HasOptional(t => t.lu_securityleveltype)
                .WithMany(t => t.photoalbum_securitylevel)
                .HasForeignKey(d => d.securityleveltype_id);
            this.HasRequired(t => t.photoalbum)
                .WithMany(t => t.photoalbum_securitylevel)
                .HasForeignKey(d => d.photoalbum_id);

        }
    }
}
