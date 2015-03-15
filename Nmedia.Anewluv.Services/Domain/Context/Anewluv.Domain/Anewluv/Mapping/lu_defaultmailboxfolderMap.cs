using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class lu_defaultmailboxfolderMap : EntityTypeConfiguration<lu_defaultmailboxfolder>
    {
        public lu_defaultmailboxfolderMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("lu_defaultmailboxfolder");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.description).HasColumnName("description");

          //  this.HasRequired(t => t.mailboxfolders).WithOptional
               
               

        }
    }
}
