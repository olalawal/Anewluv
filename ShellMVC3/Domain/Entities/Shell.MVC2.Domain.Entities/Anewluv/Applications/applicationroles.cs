using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class applicationroles
    {
        public int application_id { get; set; }
        public int role_id { get; set; }
        public virtual application application { get; set; }
        public virtual lu_role  role { get; set; }
        public DateTime? deactivationdate { get; set; }
        public DateTime? creationdate { get; set; }
        public DateTime? activedate { get; set; }   
 
    }
}
