using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MailboxMessagesFolderMap : EntityTypeConfiguration<MailboxMessagesFolder>
    {
        public MailboxMessagesFolderMap()
        {
            // Primary Key
            this.HasKey(t => t.MailboxMessagesFoldersID);

            // Properties
            // Table & Column Mappings
            this.ToTable("MailboxMessagesFolders");
            this.Property(t => t.MailboxMessagesFoldersID).HasColumnName("MailboxMessagesFoldersID");
            this.Property(t => t.MailboxFolderID).HasColumnName("MailboxFolderID");
            this.Property(t => t.MailBoxMessageID).HasColumnName("MailBoxMessageID");
            this.Property(t => t.MessageRead).HasColumnName("MessageRead");
            this.Property(t => t.MessageReplied).HasColumnName("MessageReplied");
            this.Property(t => t.MessageFlagged).HasColumnName("MessageFlagged");
            this.Property(t => t.MessageDraft).HasColumnName("MessageDraft");
            this.Property(t => t.MessageDeleted).HasColumnName("MessageDeleted");
            this.Property(t => t.MessageRecent).HasColumnName("MessageRecent");

            // Relationships
            this.HasOptional(t => t.MailboxFolder)
                .WithMany(t => t.MailboxMessagesFolders)
                .HasForeignKey(d => d.MailboxFolderID);
            this.HasOptional(t => t.MailboxMessage)
                .WithMany(t => t.MailboxMessagesFolders)
                .HasForeignKey(d => d.MailBoxMessageID);

        }
    }
}
