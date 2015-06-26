using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class applicationroleMap : EntityTypeConfiguration<applicationrole>
    {
        public applicationroleMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("applicationroles");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.application_id).HasColumnName("application_id");
            this.Property(t => t.roleexpiredate).HasColumnName("roleexpiredate");
            this.Property(t => t.rolestartdate).HasColumnName("rolestartdate");
            this.Property(t => t.deactivationdate).HasColumnName("deactivationdate");
            this.Property(t => t.creationdate).HasColumnName("creationdate");           
            this.Property(t => t.role_id).HasColumnName("role_id");

            // Relationships

            this.HasRequired(t => t.lu_role)
        .WithMany(t => t.applicationroles)
        .HasForeignKey(d => d.role_id);

            this.HasRequired(t => t.application)
       .WithMany(t => t.applicationroles)
       .HasForeignKey(d => d.application_id);


        }
    }
}
