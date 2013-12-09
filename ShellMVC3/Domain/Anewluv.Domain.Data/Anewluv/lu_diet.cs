using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_diet
    {
        public lu_diet()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_diet = new List<searchsetting_diet>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_diet> searchsetting_diet { get; set; }
    }
}
