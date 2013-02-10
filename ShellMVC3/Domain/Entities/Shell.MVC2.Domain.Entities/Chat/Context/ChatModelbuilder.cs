using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{

    public  class chatmodelbuilder
    {

        public static void buildgeneralmodels(DbModelBuilder modelBuilder)
        {

         //chat client user first
            ////////////////////////
            modelBuilder.Entity<ChatClient>().HasRequired(x => x.User )
                .WithMany()
                .HasForeignKey(z => z.User_id)
                .WillCascadeOnDelete(false);
      
            //chat message next
            ////////////////////////
            modelBuilder.Entity<ChatMessage>().HasRequired(x => x.User)
              .WithMany()
              .HasForeignKey(z => z.User_id)
              .WillCascadeOnDelete(false);

              modelBuilder.Entity<ChatMessage>().HasRequired(x => x.Room  )
              .WithMany()
              .HasForeignKey(z => z.Room_id )
              .WillCascadeOnDelete(false);

            //Chatuser
            ////////////////////////
            //do the many to one relationships first
            modelBuilder.Entity<ChatUser>().HasMany(t => t.ConnectedClients)
           .WithRequired(p => p.User).HasForeignKey(p => p.User_id ).WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatUser>().HasMany(t => t.SentMessages)
           .WithRequired(p => p.User).HasForeignKey(p => p.User_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatUser>().HasMany(t => t.RecivedMessages)
           .WithRequired(p => p.User).HasForeignKey(p => p.User_id).WillCascadeOnDelete(false);

            //chat user many to many
            //room owned to users
            modelBuilder.Entity<ChatUser>().HasMany(i => i.OwnedRooms )
            .WithMany(c => c.Owners)
            .Map(mc =>
            {
                mc.MapLeftKey("Room_Id");
                mc.MapRightKey("User_Id");
                mc.ToTable("ChatUserOwnedRoom");
            });

            //users to rooms
            modelBuilder.Entity<ChatUser>().HasMany(i => i.Rooms)
            .WithMany(c => c.Users)
            .Map(mc =>
            {
                mc.MapLeftKey("Room_Id");
                mc.MapRightKey("User_Id");
                mc.ToTable("ChatUserRoom");
            });

            //allowed rooms i.e list of users allowed to acces room

            modelBuilder.Entity<ChatUser>().HasMany(i => i.AllowedRooms)
          .WithMany(c => c.AllowedUsers)
          .Map(mc =>
          {
              mc.MapLeftKey("Room_Id");
              mc.MapRightKey("User_Id");
              mc.ToTable("ChatUserAllowedRoom");
          });

           //Rooms now
            ////////////////////////

            modelBuilder.Entity<ChatRoom >().HasMany(t => t.Messages )
           .WithRequired(p => p.Room ).HasForeignKey(p => p.Room_id ).WillCascadeOnDelete(false);


           // modelBuilder.Entity<profilemetadata>()
             //         .HasRequired(e => e.profiledata )
              //       .WithRequiredDependent(r => r.profilemetadata);


           

    

        
        }

       
    }

  
}