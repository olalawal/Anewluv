using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nemdia.Infrastructure.Domain.Data.ApiKey
{
    [DataContract(Namespace = "")]
     public class apicall
    {


         public apicall() 
          {
      
          
          } 
                   
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }
            [DataMember]
            public virtual apikey apikey { get; set; }
            [DataMember]
            public string ipaddress { get; set; }
            [DataMember]
            public string destinationurl { get; set; }
          

        }
    }

