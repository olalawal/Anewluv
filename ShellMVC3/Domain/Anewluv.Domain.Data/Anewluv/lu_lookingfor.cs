using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_lookingfor
    {
        public lu_lookingfor()
        {
            this.profiledata_lookingfor = new List<profiledata_lookingfor>();
            this.searchsetting_lookingfor = new List<searchsetting_lookingfor>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        public virtual ICollection<searchsetting_lookingfor> searchsetting_lookingfor { get; set; }
    }
}
