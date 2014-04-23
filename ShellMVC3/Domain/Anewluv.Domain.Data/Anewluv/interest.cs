using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class interest
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int profile_id { get; set; }
        [DataMember]  public int interestprofile_id { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> viewdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> modificationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> deletedbymemberdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> deletedbyinterestdate { get; set; }
        [DataMember]  public Nullable<bool> mutual { get; set; }
      [DataMember]    public virtual profilemetadata profilemetadata { get; set; }
      [DataMember]    public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
