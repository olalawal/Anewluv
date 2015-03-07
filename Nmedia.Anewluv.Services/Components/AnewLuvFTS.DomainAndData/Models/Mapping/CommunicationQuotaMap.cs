using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CommunicationQuotaMap : EntityTypeConfiguration<CommunicationQuota>
    {
        public CommunicationQuotaMap()
        {
            // Primary Key
            this.HasKey(t => t.QuotaID);

            // Properties
            this.Property(t => t.QuotaName)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CommunicationQuotas");
            this.Property(t => t.QuotaID).HasColumnName("QuotaID");
            this.Property(t => t.QuotaName).HasColumnName("QuotaName");
            this.Property(t => t.QuotaDescription).HasColumnName("QuotaDescription");
            this.Property(t => t.QuotaValue).HasColumnName("QuotaValue");
            this.Property(t => t.QuotaRoleID).HasColumnName("QuotaRoleID");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
        }
    }
}
