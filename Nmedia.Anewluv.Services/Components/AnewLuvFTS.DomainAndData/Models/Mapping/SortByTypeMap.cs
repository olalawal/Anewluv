using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SortByTypeMap : EntityTypeConfiguration<SortByType>
    {
        public SortByTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SortByTypeID);

            // Properties
            this.Property(t => t.SortByName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SortByType");
            this.Property(t => t.SortByTypeID).HasColumnName("SortByTypeID");
            this.Property(t => t.SortByName).HasColumnName("SortByName");
        }
    }
}
