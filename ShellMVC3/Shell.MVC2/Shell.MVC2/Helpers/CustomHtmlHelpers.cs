using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Web.Mvc;
using Shell.MVC2.Resources;
using System.Resources;
using System.Collections;
 





//this namespace is added under pages directvive in web.config
namespace Shell.MVC2.Helpers
{

    public static class UrlActionHelpers
    {

        /// <summary>

        /// Generates a fully qualified URL to an action method by using

        /// the specified action name, controller name and route values.

        /// </summary>

        /// <param name="url">The URL helper.</param>

        /// <param name="actionName">The name of the action method.</param>

        /// <param name="controllerName">The name of the controller.</param>

        /// <param name="routeValues">The route values.</param>

        /// <returns>The absolute URL.</returns>

        public static string AbsoluteAction(this UrlHelper url,

             string actionName, string controllerName, object routeValues = null)
        {

            return url.Action(actionName, controllerName, routeValues, "http");

        }


        public static string AbsoluteUrl(this UrlHelper url, string relativeOrAbsolute)
        {
            var uri = new Uri(relativeOrAbsolute, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return relativeOrAbsolute;
            }
            // At this point, we know the url is relative. 
            return VirtualPathUtility.ToAbsolute(relativeOrAbsolute);
        } 



    }
       


    /// <summary>
    /// Html helper extension for an Input file upload element
    /// </summary>
    public static class FileBoxHtmlHelperExtension
    {
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.FileBox(name, (object)null);
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return htmlHelper.FileBox(name, new RouteValueDictionary(htmlAttributes));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBox(this HtmlHelper htmlHelper, string name, IDictionary<String, Object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "file", true);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.GenerateId(name);

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.FileBoxFor<TModel, TValue>(expression, (object)null);
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return htmlHelper.FileBoxFor<TModel, TValue>(expression, new RouteValueDictionary(htmlAttributes));
        }
        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field as an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression that resolves to the name of the form field and the <see cref="System.Web.Mvc.ViewDataDictionary" /> key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>
        /// An input element that has its type attribute set to "file".
        /// </returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<String, Object> htmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.FileBox(name, htmlAttributes);
        }
    }


    public static class ImageActionLinkHelper
    {

        public static string ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName,string _class, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("class", _class);
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions);
            return link.ToString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

       public static string ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)    
       {         UrlHelper urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url; 
           TagBuilder imgTag = new TagBuilder("img");       
           imgTag.MergeAttribute("src", imgSrc); 
           imgTag.MergeAttributes((IDictionary<string, string>) imgHtmlAttributes,true); 
           string url = urlHelper.Action(actionName, controllerName, routeValues);  
           TagBuilder imglink = new TagBuilder("a");       
           imglink.MergeAttribute("href", url);         
           imglink.InnerHtml = imgTag.ToString();      
           imglink.MergeAttributes((IDictionary<string, string>)htmlAttributes, true); 
           return imglink.ToString();    
       }


        public static string ImageLink<T>( this HtmlHelper helper, Expression<Action<T>> linkUrlAction, 
        string imageUrlPath, string linkTarget, string altText ) where T : Controller 
    {            
         string linkUrl = helper.BuildUrlFromExpression( linkUrlAction ); 

    string outputUrl = string.Format( @"<a href='{0}' target='{1}'><img src='{2}' alt='{3}' style='border-style: none' /></a>", linkUrl, linkTarget, imageUrlPath, altText ); 
    return outputUrl; 
}


        public static string ImageActionLink<T>(this HtmlHelper helper, Expression<Action<T>> action, string imageUrl, string linkText, object routeValues = null, object htmlAttributes= null) where T : Controller 
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);

            ////use this as the link
            //var link2 = System.Web.Mvc.Html.LinkExtensions.ActionLink(
            //  helper,
            //  linkText,              
            // actionName,
            //   controllerName, new RouteValueDictionary(routeValues));

            //replase the linktext with the image
           ///return  link2.ToString().Replace(linkText, builder.ToString(TagRenderMode.SelfClosing));
            var link = helper.ActionLink(action, "[replaceme]");
            return link.ToString().Replace("[replaceme]", ImageExtensions.Image(helper, imageUrl, linkText,htmlAttributes ).ToString());

        }


        public static string ActionLinkImage<T>(this HtmlHelper helper,  Expression<Action<T>> action,  string imageRelativeUrl,  string alt)  where T : Controller
        {
            //string temp = ImageExtensions.Image(helper, imageRelativeUrl, alt).ToString();
            //string link = string.Format(helper.ActionLink(action,"{0}").ToString(),temp)
            //    return link;

          return String.Format(helper.ActionLink(action, "{0}").ToString(), ImageExtensions.Image(helper, imageRelativeUrl, alt));
        }

        public static string ActionLinkImage<T>(this HtmlHelper helper, Expression<Action<T>> action, string imageRelativeUrl, string alt, object htmlAttributes)        where T : Controller
        {

            return String.Format(helper.ActionLink(action, "{0}").ToString(),  ImageExtensions.Image(helper, imageRelativeUrl, alt, htmlAttributes));
        }


        public static MvcHtmlString ActionLinkWithSpan(this HtmlHelper html,string linkText,string actionName, string controllerName, object htmlAttributes)
        {

            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);


            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.SetInnerText(linkText);

            // Merge Attributes on the Tag you wish the htmlAttributes to be rendered on.  
            //linkTag.MergeAttributes(attributes);  
            spanTag.MergeAttributes(attributes);

            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);
            linkTag.Attributes.Add("href", url.Action(actionName, controllerName));
            linkTag.InnerHtml = spanTag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));

        }


        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper html, string linkText, string actionName, string controllerName, object htmlAttributes)
        {

            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);


            TagBuilder linkTag = new TagBuilder("a");
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.SetInnerText(linkText);

            // Merge Attributes on the Tag you wish the htmlAttributes to be rendered on.  
            //linkTag.MergeAttributes(attributes);  
            spanTag.MergeAttributes(attributes);

            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);
            linkTag.Attributes.Add("href", url.Action(actionName, controllerName));
            linkTag.InnerHtml = spanTag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));

        }

    }

    
    public static class ButtonActionLinkHelper
    {

        public static string ButtonActionLink<T>(this HtmlHelper helper, Expression<Action<T>> action, string text,string url,string cssClass, object routeValues = null, object htmlAttributes = null) where T : Controller
        {
           
            ////use this as the link
            //var link2 = System.Web.Mvc.Html.LinkExtensions.ActionLink(
            //  helper,
            //  linkText,              
            // actionName,
            //   controllerName, new RouteValueDictionary(routeValues));

            //replase the linktext with the image
            ///return  link2.ToString().Replace(linkText, builder.ToString(TagRenderMode.SelfClosing));
            var link = helper.ActionLink(action, "[replaceme]");
           // return link.ToString().Replace("[replaceme]", System.Web.Mvc.Html.LinkExtensions.ActionLink(helper, "test", "SendMsg", htmlAttributes).ToString());
            return link.ToString().Replace("[replaceme]", ButtonsAndLinkExtensions.SubmitButton(helper, "test", "", htmlAttributes).ToString());
           
        }


    
    }


     public static class ImageLinkHelper     {     
         public static  HtmlString  ImageLink(this HtmlHelper helper, string actionName, string imageUrl, string alternateText)        
         {             return  new HtmlString ( ImageLink(helper, actionName, imageUrl, alternateText, null, null, null));        
         }                   public static string ImageLink(this HtmlHelper helper, string actionName, string imageUrl, string alternateText, object routeValues)        
         {             return ImageLink(helper, actionName, imageUrl, alternateText, routeValues, null, null);  
      
         }          
         
         public static string ImageLink(this HtmlHelper helper, string actionName, string imageUrl, string alternateText, object routeValues, object linkHtmlAttributes, object imageHtmlAttributes) 
         {             
             
             var urlHelper = new UrlHelper(helper.ViewContext.RequestContext); 
             var url = urlHelper.Action(actionName, routeValues);  
             // Create link           
             var linkTagBuilder = new TagBuilder("a"); 
             linkTagBuilder.MergeAttribute("href", url);
             linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes)); 
             // Create image             
             var imageTagBuilder = new TagBuilder("img"); 
             imageTagBuilder.MergeAttribute("src", urlHelper.Content(imageUrl)); 
             imageTagBuilder.MergeAttribute("alt", urlHelper.Encode(alternateText)); 
             imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes)); 
             // Add image to link            
             linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing); 
             return linkTagBuilder.ToString();         
         }    
     }


    //manual way of doing things
    public static class ImageButtonHelper
    {

        //public static HtmlString ActionButton<T>(this HtmlHelper helper, Expression<Action<T>> linkUrlAction, string text, string url, string cssClass) where T : Controller
        //{
        //    //build the input URL and then replace the value

        //   String  linkUrl = helper.BuildUrlFromExpression(linkUrlAction);

        

        //    //string outputUrl = string.Format(@"<a href='{0}' target='{1}'><img src='{2}' alt='{3}' style='border-style: none' /></a>", linkUrl, linkTarget, imageUrlPath, altText); 




        // return new HtmlString ( "<input type=\"button\" class=\"" + cssClass + "\" value=\"" + text + "\" onclick=\"document.location.href = '" + linkUrl + "'\" />");
        //}

        public static HtmlString ActionButton(this HtmlHelper helper, string text, string actionName, string controllerName, object routevalues, string cssClass, object htmlAttributes = null)
        {
            //build the input URL and then replace the value

            // String linkUrl = helper.BuildUrlFromExpression(linkUrlAction);


            ///Members/SendBlock?SenderScreenName=dar1&TargetScreenName=danjord7&Page=5&returnUrl=%2FHome%2FQuickProfile%3Fpage%3D5'"

            UrlHelper urlHelper = ((Controller)helper.ViewContext.Controller).Url; 
            string url = urlHelper.Action(actionName, controllerName, routevalues);
           // TagBuilder imgTag = new TagBuilder("img");    
         
            TagBuilder input = new TagBuilder("button");       
           input.MergeAttribute("class", cssClass );
           input.MergeAttribute("type", "button");
           input.MergeAttribute("onclick", "document.location.href = '" + url);  
           input.MergeAttributes((IDictionary<string, string>)htmlAttributes, true);

        //   return  new HtmlString( input.ToString());


        //   string linkUrl = urlHelper.Action(actionName, controllerName, routevalues);
           string test = input.ToString();
            
         ////   <input type="button" class="likequickprofile" value="" onclick="document.location.href = '/Members/SendLike?SenderScreenName=dar1&TargetScreenName=SweetLou41&Page=4&returnUrl=%2FHome%2FQuickProfile%3Fpage%3D4'" />
           //   <input type="button" class="emailquickprofile" value="" onclick="document.location.href = '/MailController/SendMsg?ScreenName=SweetLou41&page=0&PathAndQuery=%2FHome%2FQuickProfile%3Fprofileid%3Dlouisw1970%2540yahoo.com'" />

            //  <input type="button" class="emailquickprofile" value="" onclick="document.location.href = '/Mail/SendMsg?ScreenName=SweetLou41'" />   
      
            return new HtmlString("<input type=\"button\" class=\"" + cssClass + "\" value=\"" + text + "\" onclick=\"document.location.href = '" + url + "'\" />");
        }

        public static HtmlString  SubmitButton(this HtmlHelper helper, string text)
        {
          return new HtmlString( "<input type=\"submit\" class=\"interestquickprofile\" value=\"" + text + "\" />");
        }

        // Extension method
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }


    }
    /// <summary>
    ///  helpers for determining the current configuration state of VS
    /// </summary>
    public static class ConfigurationHelper
    {

        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
        #if DEBUG
                    return true;
        #else 
              return false; 
        #endif
        } 

    
       public static bool IsDisconected(this HtmlHelper htmlHelper)
        {
        #if DISCONECTED
             return true;
        #else 
              return false; 
        #endif
        }


       public static bool IsRelease(this HtmlHelper htmlHelper)
       {
        #if RELEASE
           return true;
        #else 
              return false; 
        #endif
       } 
        
    }

    public static class StyleHelper
    {

        public static string BodyCssStyle(this HtmlHelper htmlHelper)
        {
          //Path = htmlHelper.ViewContext.RequestContext.HttpContext.vi
          return AppFabric.CachingFactory.CssStyleSelector.GetBodyCssByPageName(htmlHelper.ViewContext.RouteData.GetRequiredString("action"));
          
        }
    }




}


