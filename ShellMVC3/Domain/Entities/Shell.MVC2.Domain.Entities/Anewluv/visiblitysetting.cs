using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    [Serializable ]
    public class visiblitysetting
    {
        [Key]
        [DataMember]
        public int id { get; set; }
          [DataMember]
        public int profile_id { get; set; }
        public virtual profiledata profiledata { get; set; }
          [DataMember]
        public int? agemaxvisibility { get; set; }
          [DataMember]
        public int? ageminvisibility { get; set; }
          [DataMember]        
        public bool? chatvisiblitytointerests { get; set; }
          [DataMember]
        public bool? chatvisiblitytolikes { get; set; }
          [DataMember]
        public bool? chatvisiblitytomatches { get; set; }
          [DataMember]
        public bool? chatvisiblitytopeeks { get; set; }
          [DataMember]
        public bool? chatvisiblitytosearch { get; set; }
          [DataMember]
        public virtual  ICollection<visiblitysettings_country> countries { get; set; }
        [DataMember ]
        public virtual  ICollection<visiblitysettings_gender>  genders{ get; set; }
          [DataMember]
        public DateTime? lastupdatedate { get; set; }
          [DataMember]
        public bool? mailchatrequest { get; set; }
          [DataMember]
        public bool? mailintrests { get; set; }
          [DataMember]
        public bool? maillikes { get; set; }
          [DataMember]
        public bool? mailmatches { get; set; }
          [DataMember]
        public bool? mailnews { get; set; }
          [DataMember]
        public bool? mailpeeks { get; set; }
          [DataMember]
        public bool? profilevisiblity { get; set; }
          [DataMember]
        public bool? saveofflinechat { get; set; }
          [DataMember]
        public bool? steathpeeks { get; set; }

    }
}
