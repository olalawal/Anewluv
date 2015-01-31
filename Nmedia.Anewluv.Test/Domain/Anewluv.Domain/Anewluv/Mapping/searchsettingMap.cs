using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsettingMap : EntityTypeConfiguration<searchsetting>
    {
        public searchsettingMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsettings");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.profile_id).HasColumnName("profile_id");
            this.Property(t => t.agemax).HasColumnName("agemax");
            this.Property(t => t.agemin).HasColumnName("agemin");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.distancefromme).HasColumnName("distancefromme");
            this.Property(t => t.heightmax).HasColumnName("heightmax");
            this.Property(t => t.heightmin).HasColumnName("heightmin");
            this.Property(t => t.lastupdatedate).HasColumnName("lastupdatedate");
            this.Property(t => t.myperfectmatch).HasColumnName("myperfectmatch");
            this.Property(t => t.savedsearch).HasColumnName("savedsearch");
            this.Property(t => t.searchname).HasColumnName("searchname");
            this.Property(t => t.searchrank).HasColumnName("searchrank");
            this.Property(t => t.systemmatch).HasColumnName("systemmatch");

            // Relationships
            this.HasRequired(t => t.profilemetadata)
                .WithMany(t => t.searchsettings)
                .HasForeignKey(d => d.profile_id);

        }
    }
}
