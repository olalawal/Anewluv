using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract(IsReference = true)]
    public class ratingvalue
    {
        [Key]
        public int id { get; set; }
        public int rating_id { get; set; }
        public int profile_id { get; set; }
        public int rateeprofile_id { get; set; }
        public virtual rating  rating { get; set; }  //type of rating
        public virtual profilemetadata profilemetatadata { get; set; }    //person giving the rating
        public virtual profilemetadata  rateeprofilemetadata { get; set; }   //person being rated  
        public DateTime? date { get; set; }     
        public Double ? value { get; set; }
    }
}
     
           //   //map requierd  relationshipds for favorite
           //   //**************************************
           // favorite  reqired ,  first part has to sleetc the nav property in perant
           // modelBuilder.Entity<ratingvalue>().HasRequired(t => t.rateeprofiledata ).WithMany(z => z.ratingvalues )
           //.HasForeignKey(p => p.rateeprofile_id ).WillCascadeOnDelete(false);