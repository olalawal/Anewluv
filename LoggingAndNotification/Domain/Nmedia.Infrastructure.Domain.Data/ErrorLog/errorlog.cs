using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Infrastructure.Domain.Data.errorlog
{

    [DataContract(Namespace = "")]
     public class errorlog
    {


         public errorlog() 
          {
              logseverity = new lu_logseverity();
              application = new lu_application();
              enviroment = new lu_enviroment();
          
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
            public lu_application application { get; set; }
            [DataMember]
            public lu_enviroment enviroment { get; set; }
            
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

          public static errorlog Create(Action<errorlog> init)
          {
              errorlog errorlog = new errorlog();
            //  errorlog.LogSeverity = new lu_logSeverity();
            //  errorlog.Application = new lu_application();

              init(errorlog);
              return errorlog;
          }

          

        }
    }

