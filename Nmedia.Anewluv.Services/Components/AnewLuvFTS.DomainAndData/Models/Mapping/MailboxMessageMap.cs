using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MailboxMessageMap : EntityTypeConfiguration<MailboxMessage>
    {
        public MailboxMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MailboxMessageID);

            // Properties
            this.Property(t => t.SenderID)
                .HasMaxLength(50);

            this.Property(t => t.RecipientID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MailboxMessages");
            this.Property(t => t.MailboxMessageID).HasColumnName("MailboxMessageID");
            this.Property(t => t.SenderID).HasColumnName("SenderID");
            this.Property(t => t.RecipientID).HasColumnName("RecipientID");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.uniqueID).HasColumnName("uniqueID");
        }
    }
}
