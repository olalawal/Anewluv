
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nmedia.Infrastructure.Domain
{
    public interface IApiKeyStoredProcedures
    {

         void ResetApiKeys(string userid, string keyvalue, string application_id);

         Task UpdateApiKeyActivity(string keyvalue);

   
    }
}