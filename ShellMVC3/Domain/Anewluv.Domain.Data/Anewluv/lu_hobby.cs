using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_hobby
    {
        public lu_hobby()
        {
            this.profiledata_hobby = new List<profiledata_hobby>();
            this.searchsetting_hobby = new List<searchsetting_hobby>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata_hobby> profiledata_hobby { get; set; }
        public virtual ICollection<searchsetting_hobby> searchsetting_hobby { get; set; }
    }
}
