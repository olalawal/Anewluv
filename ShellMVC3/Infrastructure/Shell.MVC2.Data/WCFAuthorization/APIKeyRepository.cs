using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel ;
using Shell.MVC2.Infrastructure.Interfaces;

namespace Shell.MVC2.Data
{
    public class APIKeyRepository : IAPIkeyRepository
    {
        private ApiKeyContext _context;

        public APIKeyRepository(ApiKeyContext context)
        {
            _context = context;
        }




        public bool IsValidAPIKey(string key)
        {
            // TODO: Implement IsValidAPI Key using your repository

            Guid apiKey;


            // Convert the string into a Guid and validate it
            //not validating against a list anymore
            if (Guid.TryParse(key, out apiKey) && _context.apikeys.Any(p => p.key == apiKey))     //APIKeys.Contains(apiKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Guid> APIKeys
        {
            get
            {
                // Get from the cache
                // Could also use AppFabric cache for scalability
                var keys = HttpContext.Current.Cache[APIKEYLIST] as List<Guid>;

                if (keys == null)
                    keys = PopulateAPIKeys();

                return keys;
            }
        }

        //old way that use's XML file
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

        const string APIKEYLIST = "APIKeyList";

    }
}

