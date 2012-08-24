using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class mailboxmessage
    {
        [Key]
        public int id { get; set; }  
        public virtual ICollection<mailboxfolder>  messagefolders { get; set; }
        public DateTime? creationdate { get; set; }
        public string recipient_id { get; set; }
        public string sender_id { get; set; }
        public virtual profile recipeint { get; set; }
        public virtual profile sender { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public int? uniqueid { get; set; }

    }
}
