using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Nmedia.Infrastructure.Domain.Data.Notification
{



    public class lu_newstype
    {
        //we generate this manually from enums for now
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }


    }
}
