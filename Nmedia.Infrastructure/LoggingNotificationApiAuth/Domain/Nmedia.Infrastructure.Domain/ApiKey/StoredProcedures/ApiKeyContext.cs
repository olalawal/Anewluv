#region


using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace Nmedia.Infrastructure.Domain
{
    public partial class ApiKeyContext : IApiKeyStoredProcedures

            


{

    public void ResetApiKeys(string userid,string keyvalue,string application_id)
    {
        try
        {
            var unicornNames = Database.ExecuteSqlCommand("UPDATE Apikeys SET active = 'false' WHERE user_id = {0} and keyvalue <> {1} and application_id = {2} ", userid, keyvalue, application_id);
        }
        catch ( Exception ex)
            {
            
            var dd = ex.Message;
            }

    }





}

}