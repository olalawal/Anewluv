using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Infrastructure;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Anewluv.Lib
{
    public static class Api
    {

        public static System.Exception LastException = null;

        public static string ApplicationPath = "";

        public static bool Debug = false;
        //   p
        private static WebChannelFactory<IPhotoService> _photoservice;
        //do do maybe pass auth header and API key to enure its same user
        public static IPhotoService PhotoService
        {
            get
            {
                if (_photoservice == null)
                {
                    _photoservice = new WebChannelFactory<IPhotoService>("webHttpBinding_PhotoService");
                    IPhotoService channel = _photoservice.CreateChannel();
                    return channel;
                }
                else
                {
                    //just create a new chanel
                    IPhotoService channel = _photoservice.CreateChannel();
                    return channel;

                }
                // return _photoservice;
            }
        }
        public static bool DisposePhotoService()
        {
           
                if (_photoservice != null)
                {
                    _photoservice.Close();
                    //IPhotoService channel = _geoservice.CreateChannel();
                    // return channel;
                    return true;
                }
                return false;
                //return _geoservice;
            
        }

        private static WebChannelFactory<IGeoService> _geoservice;
        public static IGeoService GeoService
        {
            get
            {
                if (_geoservice == null)
                {
                    _geoservice = new WebChannelFactory<IGeoService>("geoservice");
                    IGeoService channel = _geoservice.CreateChannel();
                    return channel;

                }
                else
                {
                    IGeoService channel = _geoservice.CreateChannel();
                    return channel;


                }
                //return _geoservice;
            }
        }
      
        public static bool DisposeGeoService()
        {
         
                if (_geoservice != null)
                {
                    _geoservice.Close();
                    //IGeoService channel = _geoservice.CreateChannel();
                    // return channel;
                    return true;
                }
                return false;
                //return _geoservice;
            
        }
      
       

        private static WebChannelFactory<IMemberService> _imemberservice;
        public static IMemberService MemberService
        {
            get
            {
                if (_imemberservice == null)
                {
                    _imemberservice = new WebChannelFactory<IMemberService>("IMemberService");
                    IMemberService channel = _imemberservice.CreateChannel();
                    return channel;

                }
                else
                {
                    IMemberService channel = _imemberservice.CreateChannel();
                    return channel;


                }
                //return _IMemberService;
            }
        }
        public static bool DisposeIMemberService()
        {
            
                if (_imemberservice != null)
                {
                    _imemberservice.Close();
                    
                    //IIMemberService channel = _IMemberService.CreateChannel();
                    // return channel;
                    return true;
                }
                return false;
                //return _IMemberService;
            
        }

    } 
}
