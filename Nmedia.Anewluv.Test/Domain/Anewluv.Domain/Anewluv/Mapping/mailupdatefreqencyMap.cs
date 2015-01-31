using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class mailupdatefreqencyMap : EntityTypeConfiguration<mailupdatefreqency>
    {
        public mailupdatefreqencyMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("mailupdatefreqencies");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.updatefreqency).HasColumnName("updatefreqency");
            this.Property(t => t.profile_id).HasColumnName("profile_id");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.mailupdatefreqencies)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
