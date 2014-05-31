using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_profilefiltertype
    {
        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
    }
}
