using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class note
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int notetype_id { get; set; }
        [DataMember]  public int action_id { get; set; }  //optional action i think
        [DataMember]
        public int? abusetype_id { get; set; }  //optional mapping for abuse type
        //[DataMember]  public int? creatorprofile_id { get; set; }
        [DataMember]  public string notedetail { get; set; }
        [DataMember]  public Nullable<System.DateTime> creationdate { get; set; }
        [DataMember]  public Nullable<System.DateTime> reviewdate { get; set; }
      
       
       [DataMember]
       public virtual lu_notetype lu_notetype { get; set; }
       [DataMember]
       public virtual action action { get; set; }
       [DataMember]
       public virtual lu_abusetype lu_abusetype { get; set; }
       //[DataMember]
       //public virtual profilemetadata creatorprofilemetadata { get; set; }
    }
}
