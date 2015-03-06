using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
     public partial class block :Entity
    {
        public block()
        {
            this.blocknotes = new List<blocknote>();
        }

        [DataMember]  public int id { get; set; }
        [DataMember]  public int profile_id { get; set; }
        [DataMember]  public int blockprofile_id { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> modificationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> removedate { get; set; }
       [DataMember]   public Nullable<int> mutual { get; set; }
       [DataMember]
       public virtual ICollection<blocknote> blocknotes { get; set; }
       [DataMember]
       public virtual profilemetadata profilemetadatablocker { get; set; }
       [DataMember]
       public virtual profilemetadata profilemetadatablocked { get; set; }
    }
}
