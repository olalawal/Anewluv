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
using System.Web;


namespace Shell.MVC2.Infrastructure
{

    public class TemplateParser
    {

        const string basepath = @"C:\temp\Sample Code\";
        // const string basepath = @"E:\VisualStudio\GitHubShared\";
        // const string membertemplateshomepath = @"E:\VisualStudio\GitHubShared\Anewluv\emailtemplates";

       //private static  string  basepath   = HttpContext.Current.Server.MapPath("/");
 
        //Image1.ImageUrl = Request.ApplicationPath + "/images/Image1.gif";
       // Label2.Text = Image1.ImageUrl;
       private static string admintemplateshomepath = basepath + "Webclients/AnewLuvWebClient/AnewLuv/Views/AdminTemplates";
       private static string membertemplateshomepath =basepath + "Webclients/AnewLuvWebClient/AnewLuv/Views/MemberTemplates";

        //Implement this when ALL templales are in DB
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
            //admintemplateshomepath = basepath + "/AdminTemplates";
           // membertemplateshomepath = basepath + "/MemberTemplates";
            try
            {
                dynamic config = new TemplateServiceConfiguration { Language = RazorEngine.Language.CSharp, Debug = true };
                dynamic service = new RazorEngine.Templating.TemplateService(config);
                Razor.SetTemplateService(service);




                var admintemplatespath = Path.Combine(admintemplateshomepath, filename);
                var membertemplatespath = Path.Combine(membertemplateshomepath , filename);
                var sharedpath = Path.Combine(basepath , filename);
                var validpath = "";
                if (File.Exists(admintemplatespath))
                {
                    validpath = admintemplatespath;
 
                }
                else if (File.Exists(membertemplatespath))
                {

                    validpath = membertemplatespath;

                }
                else
                {
                    validpath = sharedpath;
                }
                templatestring = File.OpenText(validpath ).ReadToEnd();





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