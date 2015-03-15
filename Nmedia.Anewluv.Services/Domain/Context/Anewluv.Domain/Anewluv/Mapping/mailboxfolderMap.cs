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
    
          
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.displayname).HasColumnName("displayname");   
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.deleteddate).HasColumnName("deleteddate");
            this.Property(t => t.maxsizeinbytes).HasColumnName("maxsizeinbytes");
            this.Property(t => t.defaultfolder_id).HasColumnName("defaultfolder_id");

            // Relationships
            this.HasOptional(t => t.lu_defaultmailboxfolder)
                .WithMany(t => t.mailboxfolders)
                .HasForeignKey(d => d.defaultfolder_id);


            
             
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.mailboxfolders)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
