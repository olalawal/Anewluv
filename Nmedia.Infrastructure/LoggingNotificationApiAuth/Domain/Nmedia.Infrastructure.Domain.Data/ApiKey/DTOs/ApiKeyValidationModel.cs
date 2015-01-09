using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nmedia.Infrastructure.Domain.Data.ApiKey.DTOs
{
     [DataContract]
   public class ApiKeyValidationModel
    {
         [DataMember]
         public string service { get; set; }
         [DataMember]
         public string username { get; set; }
         [DataMember]
         public int useridentifier { get; set; }
         [DataMember]
         public string application { get; set; }
         [DataMember]
         public Guid? key { get; set; }

    }
}
