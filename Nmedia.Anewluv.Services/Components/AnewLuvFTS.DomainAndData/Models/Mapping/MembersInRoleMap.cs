using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MembersInRoleMap : EntityTypeConfiguration<MembersInRole>
    {
        public MembersInRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MembersInRole");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RoleStartDate).HasColumnName("RoleStartDate");
            this.Property(t => t.RoleExpireDate).HasColumnName("RoleExpireDate");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.MembersInRoles)
                .HasForeignKey(d => d.ProfileID);
            this.HasRequired(t => t.profile)
                .WithMany(t => t.MembersInRoles)
                .HasForeignKey(d => d.ProfileID);
            this.HasOptional(t => t.Role)
                .WithMany(t => t.MembersInRoles)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
