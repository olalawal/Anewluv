using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class genderMap : EntityTypeConfiguration<gender>
    {
        public genderMap()
        {
            // Primary Key
            this.HasKey(t => t.GenderID);

            // Properties
            this.Property(t => t.GenderName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("genders");
            this.Property(t => t.GenderID).HasColumnName("GenderID");
            this.Property(t => t.GenderName).HasColumnName("GenderName");
        }
    }
}
