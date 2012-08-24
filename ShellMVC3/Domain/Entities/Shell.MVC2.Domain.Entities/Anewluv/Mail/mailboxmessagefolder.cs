using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class mailboxmessagefolder
    {
        [Key]
        public int id { get; set; }
        public int mailboxfolder_id { get; set; }
        public int mailboxmessage_id { get; set; }     
        public mailboxfolder mailboxfolder { get; set; }  
        public mailboxmessage mailboxmessage { get; set; }    
        public DateTime? deleteddate { get; set; }       
        public DateTime? draftdate { get; set; }       
        public DateTime? flaggeddate { get; set; }
        public DateTime? readdate { get; set; }
        //what to do aboutrecent flag
        public bool? recent { get; set; }
        public DateTime? replieddate { get; set; }

    }
}
