
using System;
using System.Collections.Generic;

namespace Nmedia.Infrastructure.Domain
{
    public interface IApiKeyStoredProcedures
    {

         void ResetApiKeys(string userid, string keyvalue, string application_id);

   
    }
}