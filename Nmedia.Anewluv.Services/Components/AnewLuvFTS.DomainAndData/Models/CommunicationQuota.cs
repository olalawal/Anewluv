using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CommunicationQuota
    {
        public int QuotaID { get; set; }
        public string QuotaName { get; set; }
        public string QuotaDescription { get; set; }
        public Nullable<int> QuotaValue { get; set; }
        public Nullable<int> QuotaRoleID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
