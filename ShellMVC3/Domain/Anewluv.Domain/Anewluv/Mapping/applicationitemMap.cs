using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class applicationitemMap : EntityTypeConfiguration<applicationitem>
    {
        public applicationitemMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("applicationitems");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.application_id).HasColumnName("application_id");
            this.Property(t => t.purchaserprofile_id).HasColumnName("purchaserprofile_id");
            this.Property(t => t.transferprofile_id).HasColumnName("transferprofile_id");
            this.Property(t => t.purchasedate).HasColumnName("purchasedate");
            this.Property(t => t.transferdate).HasColumnName("transferdate");
            this.Property(t => t.activateddate).HasColumnName("activateddate");
            this.Property(t => t.expirationdate).HasColumnName("expirationdate");
            this.Property(t => t.application_id1).HasColumnName("application_id1");
            this.Property(t => t.purchaserprofile_id1).HasColumnName("purchaserprofile_id1");
            this.Property(t => t.transferprofile_id1).HasColumnName("transferprofile_id1");
            this.Property(t => t.transfertype_id).HasColumnName("transfertype_id");
            this.Property(t => t.paymenttype_id).HasColumnName("paymenttype_id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.profile_id1).HasColumnName("profile_id1");

            // Relationships
            this.HasOptional(t => t.application)
                .WithMany(t => t.applicationitems)
                .HasForeignKey(d => d.application_id1);
            this.HasOptional(t => t.lu_applicationitempaymenttype)
                .WithMany(t => t.applicationitems)
                .HasForeignKey(d => d.paymenttype_id);
            this.HasOptional(t => t.lu_applicationitemtransfertype)
                .WithMany(t => t.applicationitems)
                .HasForeignKey(d => d.transfertype_id);
            this.HasOptional(t => t.profile)
                .WithMany(t => t.applicationitems)
                .HasForeignKey(d => d.profile_id);
            this.HasOptional(t => t.profile1)
                .WithMany(t => t.applicationitems1)
                .HasForeignKey(d => d.profile_id1);
            this.HasOptional(t => t.profile2)
                .WithMany(t => t.applicationitems2)
                .HasForeignKey(d => d.purchaserprofile_id1);
            this.HasOptional(t => t.profile3)
                .WithMany(t => t.applicationitems3)
                .HasForeignKey(d => d.transferprofile_id1);

        }
    }
}
