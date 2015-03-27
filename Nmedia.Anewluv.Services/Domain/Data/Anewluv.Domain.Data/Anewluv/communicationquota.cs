using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class communicationquota :Entity
    {
        [DataMember]  public int id { get; set; }
        [DataMember]
        public Nullable<bool> active { get; set; }
        [DataMember]  public string quotadescription { get; set; }
        [DataMember]  public string quotaname { get; set; }
       [DataMember]   public Nullable<int> quotaroleid { get; set; }
       [DataMember]   public Nullable<int> quotavalue { get; set; }
        [DataMember]  public int updaterprofile_id { get; set; }
       [DataMember]   public Nullable<System.DateTime> updatedate { get; set; }
    
        [DataMember]
        public virtual profiledata profiledata { get; set; }
    }
}
