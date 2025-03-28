﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class mailboxmessage
    {
        [Key]
        public int id { get; set; }
        public DateTime? creationdate { get; set; }
        public int recipient_id { get; set; }
        public int sender_id { get; set; }
        public virtual profilemetadata recipeint { get; set; }
        public virtual profilemetadata sender { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public int? uniqueid { get; set; }
        public virtual ICollection<mailboxmessagefolder> mailboxmessagesfolders { get; set; } 
       

    }
}
