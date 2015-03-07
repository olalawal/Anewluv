using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class SecurityQuestionMap : EntityTypeConfiguration<SecurityQuestion>
    {
        public SecurityQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.SecurityQuestionID);

            // Properties
            this.Property(t => t.SecurityQuestionText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SecurityQuestion");
            this.Property(t => t.SecurityQuestionID).HasColumnName("SecurityQuestionID");
            this.Property(t => t.SecurityQuestionText).HasColumnName("SecurityQuestionText");
        }
    }
}
