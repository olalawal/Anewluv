using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Interfaces
{
    interface IJainrainRepositiory
    {

     
        public rpx(string apiKey, string baseUrl);
      
        public string getapikey() ;
        public string getbaseurl() ;
        public XmlElement authinfo(string token);
     
        public rpxprofile authinfojson(string token);
      
        public List<string> mappings(string primaryKey);
      
        public Dictionary<string, ArrayList> AllMappings();
     
        private string getcontents(string xpath_expr, XPathNavigator nav);
    
        public void map(string identifier, string primaryKey);
    
        public void unmap(string identifier, string primaryKey);
      
        private XmlElement apicall(string methodName, Dictionary<string, string> partialQuery);
      
        public static void Main(string[] args);     


        public rpxprofile GetProfile(string methodName, Dictionary<string, string> partialQuery);
       
    }
}
