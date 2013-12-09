using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_ethnicity
    {
        public lu_ethnicity()
        {
            this.profiledata_ethnicity = new List<profiledata_ethnicity>();
            this.searchsetting_ethnicity = new List<searchsetting_ethnicity>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata_ethnicity> profiledata_ethnicity { get; set; }
        public virtual ICollection<searchsetting_ethnicity> searchsetting_ethnicity { get; set; }
    }
}
