using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class databaseerrorMap : EntityTypeConfiguration<databaseerror>
    {
        public databaseerrorMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrorID);

            // Properties
            this.Property(t => t.ErrorDate)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.SqlStatement)
                .IsRequired();

            this.Property(t => t.Exception)
                .IsRequired();

            this.Property(t => t.PageName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("databaseerrors");
            this.Property(t => t.ErrorID).HasColumnName("ErrorID");
            this.Property(t => t.ErrorDate).HasColumnName("ErrorDate");
            this.Property(t => t.SqlStatement).HasColumnName("SqlStatement");
            this.Property(t => t.Exception).HasColumnName("Exception");
            this.Property(t => t.PageName).HasColumnName("PageName");
        }
    }
}
