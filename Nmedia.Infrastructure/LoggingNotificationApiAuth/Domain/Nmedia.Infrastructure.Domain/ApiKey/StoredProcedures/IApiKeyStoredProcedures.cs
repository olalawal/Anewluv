
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nmedia.Infrastructure.Domain
{
    public interface IApiKeyStoredProcedures
    {
        /// <summary>
        ///  Syncs api keys to the current generated or passed in one.  Makes sure only one key is only ever active
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="keyvalue"></param>
        /// <param name="application_id"></param>
         Task  ResetApplicationtUserApiKeys(string userid, string keyvalue, string application_id);
        
        /// <summary>
        /// For logout - deactivates all user api keys
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="application_id"></param>
         Task DeactivateApplicationUserApiKeys(string userid, string application_id);

         Task UpdateApiKeyActivity(string keyvalue);

   
    }
}