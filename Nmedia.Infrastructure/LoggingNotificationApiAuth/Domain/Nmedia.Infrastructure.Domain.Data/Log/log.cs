using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain.Data.log
{

    [DataContract(Namespace = "")]
     public class log : Entity
    {


         public log() 
          {
              logseverity = new lu_logseverity();
              application = new lu_logapplication();
              enviroment = new lu_logenviroment();
          
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }
            [DataMember]
            public lu_logseverity logseverity { get; set; }
            [DataMember]
            public lu_logseverityinternal logseverityinternal { get; set; } 
            //public virtual  LogSeverity Severity  { get; set; }  //complex type mapper
            //[NotMapped]
            //public logseverityEnum logseverityEnum
            //{
            //    get { return (logseverityEnum)LogSeverityLookupID; }
            //    set { LogSeverityLookupID = (int)value; }
            //} 
           [DataMember]
            public string loggeduser { get; set; }
            [DataMember]
            public object loggedobject { get; set; }

            //public int application_id { get; set; }
            [DataMember]
            public lu_logapplication application { get; set; }
            [DataMember]
            public lu_logenviroment enviroment { get; set; }
            
            public string ipaddress { get; set; }
            [DataMember]
            public string assemblyname { get; set; }
            [DataMember]
            public string classname { get; set; }
            [DataMember]
            public string methodname { get; set; }
            [DataMember]
            public string parentmethodname { get; set; }
            [DataMember]
            public string profileid { get; set; }
            [DataMember]
            public string errorpage { get; set; }
          [DataMember]
            public string type { get; set; }
          [DataMember]
            public int linenumbers { get; set; }
          [DataMember]
            public string stacktrace { get; set; }
          [DataMember]
            public string message { get; set; }
          [DataMember]
          public string sessionid { get; set; }
          [DataMember]
          public string querystring { get; set; }
          [DataMember]
          public string request { get; set; }

          public static log Create(Action<log> init)
          {
              log log = new log();
            //  log.LogSeverity = new lu_logSeverity();
            //  log.Application = new lu_application();

              init(log);
              return log;
          }

          

        }
    }

