using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.IO;


namespace Shell.MVC2.Infrastructure
{

    public class TemplateParser
    {

        const string  basepath = @"c:\temp\templates";

        public static string RazorDBTemplate<T>(string templatestring, ref T myobject)
        {
            dynamic config = new TemplateServiceConfiguration { Language = RazorEngine.Language.CSharp  };
            dynamic service = new RazorEngine.Templating.TemplateService(config);
            Razor.SetTemplateService(service);
            //default'model to use CustomErrorLog
            //defualt template is the custom ErrorlogModel
            string defaulttemplate = "<html><head><title>Error Message Email</title></head><body>ErrorMessage: @Model.Message</body></html>";
            dynamic template = !string.IsNullOrEmpty(templatestring) ? templatestring : defaulttemplate;
            dynamic result = Razor.Parse<object>(template, myobject);
            return result;
        }


        public static string RazorFileTemplate<T>(string filename, ref T myobject)
        {
            string templatestring ="";

            try
            {
                dynamic config = new TemplateServiceConfiguration { Language = RazorEngine.Language.CSharp, Debug = true };
                dynamic service = new RazorEngine.Templating.TemplateService(config);
                Razor.SetTemplateService(service);


                var fullpath = Path.Combine(basepath, filename);
                templatestring = File.OpenText(fullpath).ReadToEnd();





                dynamic result = Razor.Parse(templatestring, myobject);
                return result;
            }
           
           

            catch (Exception ex)
            {
                var messge = ex.Message;
                throw;

            }
        }

        public string MyGenericSub<T>(ref List<T> MyList)
        {

            return "";
        }



    }

}