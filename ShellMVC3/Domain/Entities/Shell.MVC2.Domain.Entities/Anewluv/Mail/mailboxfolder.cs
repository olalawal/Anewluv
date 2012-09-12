using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class mailboxfolder
    {
        [Key]
        public int id { get; set; }
        public int profiled_id { get; set; }
        public virtual profilemetadata  profilemetadata { get; set; }
        public int? active { get; set; }
        public int foldertype_id { get; set; }
        public virtual mailboxfoldertype foldertype { get; set; }       
        public virtual ICollection<mailboxmessagefolder> mailboxmessagesfolder { get; set; } 
       

    }
}
