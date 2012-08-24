using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shell.MVC2.Infastructure;

namespace Shell.MVC2.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        [HttpPost]
        public void LogJavaScriptError(string message)
        {
            // raise(new JavaScriptErrorException(message)); }
            //throw new JavaScriptException(message);
            //manually log the javascript execption.
        }

        private void logjavascripterror(string message)
        {
    

        }

    }
}
