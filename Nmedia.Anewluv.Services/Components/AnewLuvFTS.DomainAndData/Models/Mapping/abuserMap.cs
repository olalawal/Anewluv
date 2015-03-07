using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class abuserMap : EntityTypeConfiguration<abuser>
    {
        public abuserMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileID);

            // Properties
            this.Property(t => t.ProfileID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.DeletionDate)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.AbuserUserName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("abuser");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.SuspensionDate).HasColumnName("SuspensionDate");
            this.Property(t => t.DeletionDate).HasColumnName("DeletionDate");
            this.Property(t => t.FlagTypeID).HasColumnName("FlagTypeID");
            this.Property(t => t.AbuserUserName).HasColumnName("AbuserUserName");
        }
    }
}
