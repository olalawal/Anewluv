using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //make sure this code handles the adding of multiple image types i,e
    //Photo does not hold the photo data anymore
   [DataContract]
    public class photo
    {

        [Key]
        [DataMember]
        public Guid id { get; set; }
        [DataMember]
        public long size { get; set; }

     //   [DataMember]
        public virtual ICollection<photoalbum> albums { get; set; }
      //  [DataMember]
        public virtual lu_photorejectionreason rejectionreason { get; set; }
      //  [DataMember]
        public virtual lu_photostatus photostatus { get; set; }
     //   [DataMember]
        public virtual lu_photoapprovalstatus approvalstatus { get; set; }
      //  [DataMember]
        public virtual lu_photoimagetype imagetype { get; set; }

       // [DataMember]
        public int profile_id { get; set; }
       // [DataMember]
        [IgnoreDataMember  ]
        public virtual profilemetadata profilemetadata { get; set; }
       
       
        //lazy load these for now and check perforamce
      //  [DataMember]
        public virtual ICollection<photoreview> reviews { get; set; }
      //  [DataMember]
        public virtual ICollection<photoconversion> conversions { get; set; }  
        //private public secuity is done at photo level , overides album secuity
      //  [DataMember]
        public virtual ICollection<photo_securitylevel> photosecuritylevels { get; set; }

        [DataMember]
        public DateTime? creationdate { get; set; } 
        //not sure what this is for need to rember
        //public Nullable<Guid> photoUniqueID { get; set; }            
        //actual image data
         //public byte[] image { get; set; }
         // public int? size { get; set; }
        [DataMember]
        public string imagecaption { get; set; }
        [DataMember]
        public string imagename { get; set; } 
    }
}

