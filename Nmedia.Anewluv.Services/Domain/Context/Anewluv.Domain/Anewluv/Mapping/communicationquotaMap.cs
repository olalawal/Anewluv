using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class communicationquotaMap : EntityTypeConfiguration<communicationquota>
    {
        public communicationquotaMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("communicationquotas");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.quotadescription).HasColumnName("quotadescription");
            this.Property(t => t.quotaname).HasColumnName("quotaname");
            this.Property(t => t.quotaroleid).HasColumnName("quotaroleid");
            this.Property(t => t.quotavalue).HasColumnName("quotavalue");
            this.Property(t => t.updaterprofile_id).HasColumnName("updaterprofile_id");
            this.Property(t => t.updatedate).HasColumnName("updatedate");
            this.Property(t => t.updaterprofiledata_profile_id).HasColumnName("updaterprofiledata_profile_id");

            // Relationships
            this.HasRequired(t => t.profiledata)
                .WithMany(t => t.communicationquotas)
                .HasForeignKey(d => d.updaterprofiledata_profile_id);

        }
    }
}
