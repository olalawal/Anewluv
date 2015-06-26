using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profiledata_hobbyMap : EntityTypeConfiguration<profiledata_hobby>
    {
        public profiledata_hobbyMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profiledata_hobby");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.hobby_id).HasColumnName("hobby_id");

            // Relationships
            this.HasOptional(t => t.lu_hobby)
                .WithMany(t => t.profiledata_hobby)
                .HasForeignKey(d => d.hobby_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.profiledata_hobby)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
