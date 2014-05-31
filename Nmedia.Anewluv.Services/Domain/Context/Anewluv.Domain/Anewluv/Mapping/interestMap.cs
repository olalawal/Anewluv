using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class interestMap : EntityTypeConfiguration<interest>
    {
        public interestMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("interests");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.interestprofile_id).HasColumnName("interestprofile_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.viewdate).HasColumnName("viewdate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.deletedbymemberdate).HasColumnName("deletedbymemberdate");
            this.Property(t => t.deletedbyinterestdate).HasColumnName("deletedbyinterestdate");
            this.Property(t => t.mutual).HasColumnName("mutual");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.interests)
                .HasForeignKey(d => d.interestprofile_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.interests1)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
