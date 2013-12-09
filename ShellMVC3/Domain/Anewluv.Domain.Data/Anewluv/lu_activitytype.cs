using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_activitytype
    {
        public lu_activitytype()
        {
            this.profileactivities = new List<profileactivity>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profileactivity> profileactivities { get; set; }
    }
}
