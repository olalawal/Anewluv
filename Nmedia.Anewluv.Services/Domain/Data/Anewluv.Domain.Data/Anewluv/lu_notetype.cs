using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_notetype
    {
        public lu_notetype()
        {
            this.abusereportnotes = new List<abusereportnote>();
            this.blocknotes = new List<blocknote>();
        }


        [DataMember]
        public int id { get; set; }
        [DataMember]  public string description { get; set; }
        [DataMember]  public virtual ICollection<abusereportnote> abusereportnotes { get; set; }
        [DataMember]  public virtual ICollection<blocknote> blocknotes { get; set; }
    }
}
