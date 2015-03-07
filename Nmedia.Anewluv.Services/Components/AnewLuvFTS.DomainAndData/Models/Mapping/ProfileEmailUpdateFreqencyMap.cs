using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ProfileEmailUpdateFreqencyMap : EntityTypeConfiguration<ProfileEmailUpdateFreqency>
    {
        public ProfileEmailUpdateFreqencyMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProfileEmailUpdateFreqency");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.EmailUpdateFreqency).HasColumnName("EmailUpdateFreqency");

            // Relationships
            this.HasOptional(t => t.ProfileData)
                .WithMany(t => t.ProfileEmailUpdateFreqencies)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
