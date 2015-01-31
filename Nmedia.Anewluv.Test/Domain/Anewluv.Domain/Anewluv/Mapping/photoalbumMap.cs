using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class photoalbumMap : EntityTypeConfiguration<photoalbum>
    {
        public photoalbumMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("photoalbums");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.profile_id).HasColumnName("profile_id");

            // Relationships
            this.HasMany(t => t.photos)
                .WithMany(t => t.photoalbums)
                .Map(m =>
                    {
                        m.ToTable("photophotoalbums");
                        m.MapLeftKey("photoalbum_id");
                        m.MapRightKey("photo_id");
                    });

            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.photoalbums)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
