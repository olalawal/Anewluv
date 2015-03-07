using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class TribeMap : EntityTypeConfiguration<Tribe>
    {
        public TribeMap()
        {
            // Primary Key
            this.HasKey(t => t.TribeID);

            // Properties
            this.Property(t => t.TribeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Tribe");
            this.Property(t => t.TribeID).HasColumnName("TribeID");
            this.Property(t => t.TribeName).HasColumnName("TribeName");
        }
    }
}
