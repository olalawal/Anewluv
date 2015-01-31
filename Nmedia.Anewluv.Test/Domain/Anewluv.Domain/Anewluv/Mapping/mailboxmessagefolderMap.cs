using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class mailboxmessagefolderMap : EntityTypeConfiguration<mailboxmessagefolder>
    {
        public mailboxmessagefolderMap()
        {
            // Primary Key
            this.HasKey(t => new { t.mailboxfolder_id, t.mailboxmessage_id });

            // Properties
            this.Property(t => t.mailboxfolder_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mailboxmessage_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("mailboxmessagefolders");
            this.Property(t => t.mailboxfolder_id).HasColumnName("mailboxfolder_id");
            this.Property(t => t.mailboxmessage_id).HasColumnName("mailboxmessage_id");
            this.Property(t => t.deleteddate).HasColumnName("deleteddate");
            this.Property(t => t.draftdate).HasColumnName("draftdate");
            this.Property(t => t.flaggeddate).HasColumnName("flaggeddate");
            this.Property(t => t.readdate).HasColumnName("readdate");
            this.Property(t => t.recent).HasColumnName("recent");
            this.Property(t => t.replieddate).HasColumnName("replieddate");

            // Relationships
            this.HasRequired(t => t.mailboxfolder)
                .WithMany(t => t.mailboxmessagefolders)
                .HasForeignKey(d => d.mailboxfolder_id);
            this.HasRequired(t => t.mailboxmessage)
                .WithMany(t => t.mailboxmessagefolders)
                .HasForeignKey(d => d.mailboxmessage_id);

        }
    }
}
