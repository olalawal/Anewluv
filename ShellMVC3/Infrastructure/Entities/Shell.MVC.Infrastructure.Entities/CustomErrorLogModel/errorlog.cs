using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
     public class errorlog
    {


         public errorlog() 
          {
            //  LogSeverity = new lu_logSeverity();
            //  Application = new lu_application();
          
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? timestamp { get; set; }          
            public  lu_logseverity  logseverity { get; set; }     
            public  lu_logseverityinternal logseverityinternal { get; set; } 
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
            public lu_application   application { get; set; }
            
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
              errorlog CustomErrorLog = new errorlog();
            //  CustomErrorLog.LogSeverity = new lu_logSeverity();
            //  CustomErrorLog.Application = new lu_application();

              init(CustomErrorLog);
              return CustomErrorLog;
          }

          

        }
    }

