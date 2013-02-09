using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    /// <summary>
    /// similar to photo conversion for icons and applications
    /// </summary>
    [DataContract ]
    public class applicationiconconversions
    {

        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public Guid application_id { get; set; }
        [DataMember]
        public virtual  application application { get; set; }
        [DataMember]
        public lu_iconformat  imagetype { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        //actual image data
        [DataMember]
        public byte[] image { get; set; }
        [DataMember]
        public long size { get; set; }  
    }
}
