using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_hotfeature
    {
        public lu_hotfeature()
        {
            this.profiledata_hotfeature = new List<profiledata_hotfeature>();
            this.searchsetting_hotfeature = new List<searchsetting_hotfeature>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        public virtual ICollection<searchsetting_hotfeature> searchsetting_hotfeature { get; set; }
    }
}
