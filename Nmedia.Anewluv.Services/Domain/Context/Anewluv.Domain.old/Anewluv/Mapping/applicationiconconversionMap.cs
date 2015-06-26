using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class applicationiconconversionMap : EntityTypeConfiguration<applicationiconconversion>
    {
        public applicationiconconversionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("applicationiconconversions");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.application_id).HasColumnName("application_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.image).HasColumnName("image");
            this.Property(t => t.size).HasColumnName("size");          
            this.Property(t => t.iconformat_id).HasColumnName("iconformat_id");

            // Relationships
           
            this.HasOptional(t => t.lu_iconformat)
                .WithMany(t => t.applicationiconconversions)
                .HasForeignKey(d => d.iconformat_id);

            this.HasRequired(t => t.application)
       .WithMany(t => t.applicationiconconversions)
       .HasForeignKey(d => d.application_id);


        }
    }
}
