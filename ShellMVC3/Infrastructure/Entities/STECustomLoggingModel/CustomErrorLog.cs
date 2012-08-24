using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CustomLoggingModel
{
     public class CustomErrorLog
    {
   


            
       
            [Key]
            public int CustomErrorLogID { get; set; }
            [DataMember]
            public DateTime? TimeStamp { get; set; }

            
            public int LogSeverityLookupID { get; set; }
            public virtual LogSeverityLookup LogSeverity { get; set; } 
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
            [DataMember]
            public int ApplicationLookupID { get; set; }
            public virtual ApplicationLookup  Application { get; set; }
            
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

          [DataMember]
            public string  OrderNumber  { get; set; }
          [DataMember]
            public string RequisitionNumber { get; set;}

          

        }
    }

