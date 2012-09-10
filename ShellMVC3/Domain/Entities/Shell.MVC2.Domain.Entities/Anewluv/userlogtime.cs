using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class userlogtime
    {
        [Key]
        public int id { get; set; }
        public DateTime? loginTime { get; set; }
        public DateTime? logoutTime { get; set; }
        public byte offline { get; set; }
       public int profile_id { get; set; }
        public virtual profile profile { get; set; }
        public string sessionid { get; set; }
    }
}
