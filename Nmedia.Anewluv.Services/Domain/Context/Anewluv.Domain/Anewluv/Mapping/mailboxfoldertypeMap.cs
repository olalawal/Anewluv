using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class mailboxfoldertypeMap : EntityTypeConfiguration<mailboxfoldertype>
    {
        public mailboxfoldertypeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("mailboxfoldertypes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.deleteddate).HasColumnName("deleteddate");
            this.Property(t => t.maxsize).HasColumnName("maxsize");
          //  this.Property(t => t.defaultfolder).HasColumnName("defaultfolder_id");

                                
            // Relationships
            this.HasOptional(t => t.defaultfolder);
               // .WithMany(t => t.mailboxfolders)
                //.HasForeignKey(d => d.defaultfolder_id);

        }
    }
}
