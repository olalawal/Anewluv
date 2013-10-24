using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract(IsReference = true)]
    public class ratingvalue
    {
        [Key]
       [DataMember] public int id { get; set; }
        [DataMember]
        public int rating_id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public int rateeprofile_id { get; set; }
        [DataMember]
        public virtual rating rating { get; set; }  //type of rating
        [DataMember]
        public virtual profilemetadata profilemetatadata { get; set; }    //person giving the rating
        [DataMember]
        public virtual profilemetadata rateeprofilemetadata { get; set; }   //person being rated  
        [DataMember]
        public DateTime? date { get; set; }
        [DataMember]
        public Double? value { get; set; }
    }
}
     
           //   //map requierd  relationshipds for favorite
           //   //**************************************
           // favorite  reqired ,  first part has to sleetc the nav property in perant
           // modelBuilder.Entity<ratingvalue>().HasRequired(t => t.rateeprofiledata ).WithMany(z => z.ratingvalues )
           //.HasForeignKey(p => p.rateeprofile_id ).WillCascadeOnDelete(false);