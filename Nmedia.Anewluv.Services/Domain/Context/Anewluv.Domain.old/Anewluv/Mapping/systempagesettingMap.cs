using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class systempagesettingMap : EntityTypeConfiguration<systempagesetting>
    {
        public systempagesettingMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("systempagesettings");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.bodycssstylename).HasColumnName("bodycssstylename");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.hitCount).HasColumnName("hitCount");
            this.Property(t => t.ismasterpage).HasColumnName("ismasterpage");
            this.Property(t => t.path).HasColumnName("path");
            this.Property(t => t.title).HasColumnName("title");
        }
    }
}
