using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class mailboxmessageMap : EntityTypeConfiguration<mailboxmessage>
    {
        public mailboxmessageMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("mailboxmessages");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.recipient_id).HasColumnName("recipient_id");
            this.Property(t => t.sender_id).HasColumnName("sender_id");
            this.Property(t => t.body).HasColumnName("body");
            this.Property(t => t.subject).HasColumnName("subject");
            this.Property(t => t.uniqueid).HasColumnName("uniqueid");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.mailboxmessages)
                .HasForeignKey(d => d.recipient_id);
            this.HasRequired(t => t.profilemetadata1)
                .WithMany(t => t.mailboxmessages1)
                .HasForeignKey(d => d.sender_id);

        }
    }
}
