using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class ratingvalue
    {
        [Key]
        public string id { get; set; }
        public string rating_id { get; set; }
        public string profile_id { get; set; }
        public string rateeprofile_id { get; set; }

        public virtual rating  rating { get; set; }  //type of rating
        public virtual profiledata profiledata { get; set; }    //person giving the rating
        public virtual profiledata rateeprofiledata { get; set; }   //person being rated  
        public DateTime? date { get; set; }     
        public Double ? value { get; set; }
    }
}
