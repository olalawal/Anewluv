using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profiledata_lookingforMap : EntityTypeConfiguration<profiledata_lookingfor>
    {
        public profiledata_lookingforMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profiledata_lookingfor");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.lookingfor_id).HasColumnName("lookingfor_id");

            // Relationships
            this.HasOptional(t => t.lu_lookingfor)
                .WithMany(t => t.profiledata_lookingfor)
                .HasForeignKey(d => d.lookingfor_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.profiledata_lookingfor)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
