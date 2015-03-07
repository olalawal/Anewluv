using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class abusetypeMap : EntityTypeConfiguration<abusetype>
    {
        public abusetypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AbuseTypeID);

            // Properties
            this.Property(t => t.AbuseTypeName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("abusetype");
            this.Property(t => t.AbuseTypeID).HasColumnName("AbuseTypeID");
            this.Property(t => t.AbuseTypeName).HasColumnName("AbuseTypeName");
        }
    }
}
