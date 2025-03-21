using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class applicationrole
    {

       public applicationrole()
       {
           this.application = new application();
           this.lu_role = new lu_role();
       }

        [DataMember]  public int id { get; set; }
        [DataMember]  public Nullable<bool> active { get; set; }
        [DataMember]  public int application_id { get; set; }
       [DataMember]   public Nullable<System.DateTime> roleexpiredate { get; set; }
       [DataMember]   public Nullable<System.DateTime> rolestartdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> deactivationdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> creationdate { get; set; }      
       [DataMember]   public Nullable<int> role_id { get; set; }
       [DataMember]
       public virtual application application { get; set; }
       [DataMember]
       public virtual lu_role lu_role { get; set; }


    }
}
