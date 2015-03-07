using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MailboxblockMap : EntityTypeConfiguration<Mailboxblock>
    {
        public MailboxblockMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BlockID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Mailboxblocks");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.MailboxBlockDate).HasColumnName("MailboxBlockDate");
            this.Property(t => t.BlockID).HasColumnName("BlockID");
            this.Property(t => t.BlockRemoved).HasColumnName("BlockRemoved");
            this.Property(t => t.BlockRemovedDate).HasColumnName("BlockRemovedDate");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.Mailboxblocks)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
