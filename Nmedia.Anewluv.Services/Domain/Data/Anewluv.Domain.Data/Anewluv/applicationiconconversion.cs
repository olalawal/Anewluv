using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class applicationiconconversion
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int application_id { get; set; }
        [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
        public byte[] image { get; set; }
        [DataMember]
        public long size { get; set; }    
       [DataMember]   public Nullable<int> iconformat_id { get; set; }
       [DataMember]
       public virtual application application { get; set; }
       [DataMember]
       public virtual lu_iconformat lu_iconformat { get; set; }
    }
}
