
namespace Dating.Server.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;


    // Implements application logic using the AnewLuvLogEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class LoggerService : LinqToEntitiesDomainService<AnewLuvLogEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ELMAH_Error' query.
        public IQueryable<ELMAH_Error> GetELMAH_Error()
        {
            return this.ObjectContext.ELMAH_Error;
        }

        public void InsertELMAH_Error(ELMAH_Error eLMAH_Error)
        {
            if ((eLMAH_Error.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eLMAH_Error, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ELMAH_Error.AddObject(eLMAH_Error);
            }
        }

        public void UpdateELMAH_Error(ELMAH_Error currentELMAH_Error)
        {
            this.ObjectContext.ELMAH_Error.AttachAsModified(currentELMAH_Error, this.ChangeSet.GetOriginal(currentELMAH_Error));
        }

        public void DeleteELMAH_Error(ELMAH_Error eLMAH_Error)
        {
            if ((eLMAH_Error.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(eLMAH_Error, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ELMAH_Error.Attach(eLMAH_Error);
                this.ObjectContext.ELMAH_Error.DeleteObject(eLMAH_Error);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'GeoDataLogs' query.
        public IQueryable<GeoDataLog> GetGeoDataLogs()
        {
            return this.ObjectContext.GeoDataLogs;
        }

        public void InsertGeoDataLog(GeoDataLog geoDataLog)
        {
            if ((geoDataLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(geoDataLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GeoDataLogs.AddObject(geoDataLog);
            }
        }

        public void UpdateGeoDataLog(GeoDataLog currentGeoDataLog)
        {
            this.ObjectContext.GeoDataLogs.AttachAsModified(currentGeoDataLog, this.ChangeSet.GetOriginal(currentGeoDataLog));
        }

        public void DeleteGeoDataLog(GeoDataLog geoDataLog)
        {
            if ((geoDataLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(geoDataLog, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GeoDataLogs.Attach(geoDataLog);
                this.ObjectContext.GeoDataLogs.DeleteObject(geoDataLog);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SpamMessagesLogs' query.
        public IQueryable<SpamMessagesLog> GetSpamMessagesLogs()
        {
            return this.ObjectContext.SpamMessagesLogs;
        }

        public void InsertSpamMessagesLog(SpamMessagesLog spamMessagesLog)
        {
            if ((spamMessagesLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(spamMessagesLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SpamMessagesLogs.AddObject(spamMessagesLog);
            }
        }

        public void UpdateSpamMessagesLog(SpamMessagesLog currentSpamMessagesLog)
        {
            this.ObjectContext.SpamMessagesLogs.AttachAsModified(currentSpamMessagesLog, this.ChangeSet.GetOriginal(currentSpamMessagesLog));
        }

        public void DeleteSpamMessagesLog(SpamMessagesLog spamMessagesLog)
        {
            if ((spamMessagesLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(spamMessagesLog, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SpamMessagesLogs.Attach(spamMessagesLog);
                this.ObjectContext.SpamMessagesLogs.DeleteObject(spamMessagesLog);
            }
        }
    }
}


