using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class mailboxfoldertype
    {
        [Key]
        public int id { get; set; }       
        public virtual ICollection<mailboxfolder> folders { get; set; }
        //optional
        public virtual lu_defaultmailboxfolder defaultfolder { get; set; }
        public string name { get; set; }
        public bool? active { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? deleteddate { get; set; }
        public int? maxsize { get; set; }

      
        
    }
}
