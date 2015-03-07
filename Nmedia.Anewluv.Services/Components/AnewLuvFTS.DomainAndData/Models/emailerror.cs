using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class emailerror
    {
        public int EmailErrorID { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public System.DateTime ErrorDate { get; set; }
        public string ExceptionError { get; set; }
    }
}
