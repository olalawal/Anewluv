using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{



    public class lu_news
    {
        //we generate this manually from enums for now
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public DateTime? creationdate { get; set; }
        public bool? active { get; set; }  
        public DateTime? removaldate { get; set; }
        public bool? curentmessagenews { get; set; }

    }
}
