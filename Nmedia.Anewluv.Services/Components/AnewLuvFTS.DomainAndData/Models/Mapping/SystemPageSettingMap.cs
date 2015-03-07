using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SystemPageSettingMap : EntityTypeConfiguration<SystemPageSetting>
    {
        public SystemPageSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Titile);

            // Properties
            this.Property(t => t.Titile)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Path)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .IsFixedLength()
                .HasMaxLength(255);

            this.Property(t => t.BodyCssSyleName)
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SystemPageSettings");
            this.Property(t => t.Titile).HasColumnName("Titile");
            this.Property(t => t.Path).HasColumnName("Path");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BodyCssSyleName).HasColumnName("BodyCssSyleName");
            this.Property(t => t.HitCount).HasColumnName("HitCount");
            this.Property(t => t.IsMasterPage).HasColumnName("IsMasterPage");
        }
    }
}
