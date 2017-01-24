using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Nmedia.Infrastructure.Domain.Data.Notification;

namespace Nmedia.Infrastructure.Domain
{

    public partial class NotificationContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {



            //systemddress
            //  modelBuilder.Entity<systemaddress>().HasRequired(a => a.systemaddresstype)
            // .WithMany().HasForeignKey(a => a.systemaddresstype_id  );

            //set up required fiel lookups
            modelBuilder.Entity<systemaddress>().HasRequired(t => t.systemaddresstype).WithMany().HasForeignKey(p => p.systemaddresstype_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<address>().HasRequired(t => t.addresstype).WithMany().HasForeignKey(p => p.addresstype_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<address>().HasOptional(t => t.application).WithMany().HasForeignKey(p => p.application_id).WillCascadeOnDelete(false);

    

            modelBuilder.Entity<message>().HasRequired(t => t.messagetype).WithMany().HasForeignKey(p => p.messagetype_id).WillCascadeOnDelete(false); ;
            modelBuilder.Entity<message>().HasOptional(t => t.template).WithMany().HasForeignKey(p => p.template_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<message>().HasRequired(t => t.systemaddress).WithMany().HasForeignKey(p => p.systemaddress_id).WillCascadeOnDelete(false); ;

            modelBuilder.Entity<messagedetail>().HasRequired(t => t.messagetype).WithMany().HasForeignKey(p => p.messagetype_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<messagedetail>().HasRequired(t => t.templatebody).WithMany().HasForeignKey(p => p.templatebody_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<messagedetail>().HasRequired(t => t.templatesubject).WithMany().HasForeignKey(p => p.templatesubject_id).WillCascadeOnDelete(false);

            #region  "template mappings"
            modelBuilder.Entity<lu_template>().HasRequired(t => t.body).WithMany().HasForeignKey(p => p.body_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<lu_template>().HasRequired(t => t.subject).WithMany().HasForeignKey(p => p.subject_id).WillCascadeOnDelete(false);
            modelBuilder.Entity<lu_template>().HasRequired(t => t.filename).WithMany().HasForeignKey(p => p.filename_id).WillCascadeOnDelete(false);

            //add required application feilds
         //   modelBuilder.Entity<lu_template>().HasOptional(t => t.application).WithMany().HasForeignKey(p => p.application_id).WillCascadeOnDelete(false);
         //   modelBuilder.Entity<lu_templatebody>().HasOptional(t => t.application).WithMany().HasForeignKey(p => p.application_id).WillCascadeOnDelete(false);
           // modelBuilder.Entity<lu_templatesubject>().HasOptional(t => t.application).WithMany().HasForeignKey(p => p.application_id).WillCascadeOnDelete(false);


            #endregion

            //many to many for address and recipeints
            modelBuilder.Entity<address>()
                 .HasMany(p => p.messages)
                 .WithMany(t => t.recipients)
                 .Map(mc =>
                 {
                     mc.ToTable("notification_addressmessages");
                     mc.MapLeftKey("recipient_id");
                     mc.MapRightKey("message_id");

                 });
            

            //modelBuilder.Entity<address>().ToTable("notification_address");
            //modelBuilder.Entity<systemaddress>().ToTable("notification_systemaddress");
            //modelBuilder.Entity<lu_addresstype>().ToTable("notification_lu_addresstype");
            //modelBuilder.Entity<lu_messagetype>().ToTable("notification_lu_messagetype");
            //modelBuilder.Entity<lu_systemaddresstype>().ToTable("notification_lu_systemaddresstype");
            //modelBuilder.Entity<lu_template>().ToTable("notification_lu_template");
            //modelBuilder.Entity<lu_templatebody>().ToTable("notification_lu_templatebody");
            //modelBuilder.Entity<lu_templatesubject>().ToTable("notification_lu_templatesubject");
            //modelBuilder.Entity<lu_news>().ToTable("notification_lu_news");
            //modelBuilder.Entity<message>().ToTable("notification_message");
            
            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // map one to many relation shipds for metadatata table from its side 
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            //systemddress
          //  modelBuilder.Entity<systemaddress>().HasRequired(a => a.systemaddresstype)
           // .WithMany().HasForeignKey(a => a.systemaddresstype_id  );

            //set up required fiel lookups
   //         modelBuilder.Entity<systemaddress>()
   //       .HasRequired(t => t.systemaddresstype);
          

   //         modelBuilder.Entity<address>()
   //        .HasRequired(t => t.addresstype);
            

   //         modelBuilder.Entity<message>()
   //        .HasRequired(t => t.messagetype );


   //         modelBuilder.Entity<message>()
   //        .HasRequired(t => t.messagetype);


   //         modelBuilder.Entity<message>()
   //     .HasRequired(t => t.systemaddress);


   //         modelBuilder.Entity<message>()
   //.HasRequired(t => t.template);



   //        //many to many for address and recipeints
   //        modelBuilder.Entity<address>()
   //             .HasMany(p => p.messages  )
   //             .WithMany(t => t.recipients )
   //             .Map(mc =>
   //                {
   //                    mc.ToTable("addressmessages");
   //                    mc.MapLeftKey("recipient_id");
   //                    mc.MapRightKey("message_id");

   //                });
            
            //other stuff for address
           //adddress
        //   modelBuilder.Entity<address>().HasRequired(a => a.addresstype)
        //   .WithMany().HasForeignKey(a => a.addresstype_id);

        //    //other stuff for messages
        //   //mesages
        //   modelBuilder.Entity<message>().HasRequired(a => a.messagetype )
        //   .WithMany().HasForeignKey(a => a.messagetype_id );

        //   modelBuilder.Entity<message>().HasRequired(a => a.template )
        // .WithMany().HasForeignKey(a => a.template_id );
            
        //   modelBuilder.Entity<message>().HasRequired(a => a.systemaddress )
        //.WithMany().HasForeignKey(a => a.systemaddress_id );

            
        
        }

        
    }

  
}