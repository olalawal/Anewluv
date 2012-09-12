using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    /// <summary>
    /// custom join table so we define it ourslefs
    /// </summary>
    public class mailboxmessagefolder
    {
     
        //Key is a custom composite key
        //[Key]
        //public int id { get; set; }
        public int mailboxfolder_id { get; set; }
        public int mailboxmessage_id { get; set; }     
        public virtual mailboxfolder mailboxfolder { get; set; }
        public virtual mailboxmessage mailboxmessage { get; set; }    
        public DateTime? deleteddate { get; set; }       
        public DateTime? draftdate { get; set; }       
        public DateTime? flaggeddate { get; set; }
        public DateTime? readdate { get; set; }
        //what to do aboutrecent flag
        public bool? recent { get; set; }
        public DateTime? replieddate { get; set; }
      
    }
}
