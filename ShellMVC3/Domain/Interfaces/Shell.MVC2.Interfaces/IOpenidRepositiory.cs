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
         rpxprofile authinfojson(string token);      
         List<string> mappings(string primaryKey);    
         string getcontents(string xpath_expr, XPathNavigator nav);
         void map(string identifier, string primaryKey);
         void unmap(string identifier, string primaryKey);
         rpxprofile apicalljson(string methodName, Dictionary<string, string> partialQuery);
       //End of JainRan code
    }
}


// private xmlelement apicall(string methodName, Dictionary<string, string> partialQuery);

  //  public Dictionary<string, ArrayList> allmappings();

// public xmlelement authinfo(string token);