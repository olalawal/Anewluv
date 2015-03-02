using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.Apikey
{
    [DataContract(Namespace = "")]
     public class apikey :Entity
    {


         public apikey() 
          {
              keyvalue = Guid.NewGuid();
              accesslevel = null;
              application = null;
              user = null;  //default user to null
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }
            [DataMember]
            public int application_id { get; set; }
            [DataMember]
            public virtual lu_application application { get; set; }
            [DataMember]
            public int accesslevel_id { get; set; }
            [DataMember]
            public virtual lu_accesslevel accesslevel { get; set; }
            [DataMember]
            public string externalapplicationname { get; set; }
            [DataMember]
            public int? user_id { get; set; }
            [DataMember]
            public virtual user user { get; set; }
            [DataMember]
            public Guid keyvalue { get; set; }
            [DataMember]
            public bool? active { get; set; }
            [DataMember]
            public DateTime? lastaccesstime { get; set; }
          

        }
    }

