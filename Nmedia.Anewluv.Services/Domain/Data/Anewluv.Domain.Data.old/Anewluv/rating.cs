using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class rating
    {
        public rating()
        {
            this.ratingvalues = new List<ratingvalue>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public Nullable<int> ratingmaxvalue { get; set; }
        public Nullable<int> ratingweight { get; set; }
        public Nullable<long> increment { get; set; }
        public virtual ICollection<ratingvalue> ratingvalues { get; set; }
    }
}
