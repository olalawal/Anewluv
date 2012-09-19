using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;
using System.IO;

using System.Web.Routing;
using System.Web.Security;
using Shell.MVC2.Models;



using MvcContrib.Filters;
using MvcContrib;

using Shell.MVC2.AppFabric;
using System.Net;



namespace Shell.MVC2.Controllers
{
    public partial class ImagesController : Controller
    {
        //
        // GET: /Images/


        /// <summary>
        ///  Move these to a somewhere else i.e image controller for image and the get function into a repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //Public Function GetImage(ByVal id As Integer) As ActionResult
        //    Dim imageData As Byte() = ReturnImage(id)
        //    'instead of what augi wrote using the binarystreamresult this was the closest thing i found so i am assuming that this is what it evolved into 
        //    Return New FileStreamResult(New System.IO.MemoryStream(imageData), "image/jpeg")
        //End Function

        //public virtual ActionResult GetGalleryImageByProfileId(string id)
        //{
        //    //strprofileID = "ola_lawal@yahoo.com";
        //    var datingService = new DatingService().Initialize();

        //    byte[] imageData = datingService.GetGalleryPhotoByProfileID(id);
        //    //instead of what augi wrote using the binarystreamresult this was the closest thing i found so i am assuming that this is what it evolved into 
        //    if (imageData != null) return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
        //    else
        //    {
        //        //get the members gender so we know what temp image to display
        //        string imageplaceholdername = (datingService.getgenderbyscreenname(id) == "Male") ? "yourPrivacy.jpg" : "female Profile Replacement.png";
        //        //string pathToFile = @"C:\Documents and Settings\some_path.jpg";
        //        // AppDomain.CurrentDomain.a
        //        var path = Path.Combine(("~/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
        //        return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        //        // return null;
        //    }
        //    ;
        //}


        public virtual ActionResult GetGalleryImageByPhotoId(Guid id)
        {

            string imageplaceholdername = "";
            string pathToFile = "";
            byte[] imageData;
#if DISCONECTED
           
            //get the members gender so we know what temp image to display
            imageplaceholdername = "female Profile Replacement.png";

            pathToFile = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Content/images/"), imageplaceholdername);
            // AppDomain.CurrentDomain.a
            //  var path = Path.Combine( Server.MapPath("/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
            imageData= Extensions.FileToByteArray(pathToFile);

            return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/png");

#endif


            //strprofileID = "ola_lawal@yahoo.com";
            var datingService = new DatingService().Initialize();

             imageData = datingService.GetGalleryPhotoByPhotoID(id);
            //instead of what augi wrote using the binarystreamresult this was the closest thing i found so i am assuming that this is what it evolved into 
            if (imageData != null) return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
            else
                //we need a image that says -- your image here!!!
                return null;

            ;
        }



        public virtual ActionResult GetGalleryImageByScreenName(string id)
        {
              string imageplaceholdername="";
              string pathToFile = "";
              byte[] imageData;
            //for disconeced clients
            //3-28-2012 updated to allow us to at least see an image
#if DISCONECTED
            
            //get the members gender so we know what temp image to display
            imageplaceholdername = "female Profile Replacement.png";

            pathToFile = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Content/images/"), imageplaceholdername);
            // AppDomain.CurrentDomain.a
            //  var path = Path.Combine( Server.MapPath("/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
            imageData = Extensions.FileToByteArray(pathToFile);

            return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/png");

#endif


            //strprofileID = "ola_lawal@yahoo.com";
            var datingService = new DatingService().Initialize();





            imageData = datingService.GetGalleryPhotoByScreenName(id);
            //instead of what augi wrote using the binarystreamresult this was the closest thing i found so i am assuming that this is what it evolved into 
           
            if (imageData != null) return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
            else
            {
               //get the members gender so we know what temp image to display
                 imageplaceholdername = (datingService.getgenderbyscreenname(id) == "Male") ? "male profile replacement.png" : "female Profile Replacement.png";

                pathToFile = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Content/images/"), imageplaceholdername);
               // AppDomain.CurrentDomain.a
              //  var path = Path.Combine( Server.MapPath("/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
                imageData = Extensions.FileToByteArray(pathToFile);

                return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/png");

               // return new FileStreamResult(new FileStream(pathToFile , FileMode.Open), "image/jpg");
                // return null;
            }

            
        }

        // we should me moving more to using this to find the gallery since it removes spaces from screen name
        public virtual ActionResult GetGalleryImageByNormalizedScreenName(string id)
        {
            string imageplaceholdername = "";
            string pathToFile = "";
            byte[] imageData;
            //for disconeced clients
            //3-28-2012 updated to allow us to at least see an image
#if DISCONECTED

            //get the members gender so we know what temp image to display
            imageplaceholdername = "female Profile Replacement.png";

            pathToFile = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Content/images/"), imageplaceholdername);
            // AppDomain.CurrentDomain.a
            //  var path = Path.Combine( Server.MapPath("/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
            imageData = Extensions.FileToByteArray(pathToFile);

            return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/png");

#else


            //strprofileID = "ola_lawal@yahoo.com";
            var datingService = new DatingService().Initialize();





            imageData = datingService.GetGalleryPhotoByScreenName(id);
            //instead of what augi wrote using the binarystreamresult this was the closest thing i found so i am assuming that this is what it evolved into 

            if (imageData != null) return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
            else
            {
                //get the members gender so we know what temp image to display
                imageplaceholdername = (datingService.getgenderbyscreenname(id) == "Male") ? "male profile replacement.png" : "female Profile Replacement.png";

                pathToFile = Path.Combine(HttpRuntime.AppDomainAppPath + ("/Content/images/"), imageplaceholdername);
                // AppDomain.CurrentDomain.a
                //  var path = Path.Combine( Server.MapPath("/Content/images/"), imageplaceholdername);  // string.Concat("/content/images/female Profile Replacement.png");
                imageData = Extensions.FileToByteArray(pathToFile);

                return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/png");

                // return new FileStreamResult(new FileStream(pathToFile , FileMode.Open), "image/jpg");
                // return null;
            }
#endif

        }


        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult Save(List<HttpPostedFileBase> attachments)
        {
            //get current profileID from cache
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
                CachingFactory.GetProfileIDBySessionId(this.HttpContext);
            var membersmodel = new MembersViewModel();
            //guests cannot upload photos
            membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {


          
                if (file.ContentLength <= 0) continue; //Skip unused file controls.

                Dictionary<string, string> versions = new Dictionary<string, string>();
                //Define the versions to generate
               // versions.Add("_thumb", "width=94;height=95;autorotate=true;crop=auto;carve=true;format=jpg"); //Crop to square thumbnail
                //versions.Add("_medium", "maxwidth=400;maxheight=400;autorotate=true;carve=true;format=jpg;"); //Fit inside 400x400 area, jpeg
                //versions.Add("_large", "maxwidth=1900;maxheight=1900;autorotate=true format=jpg"); //Fit inside 1900x1200 area


           
                Stream tempstream  =  null;  //stream to store the resized image to to save file space
               // The resizing settings can specify any of 30 commands.. See http://imageresizing.net for details.
              //  Destination paths can have variables like <guid> and <ext>, or 
              //  even a santizied version of the original filename, like <filename:A-Za-z0-9>
                ImageResizer.ImageJob i = new ImageResizer.ImageJob(file, "~/uploads/<guid>.<ext>", new ImageResizer.ResizeSettings(
                                       "width=2000;height=2000;format=jpg;mode=max"));
                ImageResizer.ImageJob i = new ImageResizer.ImageJob(tempImage, "~/uploads/<guid>.<ext>", new ImageResizer.ResizeSettings(
                                        "width=2000;height=2000;format=jpg;mode=max;"));
                i.CreateParentDirectory = true; //Auto-create the uploads directory.                
                i.Build();        
    

                ImageResizer.ImageJob ii = new ImageResizer.ImageJob(tempImage, "~/uploads/<guid>.<ext>", new ImageResizer.ResizeSettings(
                                     "width=94;height=95;format=jpg;mode=max;carve=true;crop=auto"));
                i.CreateParentDirectory = true; //Auto-create the uploads directory.  
                i.Build();


                Generate each version
                foreach (string suffix in versions.Keys)
                {
                    //Generate a filename (GUIDs are best).
                    string fileNameTest = "~/uploads/<guid>" + suffix + ".<ext>";

                    //Let the image builder add the correct extension based on the output file type
                  //  fileName = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                   ImageResizer.ImageJob i =  new ImageResizer.ImageJob(tempImage, fileNameTest, new ImageResizer.ResizeSettings(
                                     versions[suffix]));
                    i.CreateParentDirectory = true; //Auto-create the uploads directory.                
                    i.Build();        
                }


                 Some browsers send file names with full path. This needs to be stripped.
              
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                var fileNametest = i.FinalPath;
                byte[] imagetest2 = new byte[i.ToString];

               
                


                //add the files to a temporary photo model and store it in session for now?
                var photo = new Photo();
                
                //check if there is an existing members view model in memory
                //if there is not create one and add the photos to the model             

                // byte[] fileContent = new byte[file.ContentLength];
                var fileName = Path.GetFileName(file.FileName);
                byte[] tempImage = new byte[file.ContentLength];
                file.InputStream.Read(tempImage, 0, file.ContentLength);
                photo.ImageCaption = fileName.ToString();
                //photo.ImageUploaded = file;
                photo.ActualImage = tempImage;
            
              
                //make sure photos is not empty
                if (membersmodel.MyPhotos == null)
                { //add new photo model to members model
                    var photolist = new List<Photo>();
                    membersmodel.MyPhotos = photolist;
                }
             
                
                //stiore the photo in the model in this version not into server yet and store that model into session I guess
                membersmodel.MyPhotos.Add(photo);

                //update the model in session, maybe latter just have it upload on the fly

                //Conditianl update i.e add to corrent Cache gues ot member
                //11-1-2001 removed guest stuff since guests cannot upload photos doh !!
                membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID) : null; //      CachingFactory.MembersViewModelHelper.UpdateGuestData(membersmodel, this.HttpContext);

             
               
              //byte[] fileContent = new byte[hpf.ContentLength];
              // hpf.InputStream.Read(fileContent, 0, hpf.ContentLength);
             // photomodel.AddPhoto(hpf, model.PhotoModel.ProfileID);
                //get other values from form         
                //Save file here


                // The files are not actually saved in this demo
                // file.SaveAs(physicalPath);
            }
            


            // Return an empty string to signify success
            return Content("");
        }


        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                // TODO: Verify user permissions

                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    // System.IO.File.Delete(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }




        

      




    }
}
