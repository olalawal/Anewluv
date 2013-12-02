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

using Shell.MVC2.Infrastructure.Entities.UserRepairModel;
using Shell.MVC2.Services.Contracts;


namespace LoggingLibrary
{
    public class UserRepairLogging : IDisposable
    {
        private int iApplicationID;
        private int lLogEntryID = 0;
        public  UserRepairLog  oLogEntry = null;
        // private List<LogMessage> lstMessages;
        // private List<LogValue> lstValues;

        private  IUserRepairService LoggingServiceProxy;
        ChannelFactory<IUserRepairService> factory;
        private bool disposed = false;

        /// <summary>
        /// 
        /// </summary>
        //public LogSeverity Severity
        //{
        //    get { return oLogEntry.LogLevel; }
        //    set { oLogEntry.LogLevel = value; }
        //}

       

        public UserRepairLogging()
        {
            //lstMessages = new List<LogMessage>();
            // lstValues = new List<LogValue>();
          //  iApplicationID = ApplicationID;
           // var myendpoint  = CreateLoggingServiceEndpoint();
           // Binding binding = new WSHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://localhost/LoggingService/UserRepairService.svc?wsdl");
            //UserRepairServiceClient mysClient = new UserRepairServiceClient(binding, endpointAddress);

            //mysClient.Test();
            // LogClient = new Log;//LoggerSoapClient();
            //oLogEntry = CreateLgEntryObject(null, null, logseverityEnum.Information);
            factory = new ChannelFactory<IUserRepairService>("WSHttpBinding_IUserRepairService");//(mysClient.Endpoint);
            LoggingServiceProxy = factory.CreateChannel();
            oLogEntry = new UserRepairLog();


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

        public void WriteSingleEntry(UserRepairLog log)
                                    
        {


            using (new OperationContextScope((IContextChannel)LoggingServiceProxy))
            {

                //LoggingServiceProxy.BeginWriteCompleteLogEntry(oLogEntry, null, null);
                LoggingServiceProxy.WriteCompleteLogEntry(log);
            }
            //finally EMail a template emiail to the admins 

            Clear();
        }







        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            //oLogEntry = CreateLogEntryObject(Nothing, Nothing, LogSeverity.Information)
            lLogEntryID = 0;
            //reset this
            oLogEntry = null;
            //lstValues.Clear();
            // lstMessages.Clear();
            ((IClientChannel)LoggingServiceProxy).Close();
            factory.Close();
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
