using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class profileOpenIDStoreMap : EntityTypeConfiguration<profileOpenIDStore>
    {
        public profileOpenIDStoreMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProfileID)
                .HasMaxLength(255);

            this.Property(t => t.openidIdentifier)
                .HasMaxLength(255);

            this.Property(t => t.openidProviderName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("profileOpenIDStore");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.openidIdentifier).HasColumnName("openidIdentifier");
            this.Property(t => t.openidProviderName).HasColumnName("openidProviderName");
            this.Property(t => t.creationDate).HasColumnName("creationDate");
            this.Property(t => t.active).HasColumnName("active");

            // Relationships
            this.HasOptional(t => t.profile)
                .WithMany(t => t.profileOpenIDStores)
                .HasForeignKey(d => d.ProfileID);

        }
    }
}
