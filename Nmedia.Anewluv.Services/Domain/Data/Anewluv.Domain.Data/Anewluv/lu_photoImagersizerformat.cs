using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    
    [DataContract] public partial class lu_photoImagersizerformat
    {
        public lu_photoImagersizerformat()
        {
            this.lu_photoformat = new List<lu_photoformat>();
        }

      
        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
       [IgnoreDataMember]    public virtual ICollection<lu_photoformat> lu_photoformat { get; set; }
    }
}
