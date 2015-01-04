using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profileactivityMap : EntityTypeConfiguration<profileactivity>
    {
        public profileactivityMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profileactivities");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.ipaddress).HasColumnName("ipaddress");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.sessionid).HasColumnName("sessionid");
            this.Property(t => t.useragent).HasColumnName("useragent");
            this.Property(t => t.routeurl).HasColumnName("routeurl");
            this.Property(t => t.actionname).HasColumnName("actionname");
            this.Property(t => t.profileactivitygeodata_id).HasColumnName("profileactivitygeodata_id");
            this.Property(t => t.activitytype_id).HasColumnName("activitytype_id");

            // Relationships
            this.HasOptional(t => t.lu_activitytype)
                .WithMany(t => t.profileactivities)
                .HasForeignKey(d => d.activitytype_id);
            this.HasOptional(t => t.profileactivitygeodata)
                .WithMany(t => t.profileactivities)
                .HasForeignKey(d => d.profileactivitygeodata_id);
            this.HasRequired(t => t.profile)
                .WithMany(t => t.profileactivities)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
