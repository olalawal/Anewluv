using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //10-29-2011 moved the  edit photo stuff here but it is not edit only, probably should be in photomodel
    //9-20-2012 olawal created a specific model for just uploading photos since photo model is just a pointer now
   [DataContract ]  
    public class PhotoUploadModel
    {       
          
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public string  caption { get; set; }
        [DataMember]
        public string imagename { get; set; }
        [DataMember]
        public byte?[] imagebytes { get; set; }
        [DataMember]
        public string imageb64string { get; set; } 
        //[DataMember]
        //public int? size { get; set; } 
        [DataMember]
        public int? approvalstatusid { get; set; }
       [DataMember]
       public int? imagetypeid { get; set; }

       [DataMember]
       public int? photostatusid { get; set; }

       [DataMember]
       public int?  rejectionreasonid { get; set; }

       [DataMember]
       public int? legacysize { get; set; }
        
    }  
}
