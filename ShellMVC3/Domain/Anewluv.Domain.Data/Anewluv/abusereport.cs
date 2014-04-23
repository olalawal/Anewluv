using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class abusereport
    {
        public abusereport()
        {
            this.abusereportnotes = new List<abusereportnote>();
        }

        [DataMember]  public int id { get; set; }
        [DataMember]  public int abusereporter_id { get; set; }
        [DataMember]  public int abuser_id { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }
        [DataMember]  public int abusetype_id { get; set; }
        [DataMember]
        public virtual ICollection<abusereportnote> abusereportnotes { get; set; }
        [DataMember]
        public virtual lu_abusetype lu_abusetype { get; set; }
      [DataMember]    public virtual profilemetadata profilemetadata { get; set; }
      [DataMember]    public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
