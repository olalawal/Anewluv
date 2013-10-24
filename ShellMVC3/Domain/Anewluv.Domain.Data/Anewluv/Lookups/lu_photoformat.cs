using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    //Gallery, Detail,larger, Full <- might do away with full to save memory or 
    //at least compress and rescan it
[DataContract]
    public class lu_photoformat
    {

       // public virtual ICollection<photoconversions> converted { get; set; }
        [Key]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        public int photoImagersizerformat_id { get; set; }      
        public virtual lu_photoImagersizerformat imageresizerformat { get; set; }
        //[NotMapped]
        public bool selected { get; set; }
    }
}
