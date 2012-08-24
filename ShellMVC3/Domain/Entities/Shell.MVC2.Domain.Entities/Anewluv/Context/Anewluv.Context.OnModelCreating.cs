using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial class AnewluvContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // Specify one-to-one association between profile and profiledata
            //*****************************************************************
            modelBuilder.Entity<profile>()
           .HasRequired(t => t.profiledata).WithRequiredPrincipal(t => t.profile );
            // Specify one-to-one association between profiledata  and visibilitysetting
            modelBuilder.Entity<profiledata>()
           .HasOptional(t => t.visibilitysettings).WithOptionalPrincipal(t => t.profiledata );
            
            // map required relationships abusereport
            //**************************************
            modelBuilder.Entity<abusereport>().HasRequired(p => p.abuser).WithMany()
           .HasForeignKey(p => p.abuser_id).WillCascadeOnDelete(false);

            //reporter required
            modelBuilder.Entity<abusereport>().HasRequired(p => p.abusereporter).WithMany()
            .HasForeignKey(p => p.abusereporter_id).WillCascadeOnDelete(false);

            // map required relationships abusereportnotes
            //**************************************
            //map abusereport notes to abuse report and set as required
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.abusereport).WithMany(z => z.notes)
            .HasForeignKey(p => p.abusereport_id).WillCascadeOnDelete(false);

            //abuse report notes also requires a person creating it i.e profiledata
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.profiledata )
            .WithMany()
            .HasForeignKey(p => p.profile_id ).WillCascadeOnDelete(false);


            //map requierd  relationshipds for block
            //**************************************
            //blocked  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<block>().HasRequired(t => t.blockedprofiledata).WithMany(z=>z.blocks)
            .HasForeignKey(p => p.blockprofile_id ).WillCascadeOnDelete(false);

            //blocker required
            modelBuilder.Entity<block>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for CommunicationQoutas
            //  //**************************************
            modelBuilder.Entity<communicationquota>()
           .HasRequired(t => t.updaterprofiledata);

            //  //map requierd  relationshipds for favorite
            //  //**************************************
            //blocked  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<favorite>().HasRequired(t => t.favoriteprofiledata).WithMany(z => z.favorites)
           .HasForeignKey(p => p.favoriteprofile_id).WillCascadeOnDelete(false);

            //blocker required
            modelBuilder.Entity<favorite>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);





            //  //map requierd  relationshipds for friend
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<friend>()
            // .HasRequired(t => t.friendprofiledata)
            // .WithRequiredPrincipal();
            //  //member sendings friend profiledata required
            //  modelBuilder.Entity<friend>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();


            //  //map hobby required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<hobby>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map hotfeature required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<hotfeature>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map requierd  relationshipds for hotlist
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<hotlist>()
            // .HasRequired(t => t.hotlistprofiledata )
            // .WithRequiredPrincipal();
            //  //member sendings hotlist profiledata required
            //  modelBuilder.Entity<hotlist>()
            // .HasRequired(t => t.profile )
            // .WithRequiredPrincipal();

            //  //map requierd  relationshipds for Like
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<like>()
            // .HasRequired(t => t.likeprofiledata )
            // .WithRequiredPrincipal();
            //  //member sendings like profiledata required
            //  modelBuilder.Entity<like>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map loginGeodata required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<loginGeodata>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map lookingfor required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<lookingfor>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map lookingfor required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<lookingfor>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map mailupdatefreqency required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<mailupdatefreqency >()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map membersinrole required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<membersinrole>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();
            //  //role required
            //  modelBuilder.Entity<membersinrole>()
            // .HasRequired(t => t.role )
            // .WithRequiredPrincipal();

            //  //map openid required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<openid>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map peek required realtionships
            //  //peek profiledata  reqired
            //  modelBuilder.Entity<peek>()
            // .HasRequired(t => t.peekprofiledata )
            // .WithRequiredPrincipal();
            //  //peeker profiledata required
            //  modelBuilder.Entity<peek>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //fluent config for photo table
            //  //*************************************
            //  // Specifify the generation of guid fields 
            //  modelBuilder.Entity<photo>().Property(o => o.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //  //set required feild
            //  modelBuilder.Entity<photo>()
            //  .HasRequired(t => t.profile)
            //  .WithRequiredPrincipal()
            //  ;

            //  //map photoconversions required realtionships
            //  //photoconversions profiledata  reqired
            //  modelBuilder.Entity<photoconversion>()
            // .HasRequired(t => t.photo)
            // .WithRequiredPrincipal();
            //  //peeker profiledata required
            //  modelBuilder.Entity<photoconversion>()
            // .HasRequired(t => t.type)
            // .WithRequiredPrincipal();


            //  //fluent config for photoreviewstatus table
            //  //*************************************
            //  modelBuilder.Entity<photoreviewstatus>()
            //  .HasRequired(t => t.reviewerprofiledata)
            //   .WithRequiredPrincipal();

            //  //profile and profiel data are at top

            //  //fluent confg for ratingvalues
            //  //map ratingvalues required realtionships

            // //rating tie back required 
            //  modelBuilder.Entity<ratingvalue>()
            // .HasRequired(t => t.rating )
            // .WithRequiredPrincipal();

            //  //rater profiledata  reqired
            //  modelBuilder.Entity<ratingvalue >()
            // .HasRequired(t => t.raterprofiledata)
            // .WithRequiredPrincipal();
            //  //member being rated profiledata required
            //  modelBuilder.Entity<ratingvalue>()
            // .HasRequired(t => t.rateeprofiledata )
            // .WithRequiredPrincipal();

            //  //user logtime fluent mapping
            //  //profile  reqired
            //  modelBuilder.Entity<userlogtime>()
            // .HasRequired(t => t.profile )
            // .WithRequiredPrincipal();

            //  //visibilitysetting fluent mapping
            //  //profiledata  reqired
            //  //modelBuilder.Entity<visiblitysetting>()
            // //.HasRequired(t => t.profile)
            //// .WithRequiredPrincipal();

        }

    }
}
