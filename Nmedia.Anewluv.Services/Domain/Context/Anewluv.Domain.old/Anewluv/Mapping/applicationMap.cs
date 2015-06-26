using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class applicationMap : EntityTypeConfiguration<application>
    {
        public applicationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("applications");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.applicationtype_id).HasColumnName("applicationtype_id");
            this.Property(t => t.transfertype_id).HasColumnName("transfertype_id");
            this.Property(t => t.paymenttype_id).HasColumnName("paymenttype_id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.purchaserprofile_id).HasColumnName("purchaserprofile_id");

            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.deactivationdate).HasColumnName("deactivationdate");
            this.Property(t => t.purchasedate).HasColumnName("purchasedate");
            this.Property(t => t.lasttransferdate).HasColumnName("lasttransferdate");
            this.Property(t => t.activateddate).HasColumnName("activateddate");

            this.Property(t => t.expirationdate).HasColumnName("expirationdate");
        


            // Relationships


            this.HasRequired(t => t.lu_applicationpaymenttype)
            .WithMany(t => t.applications)
            .HasForeignKey(d => d.paymenttype_id);

            this.HasRequired(t => t.lu_applicationtype)
    .WithMany(t => t.applications)
    .HasForeignKey(d => d.applicationtype_id);

            this.HasRequired(t => t.lu_applicationtransfertype)
    .WithMany(t => t.applications)
    .HasForeignKey(d => d.applicationtype_id);

            this.HasMany(t => t.applicationiconconversions).WithRequired
      (t => t.application).HasForeignKey(z=>z.application_id);

            this.HasMany(t => t.applicationroles).WithRequired(z => z.application).HasForeignKey(z => z.application_id);

            this.HasRequired(t => t.profilemetadata)
            .WithMany(t => t.applications)
            .HasForeignKey(d => d.profile_id).WillCascadeOnDelete(false); ;


           
          
        }
    }
}
