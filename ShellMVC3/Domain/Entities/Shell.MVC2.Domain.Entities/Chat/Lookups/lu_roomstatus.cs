using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    [DataContract]
    public class lu_roomstatus
    {
      
        [Key]
        public int id { get; set; }
        public string description { get; set; }
       [NotMapped]
        public bool selected { get; set; }
    }
}
