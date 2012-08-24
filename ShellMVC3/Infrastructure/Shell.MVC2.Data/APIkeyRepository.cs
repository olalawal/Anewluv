using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Data;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.Diagnostics;
using Shell.MVC2.Infrastructure.Interfaces;
using System.Web;

namespace Shell.MVC2.Data
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class APIkeyRepository : IAPIkeyRepository  
    {
        //TO DO use the database context wehre API key is stored 
        private CustomErrorLogContext _context;
        const string APIKEYLIST = "APIKeyList"; 

        public APIkeyRepository(CustomErrorLogContext context)
        {
            _context = context;
        }

        public  bool validateAPIkey(string key)
        {

            if (string.IsNullOrEmpty(key))
                return false;
            if (!string.IsNullOrEmpty(key))
            {
                Guid apiKey;
                Guid hardKey = new Guid("F5D14784-2D9E-4F57-A69E-50FB0551940A");
                // Convert the string into a Guid and validate it
                if (!Guid.TryParse(key, out apiKey) || !apiKey.Equals(hardKey)) //we are not validating yet just hard code one guid
                {
                    return true;
                }
              
            }
            return false;
        }



        public  List<Guid> getAPIkeys()
        {
          
                // Get from the cache
                // Could also use AppFabric cache for scalability
                var keys = HttpContext.Current.Cache[APIKEYLIST] as List<Guid>;

                if (keys == null)
                    keys = PopulateAPIKeys();

                return keys;
            
        }

        private  List<Guid> PopulateAPIKeys()
        {
            List<Guid> keyList = new List<Guid>();

         //get API keys from database 


            // Save it in the cache
            // Could be saved in AppFabric Cache for scalability across a farm
            HttpContext.Current.Cache[APIKEYLIST] = keyList;

            return keyList;
        }

        //TO DO use enum maybe
        public Guid generateAPIkey(string service)
        {
            Guid key = new Guid();
            //store to DB
            return key;

        }


    }
}
