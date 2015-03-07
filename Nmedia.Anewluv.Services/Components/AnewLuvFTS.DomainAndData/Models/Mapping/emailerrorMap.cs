using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class emailerrorMap : EntityTypeConfiguration<emailerror>
    {
        public emailerrorMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailErrorID);

            // Properties
            this.Property(t => t.FromEmail)
                .IsRequired();

            this.Property(t => t.ToEmail)
                .IsRequired();

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Body)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("emailerror");
            this.Property(t => t.EmailErrorID).HasColumnName("EmailErrorID");
            this.Property(t => t.FromEmail).HasColumnName("FromEmail");
            this.Property(t => t.ToEmail).HasColumnName("ToEmail");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.ErrorDate).HasColumnName("ErrorDate");
            this.Property(t => t.ExceptionError).HasColumnName("ExceptionError");
        }
    }
}
