using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class ShowMeMap : EntityTypeConfiguration<ShowMe>
    {
        public ShowMeMap()
        {
            // Primary Key
            this.HasKey(t => t.ShowMeID);

            // Properties
            this.Property(t => t.ShowMeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShowMe");
            this.Property(t => t.ShowMeID).HasColumnName("ShowMeID");
            this.Property(t => t.ShowMeName).HasColumnName("ShowMeName");
        }
    }
}
