using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    /// <summary>
    /// similar to photo conversion for icons and applications
    /// </summary>
    [DataContract ]
    public class applicationiconconversion
    {

        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public int application_id { get; set; }
        [DataMember]
        public virtual  application application { get; set; }
        [DataMember]
        public lu_iconformat  iconformat { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        //actual image data
        [DataMember]
        public byte[] image { get; set; }
        [DataMember]
        public long size { get; set; }  
    }
}
