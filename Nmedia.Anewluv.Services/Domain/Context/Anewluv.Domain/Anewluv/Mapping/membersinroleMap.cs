using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class membersinroleMap : EntityTypeConfiguration<membersinrole>
    {
        public membersinroleMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("membersinroles");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.roleexpiredate).HasColumnName("roleexpiredate");
            this.Property(t => t.rolestartdate).HasColumnName("rolestartdate");
            this.Property(t => t.role_id).HasColumnName("role_id");

            // Relationships
            this.HasRequired(t => t.lu_role)
                .WithMany(t => t.membersinroles)
                .HasForeignKey(d => d.role_id);
            this.HasRequired(t => t.profile)
                .WithMany(t => t.membersinroles)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
