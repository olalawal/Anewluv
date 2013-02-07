using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Data;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.Diagnostics;
using Shell.MVC2.Infrastructure.Interfaces;

namespace Shell.MVC2.Data
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ErrorLoggingRepository : IErrorLoggingRepository 
    {

        private CustomErrorLogContext _context;

        public ErrorLoggingRepository(CustomErrorLogContext context)
        {
            _context = context;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public int WriteCompleteLogEntry(CustomErrorLog logEntry)
        {
            // logEntry.dtTimeStamp = DateTime.Now;

          //  using (var context = new CustomErrorLogContext())
          //  {
            try
            {
               
                _context.CustomErrorLogs.Add(logEntry);

                _context.SaveChanges();
                
                return logEntry.id;
            }
            catch (UpdateException ex)
            {
                // To avoid propagating exception messages that contain sensitive data to the client tier, 
                // calls to Add and SaveChanges should be wrapped in exception handling code.
                //this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogEntry method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
                throw new InvalidOperationException(string.Format("An exception was thrown in the WriteLogEntry method in LoggerWS: {0}{1}", ex));
            }
            catch (Exception generalexception)            {

                throw new InvalidOperationException("Failed to update the log. Try your request again.",generalexception );
            }



           // }



        }

        public int TranslateLogSeverity(LogSeverityEnum logseverityValue)
        {
            try
            {
                return Convert.ToInt32(LogSeverityUtil.TranslateLogSeverity(logseverityValue));
                return 1;
            }
            catch (Exception ex)
            {
                //handle logger error 

            }
            return 0;


        }

        /// <summary>
        /// Writes to Windows event log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="entryType">Type of the entry.</param>
        private void WriteToEventLog(string message, EventLogEntryType entryType)
        {
            try
            {
                string sourceName = "Logger";
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, message, entryType);
            }
            catch (Exception ex)
            {
                //supress.  we can't risk have an endless loop of exceptions here.
            }
        }

        //public int WriteCompleteLogEntry(CustomErrorLog logEntry)
        //{
        //    int logEntryID = 0;

        //    try
        //    {
        //        logEntryID = WriteLogEntry(logEntry);
        //        WriteLogValue(logEntryID, value);
        //        WriteLogMessage(logEntryID, message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //        this.WriteToEventLog(string.Format("An exception was thrown in the WriteCompleteLogEntry method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //    }

        //    return logEntryID;

        //}

        ///// <summary>
        ///// Writes a log value.  Inserts an entry into the LogEntryValues table.
        ///// </summary>
        ///// <param name="logEntryID">The log entry ID.</param>
        ///// <param name="name">The name of the item being written.</param>
        ///// <param name="value">The value.  This must be convertable to text using the CStr() function.</param>      
        //public void WriteValue(int logEntryID, string name, object value)
        //{
        //    if (value == null)
        //    {
        //        return;
        //    }

        //    CustomErrorLog logEntry = new CustomErrorLog();
        //    logEntry.CustomErrorLogID = logEntryID;
        //    logEntry.LoggedObject = value.ToString();

        //    using (CustomErrorLogContext context =
        //    new CustomErrorLogContext())
        //    {
        //        try
        //        {
        //            // Perform validation on the updated order before applying the changes.

        //            // The Add method examines the change tracking information 
        //            // contained in the graph of self-tracking entities to infer the set of operations
        //            // that need to be performed to reflect the changes in the database. 
        //            context.CustomErrorLogs.Add(logEntry);
        //            context.SaveChanges();

        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("A database exception was thrown in the WriteValue method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("An exception was thrown in the WriteValue method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //            throw new InvalidOperationException("Failed to update the log. Try your request again.");

        //        }
        //        finally
        //        {
        //            // MyDB.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Writes a log value.  Inserts an entry into the LogEntryValues table.
        ///// </summary>
        ///// <param name="logEntryID">The log entry CustomErrorLogID.</param>
        ///// <param name="value">The value.</param>       
        //public void WriteLogValue(int logEntryID, LogValue value)
        //{
        //    if (value == null)
        //    {
        //        return;
        //    }

        //    CustomErrorLog logEntry = new CustomErrorLog();
        //    logEntry.CustomErrorLogID = logEntryID;
        //    logEntry.LoggedObject = value.CurrentValue;
        //    logEntry.Type = value.LogValueType;
        //    //need to add a feild for the name of the serilaized object that was in error stack

        //    using (CustomErrorLogContext context =
        //     new CustomErrorLogContext())
        //    {
        //        try
        //        {
        //            // Perform validation on the updated order before applying the changes.

        //            // The Add method examines the change tracking information 
        //            // contained in the graph of self-tracking entities to infer the set of operations
        //            // that need to be performed to reflect the changes in the database. 
        //            context.CustomErrorLogs.Add(logEntry);
        //            context.SaveChanges();

        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("A database exception was thrown in the WriteLogValue method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogValue method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //            throw new InvalidOperationException("Failed to update the log. Try your request again.");

        //        }
        //        finally
        //        {

        //        }
        //    }
        //}

        ///// <summary>
        ///// Writes multiple values to the LogEntryValues table.
        ///// </summary>
        ///// <param name="logEntryID">The log entry CustomErrorLogID.</param>
        ///// <param name="values">The values.</param>       
        //public void WriteLogValues(int logEntryID, LogValue[] values)
        //{
        //    //if (values == null || values.Length == 0)
        //    //{
        //    //    return;
        //    //}


        //    //try
        //    //{
        //    //    //MyDB.CommandText = "spInsLogEntryValue";
        //    //    //MyDB.CommandType = CommandType.StoredProcedure;

        //    //    //MyDB.Parameters.Add("@logEntryId", SqlDbType.BigInt).Value = logEntryID;
        //    //    //MyDB.Parameters.Add("@valueName", SqlDbType.VarChar, 255);
        //    //    //MyDB.Parameters.Add("@valueType", SqlDbType.VarChar, 50);
        //    //    //MyDB.Parameters.Add("@value", SqlDbType.VarChar, 255);
        //    //    foreach (LogValue value in values)
        //    //    {
        //    //        Debug.WriteLine("writing log value - " + DateTime.Now.ToString("hh:mm:ss.fffffff"));

        //    //        MyDB.Parameters("@valueName").Value = value.Name;
        //    //        MyDB.Parameters("@valueType").Value = value.LogValueType;
        //    //        MyDB.Parameters("@value").Value = value.CurrentValue;

        //    //        //result = MyDB.ExecuteNonQuery(ConnectionString, "spInsLogEntryValue", logEntryID, value.Name, value.LogValueType, value.CurrentValue)
        //    //        result = MyDB.ExecuteNonQuery();
        //    //    }
        //    //}
        //    ////catch ( ex)
        //    //{
        //    //    Debug.WriteLine(ex.ToString());
        //    //    this.WriteToEventLog(string.Format("A database exception was thrown in the WriteLogValues method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine(ex.ToString());
        //    //    this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogValues method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //    //}
        //    //finally
        //    //{
        //    //  //  MyDB.Dispose();
        //    //}

        //}

        ///// <summary>
        ///// Writes a message to the LogEntryMessages table.
        ///// </summary>
        ///// <param name="logEntryID">The log entry CustomErrorLogID.</param>
        ///// <param name="message">The message.</param>
        ///// <param name="logLevel">The log level.</param>       
        //public void WriteMessage(int logEntryID, string message)
        //{
        //    if (message == null)
        //    {
        //        return;
        //    }

        //    CustomErrorLog logEntry = new CustomErrorLog();
        //    logEntry.CustomErrorLogID = logEntryID;
        //    //logEntry.Message = message;
        //    //logEntry.Type = value.LogValueType;
        //    using (CustomErrorLogContext context =
        //    new CustomErrorLogContext())
        //    {
        //        try
        //        {
        //            // Perform validation on the updated order before applying the changes.
        //            // The Add method examines the change tracking information 
        //            // contained in the graph of self-tracking entities to infer the set of operations
        //            // that need to be performed to reflect the changes in the database. 
        //            context.CustomErrorLogs.Add(logEntry);
        //            context.SaveChanges();
        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("A database exception was thrown in the WriteMessage method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("An exception was thrown in the WriteMessage method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //        }
        //        finally
        //        {
        //            //   MyDB.Dispose();
        //        }
        //    }
        //}

        //public void WriteLogMessages(int logEntryID, LogMessage[] messages)
        //{
        //    if (messages == null || messages.Length == 0)
        //    {
        //        return;
        //    }

        //    //Dim MyDB As New MtxSqlDAL
        //    // DB MyDB = new DB();
        //    // int result = 0;

        //    //  try
        //    //   {
        //    //MyDB.CommandText = "spInsLogEntryMessage";
        //    //MyDB.CommandType = CommandType.StoredProcedure;

        //    //MyDB.Parameters.Add("@logEntryId", SqlDbType.BigInt).Value = logEntryID;
        //    //MyDB.Parameters.Add("@message", SqlDbType.VarChar, 255);
        //    //MyDB.Parameters.Add("@assemblyName", SqlDbType.VarChar, 100);
        //    //MyDB.Parameters.Add("@className", SqlDbType.VarChar, 50);
        //    //MyDB.Parameters.Add("@methodName", SqlDbType.VarChar, 50);
        //    //MyDB.Parameters.Add("@serverIp", SqlDbType.VarChar, 15);
        //    //MyDB.Parameters.Add("@LogLevel", SqlDbType.TinyInt);

        //    //    foreach (LogMessage message in messages)
        //    //  {
        //    //the following block of code ensures that nulls are written rather than empty strings.
        //    //If message.Text Is Nothing OrElse message.Text Is String.Empty OrElse message.Text = "" Then
        //    //    message.Text = Nothing
        //    //End If
        //    //If message.AssemblyName Is Nothing OrElse message.AssemblyName Is String.Empty OrElse message.AssemblyName = "" Then
        //    //    message.AssemblyName = Nothing
        //    //End If
        //    //If message.ClassName Is Nothing OrElse message.ClassName Is String.Empty OrElse message.ClassName = "" Then
        //    //    message.ClassName = Nothing
        //    //End If
        //    //If message.MethodName Is Nothing OrElse message.MethodName Is String.Empty OrElse message.MethodName = "" Then
        //    //    message.MethodName = Nothing
        //    //End If
        //    //If message.ServerIP Is Nothing OrElse message.ServerIP Is String.Empty OrElse message.ServerIP = "" Then
        //    //    message.ServerIP = Nothing
        //    //End If

        //    //result = MyDB.ExecuteNonQuery(ConnectionString, "spInsLogEntryMessage", _
        //    //    logEntryID, message.Text, message.AssemblyName, message.ClassName, message.MethodName, message.ServerIP, message.LogLevel)

        //    //MyDB.Parameters("@message").Value = message.Text;
        //    //MyDB.Parameters("@assemblyName").Value = message.AssemblyName;
        //    //MyDB.Parameters("@className").Value = message.ClassName;
        //    //MyDB.Parameters("@methodName").Value = message.MethodName;
        //    //MyDB.Parameters("@serverIp").Value = message.ServerIP;
        //    //MyDB.Parameters("@LogLevel").Value = message.LogLevel;

        //    //result = MyDB.ExecuteNonQuery();
        //    //    }
        //    //}
        //    //catch (System.Data.SqlClient.SqlException ex)
        //    //{
        //    //    Debug.WriteLine(ex.ToString());
        //    //    this.WriteToEventLog(string.Format("A database exception was thrown in the WriteLogMessages method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine(ex.ToString());
        //    //    this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogMessages method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //    //}
        //    //finally
        //    //{
        //    //  //  MyDB.Dispose();
        //    //}
        //}

        ///// <summary>
        ///// Writes a message to the LogEntryMessages table.
        ///// </summary>
        ///// <param name="logEntryID">The log entry CustomErrorLogID.</param>
        ///// <param name="message">The message.</param>      
        //public void WriteLogMessage(int logEntryID, LogMessage message)
        //{
        //    if (message == null)
        //    {
        //        return;
        //    }

        //    CustomErrorLog logEntry = new CustomErrorLog();
        //    logEntry.CustomErrorLogID = logEntryID;
        //    // logEntry. = message.Text;
        //    logEntry.AssemblyName = message.AssemblyName;
        //    logEntry.ClassName = message.ClassName;
        //    logEntry.MethodName = message.MethodName;
        //    logEntry.IPAddress = message.ServerIP;

        //    //logEntry.Type = value.LogValueType;

        //    using (CustomErrorLogContext context =
        //      new CustomErrorLogContext())
        //    {
        //        try
        //        {
        //            // Perform validation on the updated order before applying the changes.
        //            // The Add method examines the change tracking information 
        //            // contained in the graph of self-tracking entities to infer the set of operations
        //            // that need to be performed to reflect the changes in the database. 
        //            context.CustomErrorLogs.Add(logEntry);
        //            context.SaveChanges();

        //        }
        //        catch (System.Data.SqlClient.SqlException ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("A database exception was thrown in the WriteLogMessage method in LoggerWS: {0}", ex.ToString()), EventLogEntryType.Error);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogMessage method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
        //        }
        //        finally
        //        {
        //            //  MyDB.Dispose();
        //        }
        //    }
        //}


    }
}
