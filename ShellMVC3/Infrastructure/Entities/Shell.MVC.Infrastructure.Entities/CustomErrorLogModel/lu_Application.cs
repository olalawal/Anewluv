using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{



    public class lu_application
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [DataMember()]
        public string ApplicationName { get; set; }



    }
}
