using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class abusereportMap : EntityTypeConfiguration<abusereport>
    {
        public abusereportMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.RecordDate)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.AbuserID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("abusereport");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.RecordDate).HasColumnName("RecordDate");
            this.Property(t => t.AbuserID).HasColumnName("AbuserID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.ReporterComments).HasColumnName("ReporterComments");
            this.Property(t => t.AbuseTypeID).HasColumnName("AbuseTypeID");

            // Relationships
            this.HasRequired(t => t.ProfileData)
                .WithMany(t => t.abusereports)
                .HasForeignKey(d => d.ProfileID);
            this.HasRequired(t => t.abusetype)
                .WithMany(t => t.abusereports)
                .HasForeignKey(d => d.AbuseTypeID);

        }
    }
}
