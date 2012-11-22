using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.ApiKeyModel
{
     public class apikey
    {


         public apikey() 
          {
      
          
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }
            public int application_id { get; set; }
            public virtual lu_application  application { get; set; }
            public int accesslevel_id { get; set; }
            public virtual lu_accesslevel  accesslevel { get; set; } 
            public string externalapplicationname { get; set; }
            public virtual ICollection<user> registeredusers { get; set; }
            public Guid key { get; set; }
            public bool? active { get; set; }
            public DateTime? lastaccesstime { get; set; }
          

        }
    }

