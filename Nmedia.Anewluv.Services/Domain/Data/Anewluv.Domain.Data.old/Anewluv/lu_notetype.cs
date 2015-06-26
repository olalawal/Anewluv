using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_notetype : Repository.Pattern.Ef6.Entity
    {
        public lu_notetype()
        {
            this.notes = new List<note>();

        }


        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public virtual ICollection<note> notes { get; set; }
    }
}
