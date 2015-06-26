using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class noteMap : EntityTypeConfiguration<note>
    {
        public noteMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("notes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.notetype_id).HasColumnName("notetype_id");
            this.Property(t => t.action_id).HasColumnName("action_id");
            this.Property(t => t.abusetype_id).HasColumnName("abusetype_id");
   

            this.Property(t => t.creationdate).HasColumnName("creationdate");       
            this.Property(t => t.reviewdate).HasColumnName("reviewdate");
       

            // Relationships
            this.HasRequired(t => t.lu_notetype)
                .WithMany(t => t.notes)
                .HasForeignKey(d => d.notetype_id);

            this.HasRequired(t => t.action)
                .WithMany(t => t.notes)
                .HasForeignKey(d => d.action_id);


            this.HasOptional(t => t.lu_abusetype)
                .WithMany(t => t.abusereports)
                .HasForeignKey(d => d.abusetype_id);


        }
    }
}
