using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class visiblitysetting
    {
        [Key]
        public string id { get; set; }

        public string profile_id { get; set; }
       public virtual profiledata profiledata { get; set; }

        public int? AgeMaxVisibility { get; set; }
        public int? AgeMinVisibility { get; set; }
        public bool? ChatVisiblityToInterests { get; set; }
        public bool? ChatVisiblityToLikes { get; set; }
        public bool? ChatVisiblityToMatches { get; set; }
        public bool? ChatVisiblityToPeeks { get; set; }
        public bool? ChatVisiblityToSearch { get; set; }
        public int? CountryID { get; set; }
        public virtual lu_gender  gender { get; set; }     
        public DateTime? LastUpdateDate { get; set; }
        public bool? MailChatRequest { get; set; }
        public bool? MailIntrests { get; set; }
        public bool? MailLikes { get; set; }
        public bool? MailMatches { get; set; }
        public bool? MailNews { get; set; }
        public bool? MailPeeks { get; set; }
        public bool? ProfileVisiblity { get; set; }
        public bool? SaveOfflineChat { get; set; }
        public bool? SteathPeeks { get; set; }

    }
}
