using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
   public class photophotoalbumMap : EntityTypeConfiguration<photophotoalbum>
    {

        public photophotoalbumMap()
    {



        // Primary Key       
        this.HasKey(t => t.photophotoalbumid);

        // Properties
        // Table & Column Mappings
        this.ToTable(" photophotoalbum");
        this.Property(t => t.photophotoalbumid).HasColumnName("photophotoalbumid");

        // Properties
        this.Property(t => t.photo_id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        this.Property(t => t.photoalbum_id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        // Relationships
        this.HasRequired(t => t.photo)
            .WithMany(t => t.photophotoalbums)
            .HasForeignKey(d => d.photo_id).WillCascadeOnDelete(false);

        this.HasRequired(t => t.photoalbum)
            .WithMany(t => t.photophotoalbums)
            .HasForeignKey(d => d.photoalbum_id).WillCascadeOnDelete(false);

    }
    }
}
