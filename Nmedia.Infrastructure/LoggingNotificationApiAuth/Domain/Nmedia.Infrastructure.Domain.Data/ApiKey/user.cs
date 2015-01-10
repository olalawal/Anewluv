using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Infrastructure.Domain.Data.Apikey
{
    [DataContract(Namespace = "")]
     public class user
    {


         public user() 
          {
            //  LogSeverity = new lu_logSeverity();
            //  Application = new lu_application();
              apikeys = new List<apikey>();
          } 
            
       
            [Key]
            public int id { get; set; }          
            [DataMember]
            public DateTime? timestamp { get; set; }
            [DataMember]   
            public string username { get; set; }
            [DataMember]
            public int useridentifier { get; set; }
            [DataMember]
            public string email { get; set; }
            [DataMember]   
            public bool active { get; set; }
            [DataMember]   
            public string registeringapplication { get; set; }
            public virtual ICollection<apikey> apikeys { get; set; }
         

          

        }
    }

