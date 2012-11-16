using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{

    public  class notificationmodelbuilder
    {

        public static void buildgeneralmodels(DbModelBuilder modelBuilder)
        {

            
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
            modelBuilder.Entity<systemaddress>()
          .HasRequired(t => t.systemaddresstype);
          

            modelBuilder.Entity<address>()
           .HasRequired(t => t.addresstype);
            

            modelBuilder.Entity<message>()
           .HasRequired(t => t.messagetype );


            modelBuilder.Entity<message>()
           .HasRequired(t => t.messagetype);


            modelBuilder.Entity<message>()
        .HasRequired(t => t.systemaddress);


            modelBuilder.Entity<message>()
   .HasRequired(t => t.template);



           //many to many for address and recipeints
           modelBuilder.Entity<address>()
                .HasMany(p => p.messages  )
                .WithMany(t => t.recipients )
                .Map(mc =>
                   {
                       mc.ToTable("addressmessages");
                       mc.MapLeftKey("recipient_id");
                       mc.MapRightKey("message_id");

                   });
            
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