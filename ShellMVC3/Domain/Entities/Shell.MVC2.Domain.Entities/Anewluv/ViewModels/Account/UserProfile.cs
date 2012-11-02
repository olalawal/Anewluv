using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class UserProfile
    {
        [Key]
        //  [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
