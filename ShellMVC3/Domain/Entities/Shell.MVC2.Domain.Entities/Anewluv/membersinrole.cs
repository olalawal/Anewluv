using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class membersinrole
    {
        [Key]
        public int id { get; set; }
        public bool? active { get; set; }
        public string profile_id { get; set; }        
        public virtual profile profile { get; set; }
        public virtual lu_role role { get; set; }
        public DateTime? roleExpireDate { get; set; }
        //public int? roleID { get; set; }
        public DateTime? roleStartDate { get; set; }
    }
}
