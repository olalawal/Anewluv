using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web;
using System.ServiceModel;

using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Configuration;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using Shell.MVC2.Services.Contracts;

using Shell.MVC2.Infrastructure;
using System.Net;

namespace LoggingLibrary
{
    public class ErroLogging : IDisposable
    {
        private int iApplicationID;
        //private int lLogEntryID = 0;
        private errorlog oLogEntry = null;
        // private List<LogMessage> lstMessages;
        // private List<LogValue> lstValues;

        //private IErrorLoggingService LoggingServiceProxy;
        //ChannelFactory<IErrorLoggingService> ErrorLoggingfactory;
        private bool disposed = false;
        private int errorpass = 0;

        //chanle for notifications 
        //private IInfoNotificationService InfoNotificationServiceProxy;
        //ChannelFactory<IInfoNotificationService> InfoNotificationfactory;
       

        /// <summary>
        /// 
        /// </summary>
        //public LogSeverity Severity
        //{
        //    get { return oLogEntry.LogLevel; }
        //    set { oLogEntry.LogLevel = value; }
        //}

        public string ClientIP
        {
            get { return oLogEntry.ipaddress ; }
            set { oLogEntry.ipaddress  = value; }
        }

        public ErroLogging(applicationEnum  Application)
        {

            errorpass = 0;
            //lstMessages = new List<LogMessage>();
            // lstValues = new List<LogValue>();
            iApplicationID = Convert.ToInt32(Application);
           // var myendpoint  = CreateLoggingServiceEndpoint();
            //Binding binding = new WSHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://localhost/LoggingService/ErrorLoggingService.svc?wsdl");
            //ErrorLoggingServiceClient mysClient = new ErrorLoggingServiceClient(binding, endpointAddress);

            //mysClient.Test();
            // LogClient = new Log;//LoggerSoapClient();
            //oLogEntry = CreateLgEntryObject(null, null, logseverityEnum.Information);
           // ErrorLoggingfactory = new ChannelFactory<IErrorLoggingService>("webHttpBinding_IErrorLoggingService");//(mysClient.Endpoint);
           // LoggingServiceProxy = ErrorLoggingfactory.CreateChannel();

            //create chanle for notifcaiton servierc
           // InfoNotificationfactory = new ChannelFactory<IInfoNotificationService>("InfoNotificationService.soap");//(mysClient.Endpoint);
           //InfoNotificationServiceProxy =InfoNotificationfactory.CreateChannel();

            oLogEntry = new errorlog();


        }

        /// <summary>
        /// to do this not working , , need to be able to read the app config directly
        /// </summary>
        /// <returns></returns>
        //public ErrorLoggingServiceClient CreateLoggingServiceEndpoint()
        //{
        //    Binding binding = new WSHttpBinding();
        //    EndpointAddress endpointAddress = null;//= new EndpointAddress("http://localhost/LoggingService/ErrorLoggingService.svc?wsdl");
            

        //    var config = ConfigurationManager.OpenExeConfiguration("LoggingLibrary.dll.config"); 
        //    var wcfSection = ServiceModelSectionGroup.GetSectionGroup(config); 
        //    var clientSection = wcfSection.Client; 
        //    foreach (ChannelEndpointElement endpointElement in clientSection.Endpoints) 
        //    { 
        //       // if (endpointElement.Name == "XXX")
        //        // { 
        //          endpointAddress = new EndpointAddress(endpointElement.Address);
        //          binding = new WSHttpBinding(endpointElement.BindingConfiguration);
        //       //  } 
        //    }

        //    return new ErrorLoggingServiceClient(binding, endpointAddress);
        //}


        /// <summary>
        /// Writes a single entry.  No further writing to the current log entry is possible after commiting it.
        /// </summary>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="callingMethod">The calling method.</param>
        /// <param name="SessionID">The session ID.</param>
        /// <param name="OrderNumber">The order number.</param>
        /// <param name="RequisitionNumber">The requisition number.</param>

        public void WriteSingleEntry(logseverityEnum  severityLevelvalue, Exception referedexception,int? profileid=null,
                                    HttpContextBase context = null)
        {
            //set the error pass since this function can be used recursively
            //errorpass = errorpass + 1;

           // for (int i = 0; i <= 1; i++)
          //  {
               
                //        ' Attempt a maximum of 2 times if anything fails

            try
            {

                    //first  write database entry
                    //test of auth header info
                    Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IErrorLoggingService>.Authheader = "Y2FzZTpkcml2ZTMzMw==";
                    Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IErrorLoggingService>.Use(d =>
                    {

                        var id = d.WriteCompleteLogEntry(CreateErrorLog(severityLevelvalue,referedexception,profileid,context));
                        oLogEntry.id = id;
                    }
                    );
                    //var test = LoggingServiceProxy.WriteCompleteLogEntry(oLogEntry);
                    //modified to log the error and send message in one stroke
                    //moved this to a separate call
                    //ErrorNotificationServiceProxy.SendErrorMessageToDevelopers(oLogEntry);
                    // Clear();
                }

                //if we had an error logging this error log this
                catch (Exception ex)
                {
                   //we get an error trying to log log it here I guess i.e connecting or watatever
                    //write error directly
                    Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IErrorLoggingService>.Use(d =>
                    {
                        var id = d.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.CriticalError, ex));
                        oLogEntry.id = id;
                    });
                }

               //Attempt to send the notification 
                try
                {
                    //now write the email and send it
                    Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IInfoNotificationService>.Use(d =>
                    {
                        d.senderrormessage(oLogEntry, addresstypeenum.Developer.ToString());
                    }
                    );


                }
                catch(Exception ex)
                {
                    //write error directly
                    Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IErrorLoggingService>.Use(d =>
                    {
                        var id = d.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.CriticalError  ,ex));
                        oLogEntry.id = id;
                    }
                   );

                }
         

        }




        public errorlog CreateErrorLog(logseverityEnum severityLevelvalue, Exception referedexception, int? profileid = null,
                                  HttpContextBase context = null) 
          {
          

                //build the error object
                oLogEntry = new errorlog();


                //build the error stuff
                try
                {
                    StackTrace stackTrace = new StackTrace(referedexception, true);
                    StackFrame stackFrame = stackTrace.GetFrame(0);
                    MethodBase methodBase = stackFrame.GetMethod();

                    var inspectmoduletest = methodBase.Module;

                    //   ex.ToString();
                    //   Get stack trace for the exception with source file information 
                    //   var st = new StackTrace(ex, true);
                    //   Get the top stack frame 
                    //  var frame = st.GetFrame(0);
                    //     Get the line number from the stack frame 
                    //  var line = frame.GetFileLineNumber();




                    var messagetemp = referedexception.Message.ToString();
                    var Source = referedexception.Source;
                    var TargetSite = referedexception.TargetSite;
                    var StackTrace = referedexception.StackTrace;






                    if (referedexception != null && stackTrace != null)
                    {
                        oLogEntry.message = referedexception.ToString();
                        oLogEntry.stacktrace = stackTrace.ToString();
                        oLogEntry.linenumbers = stackFrame.GetFileLineNumber();
                        oLogEntry.methodname = methodBase.Name; ;
                        oLogEntry.assemblyname = GetAssemblyNameFromMethodBase(methodBase);
                        oLogEntry.parentmethodname = WhoCalledMe(referedexception);
                        oLogEntry.classname = methodBase.DeclaringType.Name;
                        oLogEntry.timestamp = DateTime.UtcNow;

                    }


                    if (context != null & !object.ReferenceEquals(context, string.Empty))
                    {
                        oLogEntry.loggeduser = context.User.Identity.Name;
                        oLogEntry.ipaddress = context.Request.UserHostAddress;
                        oLogEntry.errorpage = context.Request.Path;
                        oLogEntry.sessionid = context.Session.SessionID;

                        //
                        oLogEntry.request = context.Request.Form.ToString();
                        oLogEntry.querystring = context.Request.QueryString.ToString();
                    }

                    oLogEntry.application.id = this.iApplicationID;
                    oLogEntry.logseverity.id = (int)severityLevelvalue;

                    //replace with profile ID if we dont have it
                    if (profileid != null & !object.ReferenceEquals(profileid, string.Empty))
                    {
                        oLogEntry.profileid = Convert.ToString(profileid);
                    }
            }
                catch
            {

                
    }
            return oLogEntry;
          }

        /// <summary>
        /// Creates a new log entry and populate it with needed data.  This procedure does not write a log entry.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="sessionID">The session ID.</param>
        /// <param name="severityLevel">The severity level.</param>
        /// <returns></returns>
        private errorlog CreateLogEntryObject(MethodBase method, string sessionID, logseverityEnum severityLevel)
        {

            errorlog entry = new errorlog();

            if (method != null)
            {
                entry.assemblyname = GetAssemblyNameFromMethodBase(method);
                entry.classname = method.DeclaringType.Name;
                entry.methodname = method.Name;
            }
            entry.id  = iApplicationID;
            if (entry.ipaddress == null || object.ReferenceEquals(entry.ipaddress, string.Empty))
            {
                entry.ipaddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).ToString();
            }

            entry.loggeduser = sessionID;


            Shell.MVC2.Infrastructure.Channelfactoryhelper.Service<IErrorLoggingService>.Use(d =>
            { 
                
                entry.id = d.TranslateLogSeverity(severityLevel);
               
            }
            );

            //using (new OperationContextScope((IContextChannel)LoggingServiceProxy))
            //{
            //    entry.id = LoggingServiceProxy.TranslateLogSeverity(severityLevel);
            //}


            //entry.LoggedObject = new LogObject();

            return entry;
        }


        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            //oLogEntry = CreateLogEntryObject(Nothing, Nothing, LogSeverity.Information)
            //lLogEntryID = 0;
            //reset this
            oLogEntry = null;
            //lstValues.Clear();
            //// lstMessages.Clear();
           // ((IClientChannel)LoggingServiceProxy).Close();
           // ((IClientChannel)InfoNotificationServiceProxy).Close();
           // ErrorLoggingfactory.Close();
           //InfoNotificationfactory.Close();
          
        }

        private string GetAssemblyNameFromMethodBase(MethodBase method)
        {
            string assyName = method.DeclaringType.Assembly.CodeBase;
            assyName = assyName.Replace('/', '\\');
            assyName = assyName.Substring(assyName.LastIndexOf('\\') + 1);
            return assyName;
        }

        // Function to display parent function
        private static string WhoCalledMe(Exception ex)
        {
            StackTrace stackTrace = new StackTrace(ex, true);
            StackFrame stackFrame = stackTrace.GetFrame(1);
            
            MethodBase methodBase =  stackFrame != null ? stackFrame.GetMethod() : null;
            // Displays “WhatsmyName”
            if(methodBase != null)
            return String.Format(" Parent Method Name {0} ", methodBase.Name);

            return null;
        }
        private string EncodeObject(object obj)
        {
            if (obj == null)
                return string.Empty;

            try
            {
                IFormatter formatter = new BinaryFormatter();
                MemoryStream mstream = new MemoryStream();
                byte[] bytes = null;
                string str = null;

                formatter.Serialize(mstream, obj);
                bytes = mstream.ToArray();
                str = Convert.ToBase64String(bytes);

                return str;
            }
            catch (Exception ex)
            {
                return "Failed to serialize object.";
            }
        }

        private void WriteEventLog(string Message, EventLogEntryType ErrorLevel)
        {
            System.Diagnostics.EventLog.WriteEntry("Logger.dll", Message, ErrorLevel);
        }

        public void Dispose()
        {
            Dispose(true);

        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // component.Dispose();
                    Clear();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                Clear();

                // Note disposing has been done.
                disposed = true;

            }
        }



    }
}
