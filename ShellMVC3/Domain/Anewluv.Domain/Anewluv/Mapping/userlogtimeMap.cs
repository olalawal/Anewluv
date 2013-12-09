using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class userlogtimeMap : EntityTypeConfiguration<userlogtime>
    {
        public userlogtimeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("userlogtimes");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.logintime).HasColumnName("logintime");
            this.Property(t => t.logouttime).HasColumnName("logouttime");
            this.Property(t => t.offline).HasColumnName("offline");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.sessionid).HasColumnName("sessionid");

            // Relationships
            this.HasRequired(t => t.profile)
                .WithMany(t => t.userlogtimes)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
