using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Dating
{
    class SearchSettings_Tribe
    {
        [Key]
        public int id { get; set; }
        public SearchSetting SearchSetting { get; set; }
    

    }
}
