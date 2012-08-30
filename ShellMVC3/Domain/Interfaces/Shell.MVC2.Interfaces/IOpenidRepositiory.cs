using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.XPath;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Interfaces
{
    interface IOpenidRepositiory
    {
        //Jain Rain OPEN ID auth code 
        public rpxprofile authinfojson(string token);      
        public List<string> mappings(string primaryKey);    
        private string getcontents(string xpath_expr, XPathNavigator nav);
        public void map(string identifier, string primaryKey);
        public void unmap(string identifier, string primaryKey);
        public rpxprofile apicalljson(string methodName, Dictionary<string, string> partialQuery);
       //End of JainRan code
    }
}


// private xmlelement apicall(string methodName, Dictionary<string, string> partialQuery);

  //  public Dictionary<string, ArrayList> allmappings();

// public xmlelement authinfo(string token);