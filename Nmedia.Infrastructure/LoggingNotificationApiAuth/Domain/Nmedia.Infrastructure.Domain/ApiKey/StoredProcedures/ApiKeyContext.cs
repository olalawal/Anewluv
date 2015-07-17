#region


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

#endregion

namespace Nmedia.Infrastructure.Domain
{
    public partial class ApiKeyContext : IApiKeyStoredProcedures

            


{

        public async Task ResetApplicationtUserApiKeys(string userid, string keyvalue, string application_id)
    {
        try
        {
            await Database.ExecuteSqlCommandAsync("UPDATE Apikeys SET active = 'false' WHERE user_id = {0} and keyvalue <> {1} and application_id = {2} ", userid, keyvalue, application_id);
        }
        catch ( Exception ex)
            {
            
            var dd = ex.Message;
            }

    }

    public async Task DeactivateApplicationUserApiKeys(string userid,string application_id)
    {
        try
        {
          await Database.ExecuteSqlCommandAsync("UPDATE Apikeys SET active = 'false' WHERE application_id = {2} ", userid, application_id);
        }
        catch (Exception ex)
        {

            var dd = ex.Message;
        }

    }

    public async Task UpdateApiKeyActivity(string keyvalue)
    {

        try
        {
            var lastaccesstime = DateTime.Now;
            await Database.ExecuteSqlCommandAsync("UPDATE Apikeys SET lastaccesstime = {0} WHERE  keyvalue = {1} ", lastaccesstime, keyvalue);        
        }
        catch (Exception ex)
        { 
        //TODO log
        
        }
    
    }



}

}