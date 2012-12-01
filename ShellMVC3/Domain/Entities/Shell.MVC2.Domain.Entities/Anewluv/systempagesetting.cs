using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract]
    public class systempagesetting
    {
        [Key]
        public int id { get; set; }
        public string bodycssstylename { get; set; }
        public string description { get; set; }
        public int? hitCount { get; set; }
        public bool? ismasterpage { get; set; }
        public string path { get; set; }
        public string title { get; set; }
    }
}
