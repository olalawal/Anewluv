using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SecurityQuestion
    {
        public SecurityQuestion()
        {
            this.profiles = new List<profile>();
        }

        public byte SecurityQuestionID { get; set; }
        public string SecurityQuestionText { get; set; }
        public virtual ICollection<profile> profiles { get; set; }
    }
}
