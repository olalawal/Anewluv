using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SystemPageSetting
    {
        public string Titile { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string BodyCssSyleName { get; set; }
        public Nullable<int> HitCount { get; set; }
        public Nullable<bool> IsMasterPage { get; set; }
    }
}
