using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photoMap : EntityTypeConfiguration<photo>
    {
        public photoMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photos");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.size).HasColumnName("size");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.imagecaption).HasColumnName("imagecaption");
            this.Property(t => t.imagename).HasColumnName("imagename");
            this.Property(t => t.providername).HasColumnName("providername");
            this.Property(t => t.rejectionreason_id).HasColumnName("rejectionreason_id");
            this.Property(t => t.photostatus_id).HasColumnName("photostatus_id");
            this.Property(t => t.approvalstatus_id).HasColumnName("approvalstatus_id");
            this.Property(t => t.imagetype_id).HasColumnName("imagetype_id");

            // Relationships
            this.HasOptional(t => t.lu_photoapprovalstatus)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.approvalstatus_id);
            this.HasOptional(t => t.lu_photoimagetype)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.imagetype_id);
            this.HasOptional(t => t.lu_photorejectionreason)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.rejectionreason_id);
            this.HasOptional(t => t.lu_photostatus)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.photostatus_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.photos)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
