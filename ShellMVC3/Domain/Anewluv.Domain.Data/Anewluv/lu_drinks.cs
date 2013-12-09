using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_drinks
    {
        public lu_drinks()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_drink = new List<searchsetting_drink>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_drink> searchsetting_drink { get; set; }
    }
}
