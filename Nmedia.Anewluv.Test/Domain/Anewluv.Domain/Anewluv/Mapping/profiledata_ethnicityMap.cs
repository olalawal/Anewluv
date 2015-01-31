using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profiledata_ethnicityMap : EntityTypeConfiguration<profiledata_ethnicity>
    {
        public profiledata_ethnicityMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profiledata_ethnicity");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.ethnicty_id).HasColumnName("ethnicty_id");

            // Relationships
            this.HasOptional(t => t.lu_ethnicity)
                .WithMany(t => t.profiledata_ethnicity)
                .HasForeignKey(d => d.ethnicty_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.profiledata_ethnicity)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
