using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Services.Contracts;
using Shell.MVC2.Infrastructure;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Nmedia.Services.Contracts;

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
                    _photoservice = new WebChannelFactory<IPhotoService>("webHttpBinding_IPhotoService");
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
                    _geoservice = new WebChannelFactory<IGeoService>("webHttpBinding_IGeoService");
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
                    _imemberservice = new WebChannelFactory<IMemberService>("webHttpBinding_IMemberService");
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
        public static bool DisposeMemberService()
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

        private static WebChannelFactory<IMembersMapperService> _membermapperservice;
        public static IMembersMapperService MemberMapperService
        {
            get
            {
                if (_membermapperservice == null)
                {
                    _membermapperservice = new WebChannelFactory<IMembersMapperService>("webHttpBinding_IMembersMapperService");
                    IMembersMapperService channel = _membermapperservice.CreateChannel();
                    return channel;

                }
                else
                {
                    IMembersMapperService channel = _membermapperservice.CreateChannel();
                    return channel;


                }
                //return _IMemberService;
            }
        }
        public static bool DisposeMemberMapperService()
        {

            if (_membermapperservice != null)
            {
                _membermapperservice.Close();

                //IIMemberService channel = _IMemberService.CreateChannel();
                // return channel;
                return true;
            }
            return false;
            //return _IMemberService;

        }

        private static WebChannelFactory<IAuthenticationService> _authenticationservice;
        public static IAuthenticationService AuthenticationService
        {
            get
            {
                if (_authenticationservice == null)
                {
                    _authenticationservice = new WebChannelFactory<IAuthenticationService>("webHttpBinding_IAuthenticationService");
                    IAuthenticationService channel = _authenticationservice.CreateChannel();
                    return channel;

                }
                else
                {
                    IAuthenticationService channel = _authenticationservice.CreateChannel();
                    return channel;


                }
                //return _IMemberService;
            }
        }
        public static bool DisposeAuthenticationService()
        {

            if (_authenticationservice != null)
            {
                _authenticationservice.Close();

                //IIMemberService channel = _IMemberService.CreateChannel();
                // return channel;
                return true;
            }
            return false;
            //return _IMemberService;

        }

        private static WebChannelFactory<IApiKeyService> _apikeyservice;
        public static IApiKeyService ApiKeyService
        {
            get
            {
                if (_apikeyservice == null)
                {
                    _apikeyservice = new WebChannelFactory<IApiKeyService>("webHttpBinding_IApiKeyService");
                    IApiKeyService channel = _apikeyservice.CreateChannel();
                    return channel;

                }
                else
                {
                    IApiKeyService channel = _apikeyservice.CreateChannel();
                    return channel;


                }
                //return _IMemberService;
            }
        }
        public static bool DisposeApiKeyService()
        {

            if (_apikeyservice != null)
            {
                _apikeyservice.Close();

                //IIMemberService channel = _IMemberService.CreateChannel();
                // return channel;
                return true;
            }
            return false;
            //return _IMemberService;

        }

    } 
}
