using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnewLuvFTS.DomainAndData.Models.Mapping
{
    public class CriteriaLife_EducationLevelMap : EntityTypeConfiguration<CriteriaLife_EducationLevel>
    {
        public CriteriaLife_EducationLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.EducationLevelID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CriteriaLife_EducationLevel");
            this.Property(t => t.EducationLevelID).HasColumnName("EducationLevelID");
            this.Property(t => t.EducationLevelName).HasColumnName("EducationLevelName");
        }
    }
}
