using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class blocknote
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int block_id { get; set; }
        [DataMember]  public int profile_id { get; set; }
        [DataMember]  public string note { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> reviewdate { get; set; }
       [DataMember]   public Nullable<int> notetype_id { get; set; }
       [DataMember]
       public virtual block block { get; set; }
       [DataMember]
       public virtual lu_notetype lu_notetype { get; set; }
       [DataMember]
       public virtual profilemetadata profilemetadata { get; set; }
    }
}
