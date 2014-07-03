using Anewluv.Domain.Data.ViewModels;
using Anewluv.Services.Contracts;
using Nmedia.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.XPath;

namespace Anewluv.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class OpenidService : IOpenidService
    {

        private string apiKey;
        private string baseUrl;
        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        // private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public OpenidService(IUnitOfWork unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

            //promotionrepository = _promotionrepository;
            _unitOfWork = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }


       // public string getapikey() { return apiKey; }
       // public string getbaseurl() { return baseUrl; }
        public rpxprofile authinfojson(string token)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("token", token);
            return apicalljson("auth_info", query);

        }
        public string getcontents(string xpath_expr, XPathNavigator nav)
        {
            XPathNodeIterator rk_nodes = (XPathNodeIterator)nav.Evaluate(xpath_expr);
            while (rk_nodes.MoveNext())
            {
                return rk_nodes.Current.ToString();
            }
            return null;
        }
        private void map(string identifier, string primaryKey)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("identifier", identifier);
            query.Add("primaryKey", primaryKey);
            apicalljson("map", query);
        }
        public rpxprofile apicalljson(string methodName, Dictionary<string, string> partialQuery)
        {

            Dictionary<string, string> query = new Dictionary<string, string>(partialQuery);
            query.Add("format", "");
            query.Add("apiKey", apiKey);
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> e in query)
            {
                if (sb.Length > 0)
                {
                    sb.Append('&');
                }
                sb.Append(System.Web.HttpUtility.UrlEncode(e.Key, Encoding.UTF8));
                sb.Append('=');
                sb.Append(HttpUtility.UrlEncode(e.Value, Encoding.UTF8));
            }
            string data = sb.ToString();
            // Fetch authentication info from RPX 
            Uri url = new Uri(baseUrl + "/api/v2/" + methodName);
            // string data = "apiKey=" + apiKey + "&token=" + token;



            // Auth_info request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            requestWriter.Write(data);
            requestWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            TextReader responseReader = new StreamReader(response.GetResponseStream());
            string responseString = responseReader.ReadToEnd();
            responseReader.Close();

            // De-serialize JSON 
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            rpxauthinfo authInfo = serializer.Deserialize<rpxauthinfo>(responseString);

            // Ok? 
            if (authInfo.Stat != "ok")
            {
                throw new rpxexception("RPX login failed");
            }

            return authInfo.Profile;
        } 

    }
}
