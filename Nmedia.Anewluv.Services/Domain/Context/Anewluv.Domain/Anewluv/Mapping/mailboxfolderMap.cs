using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class mailboxfolderMap : EntityTypeConfiguration<mailboxfolder>
    {
        public mailboxfolderMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("mailboxfolders");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.foldertype_id).HasColumnName("foldertype_id");

            // Relationships
            this.HasOptional(t => t.mailboxfoldertype)
                .WithMany(t => t.mailboxfolders)
                .HasForeignKey(d => d.foldertype_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.mailboxfolders)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
