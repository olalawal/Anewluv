using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class application
    {
        public application()
        {
            this.applicationiconconversions = new List<applicationiconconversion>();
            this.applicationitems = new List<applicationitem>();
            this.applicationroles = new List<applicationrole>();
        }

        [DataMember]  public int id { get; set; }
        public System.DateTime creationdate { get; set; }
        [DataMember] public bool active { get; set; }
       [DataMember]   public Nullable<System.DateTime> deactivationdate { get; set; }
       [DataMember]   public Nullable<int> applicationtype_id { get; set; }
       [DataMember]
       public virtual ICollection<applicationiconconversion> applicationiconconversions { get; set; }
       [DataMember]
       public virtual ICollection<applicationitem> applicationitems { get; set; }
       [DataMember]
       public virtual ICollection<applicationrole> applicationroles { get; set; }
       [DataMember]
       public virtual lu_applicationtype lu_applicationtype { get; set; }
    }
}
