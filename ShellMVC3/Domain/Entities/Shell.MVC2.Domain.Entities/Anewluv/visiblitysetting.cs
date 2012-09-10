using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class visiblitysetting
    {
        [Key]
        public string id { get; set; }

        public int profile_id { get; set; }
        public virtual profiledata profiledata { get; set; }
        public int? agemaxvisibility { get; set; }
        public int? ageminvisibility { get; set; }
        public bool? chatvisiblitytointerests { get; set; }
        public bool? chatvisiblitytolikes { get; set; }
        public bool? chatvisiblitytomatches { get; set; }
        public bool? chatvisiblitytopeeks { get; set; }
        public bool? chatvisiblitytosearch { get; set; }
        public  ICollection<string> countries { get; set; }
        public ICollection<lu_gender>  genders{ get; set; }     
        public DateTime? lastupdatedate { get; set; }
        public bool? mailchatrequest { get; set; }
        public bool? mailintrests { get; set; }
        public bool? maillikes { get; set; }
        public bool? mailmatches { get; set; }
        public bool? mailnews { get; set; }
        public bool? mailpeeks { get; set; }
        public bool? profilevisiblity { get; set; }
        public bool? saveofflinechat { get; set; }
        public bool? steathpeeks { get; set; }

    }
}
