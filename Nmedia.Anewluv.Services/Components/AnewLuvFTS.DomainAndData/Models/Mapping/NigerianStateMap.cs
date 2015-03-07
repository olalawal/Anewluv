using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class NigerianStateMap : EntityTypeConfiguration<NigerianState>
    {
        public NigerianStateMap()
        {
            // Primary Key
            this.HasKey(t => t.NigerianStateID);

            // Properties
            this.Property(t => t.StateName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NigerianState");
            this.Property(t => t.NigerianStateID).HasColumnName("NigerianStateID");
            this.Property(t => t.StateName).HasColumnName("StateName");
        }
    }
}
