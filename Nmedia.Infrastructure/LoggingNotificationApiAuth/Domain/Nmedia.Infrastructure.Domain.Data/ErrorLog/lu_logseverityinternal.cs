using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualBasic;

using System.Collections;

using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Nmedia.Infrastructure.Domain.Data.errorlog;

namespace Nmedia.Infrastructure.Domain.Data.errorlog
{
    [DataContract(Namespace = "")]
    public class lu_logseverityinternal
    {
        //we generate this manually from enums for now
       [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }

    }

   
}
