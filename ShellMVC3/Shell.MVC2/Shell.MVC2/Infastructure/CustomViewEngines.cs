using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shell.MVC2.Helpers
{
    /// <summary>
    ///  Added locations for my partial views 
    ///  3-30-2012
    /// </summary>
    public class MyViewEngineWithPartialPaths : RazorViewEngine
    {
        private static string[] LocalViewFormats = new[] {
            
        "~/Views/{1}/Partials/{0}.cshtml", 
        "~/Views/Shared/Partials/{0}.cshtml" ,
        "~/Views/Shared/AddSense/{0}.cshtml" 
    
        
  };







        public MyViewEngineWithPartialPaths()
            : base()
        {
            base.ViewLocationFormats = base.ViewLocationFormats.Concat(new[] {
            
        "~/Views/{1}/Partials/{0}.cshtml", 
        "~/Views/Shared/Partials/{0}.cshtml" ,
        "~/Views/Shared/AddSense/{0}.cshtml" 
  }).ToArray();
            base.PartialViewLocationFormats = base.ViewLocationFormats;
            base.MasterLocationFormats = base.ViewLocationFormats;
           
            //   ViewLocationFormats = LocalViewFormats.Union(ViewLocationFormats).ToArray();

        }
    }
}