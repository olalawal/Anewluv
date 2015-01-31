using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class blocknoteMap : EntityTypeConfiguration<blocknote>
    {
        public blocknoteMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("blocknotes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.block_id).HasColumnName("block_id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.note).HasColumnName("note");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.reviewdate).HasColumnName("reviewdate");
            this.Property(t => t.notetype_id).HasColumnName("notetype_id");

            // Relationships
            this.HasRequired(t => t.block)
                .WithMany(t => t.blocknotes)
                .HasForeignKey(d => d.block_id);
            this.HasOptional(t => t.lu_notetype)
                .WithMany(t => t.blocknotes)
                .HasForeignKey(d => d.notetype_id);
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.blocknotes)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
