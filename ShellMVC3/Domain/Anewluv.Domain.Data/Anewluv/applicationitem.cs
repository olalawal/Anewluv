using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class applicationitem
    {
        public int id { get; set; }
        public int application_id { get; set; }
        public int purchaserprofile_id { get; set; }
        public int transferprofile_id { get; set; }
        public System.DateTime purchasedate { get; set; }
        public Nullable<System.DateTime> transferdate { get; set; }
        public Nullable<System.DateTime> activateddate { get; set; }
        public Nullable<System.DateTime> expirationdate { get; set; }
        public Nullable<int> application_id1 { get; set; }
        public Nullable<int> purchaserprofile_id1 { get; set; }
        public Nullable<int> transferprofile_id1 { get; set; }
        public Nullable<int> transfertype_id { get; set; }
        public Nullable<int> paymenttype_id { get; set; }
        public Nullable<int> profile_id { get; set; }
        public Nullable<int> profile_id1 { get; set; }
        public virtual application application { get; set; }
        public virtual lu_applicationitempaymenttype lu_applicationitempaymenttype { get; set; }
        public virtual lu_applicationitemtransfertype lu_applicationitemtransfertype { get; set; }
        public virtual profile profile { get; set; }
        public virtual profile profile1 { get; set; }
        public virtual profile profile2 { get; set; }
        public virtual profile profile3 { get; set; }
    }
}
