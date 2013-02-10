using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv.Context
{
    public partial class anewluvmodelbuilder
    {

        //2-09-2012 olawal created today for applicaiton modesl
        public static void buildapplicationmodels(DbModelBuilder modelBuilder)
        {
            //icomns and photo format first

            //confgure 1 to 1 required relatonship woth p lu_iconformat
            //********************************************
            modelBuilder.Entity<lu_iconformat>()
           .HasRequired(p => p.imageresizerformat).WithMany().HasForeignKey(z => z.imagersizerformat_id );

            //application item
            //********************************************

            modelBuilder.Entity<application>().HasMany(t => t.icons)
           .WithRequired(p => p.application ).HasForeignKey(p => p.application_id ).WillCascadeOnDelete(false);


            //application ietm another 1 to many
            modelBuilder.Entity<application>().HasMany(t => t.items)
            .WithRequired(p => p.application).HasForeignKey(p => p.application_id).WillCascadeOnDelete(false);

            //applicationiconconversion item
            //********************************************
            modelBuilder.Entity<lu_iconformat>()
          .HasRequired(p => p.imageresizerformat).WithMany().HasForeignKey(z => z.imagersizerformat_id);


            //application item
            //********************************************
            //map profile purchased and transfered items
            modelBuilder.Entity<profile>().HasMany(t => t.purchasedapplicationitems)
              .WithRequired(p => p.purchaserprofile  ).HasForeignKey(p => p.purchaserprofile_id ).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(t => t.transferedapplicationitems)
            .WithRequired(p => p.transferprofile ).HasForeignKey(p => p.transferprofile_id ).WillCascadeOnDelete(false);

            //relation shit for roles and applicationRoles

            //Members in role required lookup
            //11-17-2012 olawal added requured lookup
            modelBuilder.Entity<applicationrole>()
            .HasRequired(t => t.role);

            //Members in role required lookup
            //11-17-2012 olawal added requured lookup
            modelBuilder.Entity<applicationrole>()
            .HasRequired(t => t.application);


            


        }
    }
}
