using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MailboxFolderMap : EntityTypeConfiguration<MailboxFolder>
    {
        public MailboxFolderMap()
        {
            // Primary Key
            this.HasKey(t => t.MailboxFolderID);

            // Properties
            this.Property(t => t.MailboxFolderTypeName)
                .HasMaxLength(15);

            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MailboxFolders");
            this.Property(t => t.MailboxFolderID).HasColumnName("MailboxFolderID");
            this.Property(t => t.MailboxFolderTypeID).HasColumnName("MailboxFolderTypeID");
            this.Property(t => t.MailboxFolderTypeName).HasColumnName("MailboxFolderTypeName");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships
            this.HasOptional(t => t.MailboxFolderType)
                .WithMany(t => t.MailboxFolders)
                .HasForeignKey(d => d.MailboxFolderTypeID);
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.MailboxFolders)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
