using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class visiblitysettingMap : EntityTypeConfiguration<visiblitysetting>
    {
        public visiblitysettingMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("visiblitysettings");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.agemaxvisibility).HasColumnName("agemaxvisibility");
            this.Property(t => t.ageminvisibility).HasColumnName("ageminvisibility");
            this.Property(t => t.chatvisiblitytointerests).HasColumnName("chatvisiblitytointerests");
            this.Property(t => t.chatvisiblitytolikes).HasColumnName("chatvisiblitytolikes");
            this.Property(t => t.chatvisiblitytomatches).HasColumnName("chatvisiblitytomatches");
            this.Property(t => t.chatvisiblitytopeeks).HasColumnName("chatvisiblitytopeeks");
            this.Property(t => t.chatvisiblitytosearch).HasColumnName("chatvisiblitytosearch");
            this.Property(t => t.lastupdatedate).HasColumnName("lastupdatedate");
            this.Property(t => t.mailchatrequest).HasColumnName("mailchatrequest");
            this.Property(t => t.mailintrests).HasColumnName("mailintrests");
            this.Property(t => t.maillikes).HasColumnName("maillikes");
            this.Property(t => t.mailmatches).HasColumnName("mailmatches");
            this.Property(t => t.mailnews).HasColumnName("mailnews");
            this.Property(t => t.mailpeeks).HasColumnName("mailpeeks");
            this.Property(t => t.profilevisiblity).HasColumnName("profilevisiblity");
            this.Property(t => t.saveofflinechat).HasColumnName("saveofflinechat");
            this.Property(t => t.steathpeeks).HasColumnName("steathpeeks");

            // Relationships
            this.HasRequired(t => t.profiledata)
                .WithMany(t => t.visiblitysettings)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
