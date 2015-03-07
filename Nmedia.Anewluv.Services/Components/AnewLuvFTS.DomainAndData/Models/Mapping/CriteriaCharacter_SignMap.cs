using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaCharacter_SignMap : EntityTypeConfiguration<CriteriaCharacter_Sign>
    {
        public CriteriaCharacter_SignMap()
        {
            // Primary Key
            this.HasKey(t => t.SignID);

            // Properties
            this.Property(t => t.SignName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SignBirthMonth)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CriteriaCharacter_Sign");
            this.Property(t => t.SignID).HasColumnName("SignID");
            this.Property(t => t.SignName).HasColumnName("SignName");
            this.Property(t => t.SignBirthMonth).HasColumnName("SignBirthMonth");
        }
    }
}
