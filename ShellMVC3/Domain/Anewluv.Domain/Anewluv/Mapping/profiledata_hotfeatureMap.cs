using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profiledata_hotfeatureMap : EntityTypeConfiguration<profiledata_hotfeature>
    {
        public profiledata_hotfeatureMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profiledata_hotfeature");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.hotfeature_id).HasColumnName("hotfeature_id");

            // Relationships
            this.HasOptional(t => t.lu_hotfeature)
                .WithMany(t => t.profiledata_hotfeature)
                .HasForeignKey(d => d.hotfeature_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.profiledata_hotfeature)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
