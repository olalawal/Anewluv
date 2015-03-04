using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_flagyesno : Entity
    {
        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
    }
}
