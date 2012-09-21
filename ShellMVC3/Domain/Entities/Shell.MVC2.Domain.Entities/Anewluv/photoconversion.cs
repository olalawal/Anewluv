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
        public int id { get; set; }
        public Guid photo_id { get; set; }
        public virtual photo photo { get; set; }   
        public virtual lu_photoformat  formattype { get; set; }
        public DateTime? creationdate { get; set; }
        public string description { get; set; } 
        //actual image data
        public byte[] image { get; set; }
        public int size { get; set; }  

        
    }
}
