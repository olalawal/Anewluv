using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Anewluv.Domain.Data.Mapping
{
    public class profilemetadataMap : EntityTypeConfiguration<profilemetadata>
    {
        public profilemetadataMap()
        {
            // Primary Key
            this.HasKey(t => t.profile_id);

            // Properties
            this.Property(t => t.profile_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("profilemetadatas");
            this.Property(t => t.profile_id).HasColumnName("profile_id");



            // Relationships
            this.HasRequired(t => t.profile)
                .WithOptional(p=>p.profilemetadata);

            this.HasMany(t => t.mailboxfolders).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);

            this.HasMany(t => t.sentmailboxmessages).WithRequired(z => z.senderprofilemetadata).HasForeignKey(z => z.sender_id).WillCascadeOnDelete(false); ;
            this.HasMany(t => t.receivedmailboxmessages).WithRequired(z => z.recipientprofilemetadata).HasForeignKey(z => z.recipient_id).WillCascadeOnDelete(false); ;

              this.HasMany(t => t.mailupdatefreqencies).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);

              this.HasMany(t => t.photoalbums).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);
              this.HasMany(t => t.photos).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);

              this.HasMany(t => t.photoreviews).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.reviewerprofile_id).WillCascadeOnDelete(false);
              this.HasMany(t => t.profiledata_ethnicity).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);
              this.HasMany(t => t.profiledata_hobby).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);
              this.HasMany(t => t.profiledata_hotfeature).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);
              this.HasMany(t => t.profiledata_lookingfor).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);

              this.HasMany(t => t.raterratingvalues).WithRequired(z => z.raterprofilemetadata).HasForeignKey(z => z.raterprofile_id).WillCascadeOnDelete(false); ;
              this.HasMany(t => t.rateeratingvalues).WithRequired(z => z.rateeprofilemetadata).HasForeignKey(z => z.rateeprofile_id).WillCascadeOnDelete(false); ;

              this.HasMany(t => t.searchsettings).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id);

              this.HasMany(t => t.createdactions).WithRequired(z => z.creatorprofilemetadata).HasForeignKey(z => z.creator_profile_id).WillCascadeOnDelete(false); ;
              this.HasMany(t => t.targetofactions).WithRequired(z => z.targetprofilemetadata).HasForeignKey(z => z.target_profile_id).WillCascadeOnDelete(false); ;

              this.HasMany(t => t.applications).WithRequired(z => z.profilemetadata).HasForeignKey(z => z.profile_id).WillCascadeOnDelete(false); ;





        }
    }
}
