using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class mailboxfolder
    {
        [Key]
        public int id { get; set; }
        public int profiled_id { get; set; }
        public virtual profilemetadata  profilemetadata { get; set; }
        public int? active { get; set; } 
        public virtual mailboxfoldertype foldertype { get; set; }       
        public virtual ICollection<mailboxmessagefolder> mailboxmessagesfolders { get; set; } 
       

    }
}
