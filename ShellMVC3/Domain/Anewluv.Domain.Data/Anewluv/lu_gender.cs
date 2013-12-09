using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_gender
    {
        public lu_gender()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_gender = new List<searchsetting_gender>();
            this.visiblitysettings_gender = new List<visiblitysettings_gender>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_gender> searchsetting_gender { get; set; }
        public virtual ICollection<visiblitysettings_gender> visiblitysettings_gender { get; set; }
    }
}
