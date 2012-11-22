using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
     public class CustomErrorLog
    {


         public CustomErrorLog() 
          {
            //  LogSeverity = new lu_logSeverity();
            //  Application = new lu_application();
          
          } 
            
       
            [Key]
            public int id { get; set; }
            [DataMember]
            public DateTime? TimeStamp { get; set; }

            public int logseverity_id { get; set; }
            public virtual lu_logSeverity  LogSeverity { get; set; } 
            //public virtual  LogSeverity Severity  { get; set; }  //complex type mapper
            //[NotMapped]
            //public LogSeverityEnum LogSeverityEnum
            //{
            //    get { return (LogSeverityEnum)LogSeverityLookupID; }
            //    set { LogSeverityLookupID = (int)value; }
            //} 

            public string LoggedUser { get; set; }
            [DataMember]
            public object LoggedObject { get; set; }

            public int application_id { get; set; }
            public virtual lu_Application   Application { get; set; }
            
            public string IPAddress { get; set; }
            [DataMember]
            public string AssemblyName { get; set; }
            [DataMember]
            public string ClassName { get; set; }
            [DataMember]
            public string MethodName { get; set; }
            [DataMember]
            public string ParentMethodName { get; set; }
            [DataMember]
            public string ProfileID { get; set; }
            [DataMember]
            public string ErrorPage { get; set; }
          [DataMember]
            public string Type { get; set; }
          [DataMember]
            public int LineNumbers { get; set; }
          [DataMember]
            public string Stacktrace { get; set; }
          [DataMember]
            public string Message { get; set; }
          [DataMember]
          public string Sessionid { get; set; }
          [DataMember]
          public string QueryString { get; set; }
          [DataMember]
          public string Request { get; set; }

          public static CustomErrorLog Create(Action<CustomErrorLog> init)
          {
              CustomErrorLog CustomErrorLog = new CustomErrorLog();
            //  CustomErrorLog.LogSeverity = new lu_logSeverity();
            //  CustomErrorLog.Application = new lu_application();

              init(CustomErrorLog);
              return CustomErrorLog;
          }

          

        }
    }

