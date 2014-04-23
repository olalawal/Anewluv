using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract]  public partial class applicationitem
    {
        [DataMember]  public int id { get; set; }
        [DataMember]  public int application_id { get; set; }
        [DataMember]  public int purchaserprofile_id { get; set; }
        [DataMember]  public int transferprofile_id { get; set; }
        [DataMember]       
        public System.DateTime purchasedate { get; set; }
       [DataMember]   public Nullable<System.DateTime> transferdate { get; set; }
       [DataMember]   public Nullable<System.DateTime> activateddate { get; set; }
       [DataMember]   public Nullable<System.DateTime> expirationdate { get; set; }
       [DataMember]   public Nullable<int> application_id1 { get; set; }
       [DataMember]   public Nullable<int> purchaserprofile_id1 { get; set; }
       [DataMember]   public Nullable<int> transferprofile_id1 { get; set; }
       [DataMember]   public Nullable<int> transfertype_id { get; set; }
       [DataMember]   public Nullable<int> paymenttype_id { get; set; }
       [DataMember]   public Nullable<int> profile_id { get; set; }
       [DataMember]   public Nullable<int> profile_id1 { get; set; }
       [DataMember]
       public virtual application application { get; set; }
       [DataMember]
       public virtual lu_applicationitempaymenttype lu_applicationitempaymenttype { get; set; }
       [DataMember]
       public virtual lu_applicationitemtransfertype lu_applicationitemtransfertype { get; set; }
        [DataMember]   public virtual profile profile { get; set; }
        [DataMember]   public virtual profile profile1 { get; set; }
        [DataMember]   public virtual profile profile2 { get; set; }
        [DataMember]   public virtual profile profile3 { get; set; }
    }
}
