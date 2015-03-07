using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class profilestatusMap : EntityTypeConfiguration<profilestatus>
    {
        public profilestatusMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileStatusID);

            // Properties
            this.Property(t => t.ProfileStatusName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("profilestatuses");
            this.Property(t => t.ProfileStatusID).HasColumnName("ProfileStatusID");
            this.Property(t => t.ProfileStatusName).HasColumnName("ProfileStatusName");
        }
    }
}
