using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profiledata_hotfeature
    {

        
        public virtual lu_character_hotfeature hotfeature { get; set; }
        [Key ]
        public int id { get; set; }
       public virtual profiledata profiledata { get; set; }
       
    }
}
