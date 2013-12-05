using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Net;
using System.ServiceModel.Web;

namespace Anewluv.Web.MemberActions
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

     

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            // Get the request headers
            //WebHeaderCollection headers = WebOperationContext.Current.IncomingRequest.Headers;


            ////HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept,apikey,Authorization");
            //  ;


            //    HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.OK;
               
            //}

            //// Allow the cross domain call
            //if (WebOperationContext.Current.IncomingRequest.Method  == "OPTIONS")
            //{
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, apikey,Authorization");

            //    // Stop ajax caching the responses on Safari/iOS
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Pragma", "no-cache");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Expires", "0");

            //    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;

            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}