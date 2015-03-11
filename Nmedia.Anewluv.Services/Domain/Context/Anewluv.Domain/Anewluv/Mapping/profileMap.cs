using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profileMap : EntityTypeConfiguration<profile>
    {
        public profileMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("profiles");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.emailaddress).HasColumnName("emailaddress");
            this.Property(t => t.screenname).HasColumnName("screenname");
            this.Property(t => t.activationcode).HasColumnName("activationcode");
            this.Property(t => t.dailsentmessagequota).HasColumnName("dailsentmessagequota");
            this.Property(t => t.dailysentemailquota).HasColumnName("dailysentemailquota");
            this.Property(t => t.forwardmessages).HasColumnName("forwardmessages");
            this.Property(t => t.logindate).HasColumnName("logindate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.readprivacystatement).HasColumnName("readprivacystatement");
            this.Property(t => t.readtemsofuse).HasColumnName("readtemsofuse");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.passwordChangeddate).HasColumnName("passwordChangeddate");
            this.Property(t => t.passwordchangecount).HasColumnName("passwordchangecount");
            this.Property(t => t.failedpasswordchangedate).HasColumnName("failedpasswordchangedate");
            this.Property(t => t.failedpasswordchangeattemptcount).HasColumnName("failedpasswordchangeattemptcount");
            this.Property(t => t.salt).HasColumnName("salt");
            this.Property(t => t.securityanswer).HasColumnName("securityanswer");
            this.Property(t => t.sentemailquotahitcount).HasColumnName("sentemailquotahitcount");
            this.Property(t => t.sentmessagequotahitcount).HasColumnName("sentmessagequotahitcount");
            this.Property(t => t.status_id).HasColumnName("status_id");
            this.Property(t => t.securityquestion_id).HasColumnName("securityquestion_id");

            // Relationships
            this.HasRequired(t => t.lu_profilestatus)
                .WithMany(t => t.profiles)
                .HasForeignKey(d => d.status_id);
            this.HasOptional(t => t.lu_securityquestion)
                .WithMany(t => t.profiles)
                .HasForeignKey(d => d.securityquestion_id);

        }
    }
}
