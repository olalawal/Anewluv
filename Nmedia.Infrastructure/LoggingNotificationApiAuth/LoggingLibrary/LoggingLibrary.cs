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
using Nmedia.Infrastructure.Domain.Data.log;


using System.Net;
using Nmedia.Services.Contracts;

using Nmedia.Infrastructure.Domain.Data.Notification;
using Nmedia.Infrastructure;

using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class Logging : IDisposable
    {
        private int iApplicationID;
        //private int lLogEntryID = 0;
        private log oLogEntry = null;
        // private List<LogMessage> lstMessages;
        // private List<LogValue> lstValues;

        //private IAnewluvLoggingService LoggingServiceProxy;
        //ChannelFactory<IAnewluvLoggingService> ErrorLoggingfactory;
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
            get { return oLogEntry.ipaddress; }
            set { oLogEntry.ipaddress = value; }
        }

        public Logging(applicationEnum Application)
        {

            errorpass = 0;
            //lstMessages = new List<LogMessage>();
            // lstValues = new List<LogValue>();
            iApplicationID = Convert.ToInt32(Application);
            // var myendpoint  = CreateLoggingServiceEndpoint();
            //Binding binding = new WSHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://localhost/LoggingService/LoggingService.svc?wsdl");
            //LoggingServiceClient mysClient = new LoggingServiceClient(binding, endpointAddress);

            //mysClient.Test();
            // LogClient = new Log;//LoggerSoapClient();
            //oLogEntry = CreateLgEntryObject(null, null, logseverityEnum.Information);
            // ErrorLoggingfactory = new ChannelFactory<IAnewluvLoggingService>("webHttpBinding_IAnewluvLoggingService");//(mysClient.Endpoint);
            // LoggingServiceProxy = ErrorLoggingfactory.CreateChannel();

            //create chanle for notifcaiton servierc
            // InfoNotificationfactory = new ChannelFactory<IInfoNotificationService>("InfoNotificationService.soap");//(mysClient.Endpoint);
            //InfoNotificationServiceProxy =InfoNotificationfactory.CreateChannel();

            oLogEntry = new log();


        }

        public Logging(int Applicationid)
        {

            errorpass = 0;
            //lstMessages = new List<LogMessage>();
            // lstValues = new List<LogValue>();
            iApplicationID = Applicationid;
            // var myendpoint  = CreateLoggingServiceEndpoint();
            //Binding binding = new WSHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://localhost/LoggingService/LoggingService.svc?wsdl");
            //LoggingServiceClient mysClient = new LoggingServiceClient(binding, endpointAddress);

            //mysClient.Test();
            // LogClient = new Log;//LoggerSoapClient();
            //oLogEntry = CreateLgEntryObject(null, null, logseverityEnum.Information);
            // ErrorLoggingfactory = new ChannelFactory<IAnewluvLoggingService>("webHttpBinding_IAnewluvLoggingService");//(mysClient.Endpoint);
            // LoggingServiceProxy = ErrorLoggingfactory.CreateChannel();

            //create chanle for notifcaiton servierc
            // InfoNotificationfactory = new ChannelFactory<IInfoNotificationService>("InfoNotificationService.soap");//(mysClient.Endpoint);
            //InfoNotificationServiceProxy =InfoNotificationfactory.CreateChannel();

            oLogEntry = new log();


        }

        /// <summary>
        /// Writes a single entry.  No further writing to the current log entry is possible after commiting it.
        /// </summary>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="callingMethod">The calling method.</param>
        /// <param name="SessionID">The session ID.</param>
        /// <param name="OrderNumber">The order number.</param>
        /// <param name="RequisitionNumber">The requisition number.</param>

        public void WriteSingleEntry(logseverityEnum severityLevelvalue, enviromentEnum enviroment, Exception referedexception, int? profileid = null,
                                    HttpContext context = null, bool? sendnotification = false)
        {

            //set the error pass since this function can be used recursively
            //errorpass = errorpass + 1;

            // for (int i = 0; i <= 1; i++)
            //  {
            //        ' Attempt a maximum of 2 times if anything fails

            try
            {

                //new LamdaProxyHelper<IAnewluvLoggingService>().UseAsync(serviceProxy =>
                //    {
                //        serviceProxy.BeginWriteCompleteLogEntry(CreateErrorLog(severityLevelvalue, enviroment, referedexception, username, context), null, null);  
                //    }, AsyncResultCallBack, Guid.NewGuid());


                //first  write database entry
                //test of auth header info
                //Channelfactoryhelper.Service<IAnewluvLoggingService>.Use(d =>
                //{
                //   // errorlog dd = CreateErrorLog(severityLevelvalue, referedexception,username,null);                       
                //    //AsyncCallback callback = result =>
                //    //{
                //    //    var endoflogging = d.EndWriteCompleteLogEntry(result);
                //    //    //Debug.WriteLine("I Have Makes");
                //    //}; 
                //    d.WriteCompleteLogEntry(CreateErrorLog(severityLevelvalue, enviroment, referedexception, username, context)d);
                //}
                //);
                //var test = LoggingServiceProxy.WriteCompleteLogEntry(oLogEntry);
                //modified to log the error and send message in one stroke
                //moved this to a separate call
                //ErrorNotificationServiceProxy.SendErrorMessageToDevelopers(oLogEntry);
                // Clear();
                WebChannelFactory<IAnewluvLoggingService> cflogging = new WebChannelFactory<IAnewluvLoggingService>("webHttpBinding_IAnewluvLoggingService");
                IAnewluvLoggingService channellogging = cflogging.CreateChannel();
                channellogging.WriteCompleteLogEntry(oLogEntry);
               // bool result = await returnedTaskTResult;
                cflogging.Close();
            }

                //if we had an error logging this error log this
            catch (Exception ex)
            {
                ////we get an error trying to log log it here I guess i.e connecting or watatever
                // //write error directly
                //Channelfactoryhelper.Service<IAnewluvLoggingService>.Use(d =>
                // {
                //    // var id = d.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.CriticalError, ex));
                //   //  oLogEntry.id = id;
                //     //AsyncCallback callback = result =>
                //     //{
                //     //    var endoflogging = d.EndWriteCompleteLogEntry(result);
                //     //    //Debug.WriteLine("I Have Makes");
                //     //};
                //     d.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.CriticalError, enviroment, ex));

                // });

                //WebChannelFactory<IAnewluvLoggingService> cflogging = new WebChannelFactory<IAnewluvLoggingService>("webHttpBinding_IAnewluvLoggingService");
                //IAnewluvLoggingService channellogging = cflogging.CreateChannel();
                //channellogging.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.CriticalError, enviroment, ex));
                //cflogging.Close();
            }

            //Attempt to send the notification if we requested it
            //i.e if calling write log errror from the notification service its slef we should not be sending a notification !!! or its a loop
            if (sendnotification == true)
            {
                try
                {
                    //now write the email and send it
                    //add the auth header , later we will add API key

                    //Channelfactoryhelper.UsingContract<INotificationService>(d =>
                    //{

                    //    //AsyncCallback callback = result =>
                    //    //{
                    //    //     var value = d.Endsenderrormessage(result);
                    //    //    //Debug.WriteLine("I Have Makes");
                    //    //};
                    //   // oLogEntry.id = d.Endsenderrormessage(result);
                    //    //d.senderrormessage(oLogEntry, addresstypeenum.Developer.ToString());

                    //    //TO DO get the systemaddress type from app.config 
                    //    d.senderrormessage(oLogEntry, systemaddresstypeenum.Developmentrelay.ToString());

                    //}
                    //);
                    WebChannelFactory<IAnewluvNotificationService> cfnotification = new WebChannelFactory<IAnewluvNotificationService>("webHttpBinding_IAnewluvNotificationService");
                    IAnewluvNotificationService channel = cfnotification.CreateChannel();
                    //TO DO get the systemaddress type from app.config 
                    channel.senderrormessage(oLogEntry, systemaddresstypeenum.DoNotReplyAddress.ToString());
                    cfnotification.Close();
                }
                catch (Exception ex)
                {

                    //Errors are logged in notification service n
                    //write error directly

                    // Channelfactoryhelper.Service<IAnewluvLoggingService>.Use(d =>
                    // {
                    //     //AsyncCallback callback = result =>
                    //     //{
                    //     //    var endoflogging = d.EndWriteCompleteLogEntry(result);
                    //     //    //Debug.WriteLine("I Have Makes");
                    //     //};
                    //     d.WriteCompleteLogEntry(CreateErrorLog(logseverityEnum.Warning, enviroment, ex));
                    // }
                    //);

                }
            }

        }

        static void AsyncResultCallBack(IAsyncResult ar)
        {
            Console.WriteLine("Completed execution of " + ar.AsyncState);
        }


        public log CreateErrorLog(logseverityEnum severityLevelvalue, enviromentEnum enviroment, Exception referedexception, int? profileid = null,HttpContext context = null)
        {


            //build the error object
            oLogEntry = new log();


            //build the error stuff
            try
            {
                // if (referedexception.Message != null)



                if (referedexception.Message != null)
                {
                    StackTrace stackTrace = (referedexception.StackTrace != null) ? new StackTrace(referedexception, true) : null;
                    StackFrame stackFrame = (referedexception.StackTrace != null) ? stackTrace.GetFrame(0) : null;
                    MethodBase methodBase = (stackFrame != null) ? stackFrame.GetMethod() : null;

                    // var inspectmoduletest = methodBase.Module;

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
                    // var StackTrace = referedexception.StackTrace;


                   
                    oLogEntry.message = referedexception.ToString();
                    oLogEntry.stacktrace = stackTrace != null ? stackTrace.ToString() : null;
                    oLogEntry.linenumbers = stackTrace != null ? stackFrame.GetFileLineNumber() : 0;
                    oLogEntry.methodname = stackTrace != null ? methodBase.Name : null;
                    oLogEntry.assemblyname = stackTrace != null ? GetAssemblyNameFromMethodBase(methodBase) : null;
                    oLogEntry.parentmethodname = stackTrace != null ? WhoCalledMe(referedexception) : null;
                    oLogEntry.classname = stackTrace != null ? methodBase.DeclaringType.Name : null;
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
                oLogEntry.enviroment.id = (int)enviroment;

                //replace with profile ID if we dont have it
                if (profileid != null & !object.ReferenceEquals(profileid, string.Empty))
                {
                    oLogEntry.profileid = Convert.ToString(profileid);
                }
            }
            catch (Exception ex)
            {
                var dd = ex.Message;
            }
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
        private log CreateLogEntryObject(MethodBase method, string sessionID, logseverityEnum severityLevel)
        {

            log entry = new log();

            if (method != null)
            {
                entry.assemblyname = GetAssemblyNameFromMethodBase(method);
                entry.classname = method.DeclaringType.Name;
                entry.methodname = method.Name;
            }
            entry.id = iApplicationID;
            if (entry.ipaddress == null || object.ReferenceEquals(entry.ipaddress, string.Empty))
            {
                entry.ipaddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).ToString();
            }

            entry.loggeduser = sessionID;


            WebChannelFactory<IAnewluvLoggingService> cflogging = new WebChannelFactory<IAnewluvLoggingService>("webHttpBinding_IAnewluvLoggingService");
            IAnewluvLoggingService channellogging = cflogging.CreateChannel();
            entry.id = channellogging.TranslateLogSeverity(severityLevel);
            cflogging.Close();

            //Channelfactoryhelper.Service<IAnewluvLoggingService>.Use(d =>
            //{                 
            //    entry.id = d.TranslateLogSeverity(severityLevel);               
            //});



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

            MethodBase methodBase = stackFrame != null ? stackFrame.GetMethod() : null;
            // Displays “WhatsmyName”
            if (methodBase != null)
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
