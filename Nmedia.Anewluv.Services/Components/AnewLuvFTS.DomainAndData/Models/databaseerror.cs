using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class databaseerror
    {
        public short ErrorID { get; set; }
        public byte[] ErrorDate { get; set; }
        public string SqlStatement { get; set; }
        public string Exception { get; set; }
        public string PageName { get; set; }
    }
}
