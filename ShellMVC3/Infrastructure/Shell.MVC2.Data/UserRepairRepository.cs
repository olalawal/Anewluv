using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.UserRepairModel ;
using System.Data;
using Shell.MVC2.Infrastructure.Interfaces ;

namespace Shell.MVC2.Data
{
    public class UserRepairRepository : IUserRepairRepository 
    {

        private UserRepairLogContext _context;

         public UserRepairRepository(UserRepairLogContext context)
        {
            _context = context;
        }

        public int WriteCompleteLogEntry(UserRepairLog log)
        {

            // logEntry.dtTimeStamp = DateTime.Now;

          //  using (var context = new UserRepairLogContext())
          //  {
                try
                {
                    // Perform validation on the updated order before applying the changes.

                    // The Add method examines the change tracking information 
                    // contained in the graph of self-tracking entities to infer the set of operations
                    // that need to be performed to reflect the changes in the database. 
                    _context.UserRepairLogs.Add(log);
                    _context.SaveChanges();
                    return log.UserRepairLogID;
                }
                catch (UpdateException ex)
                {
                    // To avoid propagating exception messages that contain sensitive data to the client tier, 
                    // calls to Add and SaveChanges should be wrapped in exception handling code.
                    // this.WriteToEventLog(string.Format("An exception was thrown in the WriteLogEntry method in LoggerWS: {0}{1}", ex.ToString(), ex.StackTrace), EventLogEntryType.Error);
                    // throw new InvalidOperationException("Failed to update the log. Try your request again.");
                }
                return 0;
            }
      //  }
    }
}
