using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class openidMap : EntityTypeConfiguration<openid>
    {
        public openidMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("openids");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.openididentifier).HasColumnName("openididentifier");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.openidprovider_id).HasColumnName("openidprovider_id");

            // Relationships
            this.HasOptional(t => t.lu_openidprovider)
                .WithMany(t => t.openids)
                .HasForeignKey(d => d.openidprovider_id);
            this.HasRequired(t => t.profile)
                .WithMany(t => t.openids)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
