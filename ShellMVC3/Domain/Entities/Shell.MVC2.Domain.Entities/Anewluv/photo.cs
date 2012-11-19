using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //make sure this code handles the adding of multiple image types i,e
    //Photo does not hold the photo data anymore
    public class photo
    {

        [Key]
        public Guid id { get; set; }           
      
        
        public virtual ICollection<photoalbum>  albums { get; set; }
        public virtual lu_photorejectionreason  rejectionreason { get; set; }
        public virtual lu_photostatus  photostatus { get; set; }
        public virtual lu_photoapprovalstatus approvalstatus { get; set; }    
        public virtual  lu_photoimagetype imagetype  { get; set; }
       
        public int profile_id { get; set; }  
        public virtual profilemetadata profilemetadata { get; set; }
       
        //lazy load these for now and check perforamce
        public virtual ICollection<photoreview> reviews { get; set; }
        public virtual ICollection<photoconversion> conversions { get; set; }  
        //private public secuity is done at photo level , overides album secuity
        public virtual ICollection<photo_securitylevel> photosecuritylevels { get; set; } 

        public DateTime creationdate { get; set; } 
        //not sure what this is for need to rember
        //public Nullable<Guid> photoUniqueID { get; set; }            
        //actual image data
         //public byte[] image { get; set; }
         // public int? size { get; set; }
         public string imagecaption { get; set; } 
    }
}

