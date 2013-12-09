using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_religiousattendance
    {
        public lu_religiousattendance()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_religiousattendance = new List<searchsetting_religiousattendance>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_religiousattendance> searchsetting_religiousattendance { get; set; }
    }
}
