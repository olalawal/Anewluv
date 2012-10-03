using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //appearance
    public class lu_haircolor
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool selected { get; set; }
     
    }
}
