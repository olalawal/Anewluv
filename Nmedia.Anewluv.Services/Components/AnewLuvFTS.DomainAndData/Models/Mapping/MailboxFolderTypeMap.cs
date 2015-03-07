using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class MailboxFolderTypeMap : EntityTypeConfiguration<MailboxFolderType>
    {
        public MailboxFolderTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.MailboxFolderTypeID);

            // Properties
            this.Property(t => t.MailboxFolderTypeName)
                .HasMaxLength(50);

            this.Property(t => t.MailboxFolderTypeDescription)
                .HasMaxLength(50);

            this.Property(t => t.FolderType)
                .IsFixedLength()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("MailboxFolderTypes");
            this.Property(t => t.MailboxFolderTypeID).HasColumnName("MailboxFolderTypeID");
            this.Property(t => t.MailboxFolderTypeName).HasColumnName("MailboxFolderTypeName");
            this.Property(t => t.MailboxFolderTypeDescription).HasColumnName("MailboxFolderTypeDescription");
            this.Property(t => t.FolderType).HasColumnName("FolderType");
        }
    }
}
