using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Infrastructure.Domain.Data.ApiKey
{
    [DataContract(Namespace = "")]
     public class apikey
    {


         public apikey() 
          {
      
          
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }      
            public virtual lu_application  application { get; set; }        
            public virtual lu_accesslevel  accesslevel { get; set; } 
            public string externalapplicationname { get; set; }
            public virtual user? user { get; set; }
            [DataMember]
            public Guid key { get; set; }
            public bool? active { get; set; }
            public DateTime? lastaccesstime { get; set; }
          

        }
    }

