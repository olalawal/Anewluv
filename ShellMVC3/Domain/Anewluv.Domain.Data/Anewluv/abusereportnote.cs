using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class abusereportnote
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int abusereport_id { get; set; }
        [DataMember]  public int profile_id { get; set; }
        [DataMember]  public string note { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> reviewdate { get; set; }
        [DataMember]  public int notetype_id { get; set; }
        [DataMember]
        public virtual abusereport abusereport { get; set; }
        [DataMember]
        public virtual lu_notetype lu_notetype { get; set; }
      [DataMember]    public virtual profilemetadata profilemetadata { get; set; }
    }
}
