using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profilemetadataMap : EntityTypeConfiguration<profilemetadata>
    {
        public profilemetadataMap()
        {
            // Primary Key
            this.HasKey(t => t.profile_id);

            // Properties
            this.Property(t => t.profile_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("profilemetadatas");
            this.Property(t => t.profile_id).HasColumnName("profile_id");

            // Relationships
            this.HasRequired(t => t.profile)
                .WithOptional(t => t.profilemetadata);

        }
    }
}
