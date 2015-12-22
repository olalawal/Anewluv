using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class application : Entity
    {
        public application()
        {
            this.applicationiconconversions = new List<applicationiconconversion>();            
            this.applicationroles = new List<applicationrole>();
        }

        [DataMember]  public int id { get; set; }
        
        [DataMember]   public Nullable<int> applicationtype_id { get; set; }
        [DataMember]   public Nullable<int> transfertype_id { get; set; }
        [DataMember]   public Nullable<int> paymenttype_id { get; set; }

        [DataMember]
        public int profile_id { get; set; }
      
        [DataMember]
        public int purchaserprofile_id { get; set; }  //foir history who owned it initally        

        public System.DateTime creationdate { get; set; }
        [DataMember]
        public bool active { get; set; }
        [DataMember]
        public Nullable<System.DateTime> deactivationdate { get; set; }
        [DataMember]
        public System.DateTime purchasedate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> lasttransferdate { get; set; }
       [DataMember]
       public Nullable<System.DateTime>  activateddate { get; set; }
       [DataMember]
       public Nullable<System.DateTime>  expirationdate { get; set; }


      
       [DataMember]
       public virtual lu_applicationpaymenttype lu_applicationpaymenttype { get; set; }
       [DataMember]
       public virtual lu_applicationtransfertype lu_applicationtransfertype { get; set; }    
       [DataMember]
       public virtual lu_applicationtype lu_applicationtype { get; set; }
       [DataMember]
       public virtual ICollection<applicationiconconversion> applicationiconconversions { get; set; }
       [DataMember]
       public virtual ICollection<applicationrole> applicationroles { get; set; }

       public virtual profilemetadata profilemetadata { get; set; }

    }
}
