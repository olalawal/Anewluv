using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //make sure this code handles the adding of multiple image types i,e
  

    public class photoconversion
    {

        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public Guid photo_id { get; set; }
        [DataMember]
        public virtual photo photo { get; set; }
        [DataMember]
        public virtual lu_photoformat formattype { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public string description { get; set; } 
        //actual image data
        [DataMember]
        public byte[] image { get; set; }
        [DataMember]
        public long size { get; set; }  

        
    }
}
