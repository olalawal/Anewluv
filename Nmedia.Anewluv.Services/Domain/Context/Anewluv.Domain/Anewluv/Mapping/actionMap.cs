using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class actionMap : EntityTypeConfiguration<action>
    {
        public actionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("actions");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.creator_profile_id).HasColumnName("creator_profile_id");
            this.Property(t => t.target_profile_id).HasColumnName("target_profile_id");
            this.Property(t => t.actiontype_id).HasColumnName("actiontype_id");
            this.Property(t => t.creationdate).HasColumnName("creationdate");
            this.Property(t => t.viewdate).HasColumnName("viewdate");
            this.Property(t => t.modificationdate).HasColumnName("modificationdate");
            this.Property(t => t.deletedbycreatordate).HasColumnName("deletedbycreatordate");
            this.Property(t => t.deletedbytargetdate).HasColumnName("deletedbytargetdate");
            this.Property(t => t.active).HasColumnName("active");
           

            // Relationships
            this.HasRequired(t => t.lu_actiontype)
            .WithMany(t => t.actions)
            .HasForeignKey(d => d.actiontype_id);
            this.HasRequired(t => t.targetprofilemetadata)
            .WithMany(t => t.targetofactions)
            .HasForeignKey(d => d.target_profile_id).WillCascadeOnDelete(false);
            this.HasRequired(t => t.creatorprofilemetadata)
            .WithMany(t => t.createdactions)
            .HasForeignKey(d => d.creator_profile_id).WillCascadeOnDelete(false); 
            this.HasMany(t => t.notes).WithRequired(z => z.action).HasForeignKey(z=>z.action_id);


      


        }   
    
    }
}
