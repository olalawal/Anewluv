using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class systempagesetting
    {
        public int id { get; set; }
        public string bodycssstylename { get; set; }
        public string description { get; set; }
        public Nullable<int> hitCount { get; set; }
        public Nullable<bool> ismasterpage { get; set; }
        public string path { get; set; }
        public string title { get; set; }
    }
}
