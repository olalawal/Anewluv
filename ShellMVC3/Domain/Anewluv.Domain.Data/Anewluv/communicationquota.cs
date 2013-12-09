using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class communicationquota
    {
        public int id { get; set; }
        public Nullable<bool> active { get; set; }
        public string quotadescription { get; set; }
        public string quotaname { get; set; }
        public Nullable<int> quotaroleid { get; set; }
        public Nullable<int> quotavalue { get; set; }
        public string updaterprofile_id { get; set; }
        public Nullable<System.DateTime> updatedate { get; set; }
        public int updaterprofiledata_profile_id { get; set; }
        public virtual profiledata profiledata { get; set; }
    }
}
