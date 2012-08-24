using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.UserRepairModel
{
    public class UserRepairLog
    {      
           
            [Key]
        public int UserRepairLogID { get; set; }
            public string ProfileID { get; set; }
                      public DateTime? DateFixed { get; set; }            
            public string CountryName { get; set; }
            public string RepairReason { get; set; }
            
           

    }
}
