using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel ;
using Shell.MVC2.Infrastructure.Interfaces;
using System.Diagnostics;

using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace Shell.MVC2.Data
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class APIkeyRepository : IAPIkeyRepository  
    {
        //TO DO use the database context wehre API key is stored 
      
        private ApiKeyContext _context;
        const string APIKEYLIST = "APIKeyList";

        public APIkeyRepository(ApiKeyContext context)
        {
            _context = context;
        }

        public bool IsValidAPIKey(string key)
        {
            // TODO: Implement IsValidAPI Key using your repository

            Guid apiKey;

             //Convert the string into a Guid and validate it
           // not validating against a list anymore
           if (Guid.TryParse(key, out apiKey) && _context.apikeys.Any(p => p.key == apiKey))     //APIKeys.Contains(apiKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Guid> APIKeys()
        {
          
                // Get from the cache
                // Could also use AppFabric cache for scalability
                var keys = HttpContext.Current.Cache[APIKEYLIST] as List<Guid>;

                if (keys == null)
                    keys = PopulateAPIKeys();

                return keys;
            
        }

        private List<Guid> PopulateAPIKeys()
        {
            List<Guid> keyList;

            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Guid>));
            var server = HttpContext.Current.Server;
            using (FileStream fs = new FileStream(server.MapPath("~/ApiKeys/APIKeys.xml"), FileMode.Open))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
            {
                keyList = (List<Guid>)dcs.ReadObject(reader);
            }

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
