using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class searchsettingdetailMap : EntityTypeConfiguration<searchsettingdetail>
    {

        public searchsettingdetailMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("searchsettingdetails");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.searchsetting_id).HasColumnName("searchsetting_id");
            this.Property(t => t.searchsettingdetailtype_id).HasColumnName("searchsettingdetailtype_id");
            this.Property(t => t.value).HasColumnName("value");



            this.Property(t => t.creationdate).HasColumnName("creationdate");     
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
  

            // Relationships
            this.HasRequired(t => t.lu_searchsettingdetailtype)
            .WithMany(t => t.details)
            .HasForeignKey(d => d.searchsettingdetailtype_id);

            this.HasRequired(t => t.searchsetting)
            .WithMany(t => t.details)
            .HasForeignKey(d => d.searchsetting_id);




      


        }  
    }
}
