using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class systempagesetting
    {
        [DataMember]   public int id { get; set; }
        [DataMember]
        public string bodycssstylename { get; set; }
        [DataMember]   public string description { get; set; }
        public Nullable<int> hitCount { get; set; }
        [DataMember]
        public Nullable<bool> ismasterpage { get; set; }
        [DataMember]
        public string path { get; set; }
        [DataMember]
        public string title { get; set; }
    }
}
