using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Services.Contracts;
using Nmedia.Infrastructure;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Nmedia.Services.Contracts;

namespace Anewluv.Api
{
    public static class Api
    {

        public static System.Exception LastException = null;

        public static string ApplicationPath = "";

        public static bool Debug = false;
        //   p
        private static WebChannelFactory<IPhotoService> _photoservice;
        //do do maybe pass auth header and API key to enure its same user
        private static IPhotoService _photoservicechannel = null;
        public static IPhotoService PhotoService
        {
            get
            {
                if (_photoservice == null & _photoservicechannel == null)
                {
                    _photoservice = new WebChannelFactory<IPhotoService>("webHttpBinding_IPhotoService");
                   _photoservicechannel = _photoservice.CreateChannel();
                    return _photoservicechannel;
                }
                else
                {
                    //just create a new chanel
                   // IPhotoService channel = _photoservice.CreateChannel();
                    return _photoservicechannel;

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
        private static IGeoService _geoservicechannel = null;
        public static IGeoService GeoService
        {
            get
            {
                if (_geoservice == null)
                {
                    _geoservice = new WebChannelFactory<IGeoService>("webHttpBinding_IGeoService");
                    _geoservicechannel = _geoservice.CreateChannel();
                    return _geoservicechannel;

                }
                else
                {
                    //IGeoService channel = _geoservice.CreateChannel();
                    return _geoservicechannel;


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
        private static IMemberService _memberservicechannel = null;
        public static IMemberService MemberService
        {
            get
            {
                if (_imemberservice == null || _imemberservice.State == CommunicationState.Closed || _memberservicechannel == null)
                {
                    _imemberservice = new WebChannelFactory<IMemberService>("webHttpBinding_IMemberService");
                    _memberservicechannel = _imemberservice.CreateChannel();
                    return _memberservicechannel;

                }
                else
                {
                   // IMemberService channel = _imemberservice.CreateChannel();
                    return _memberservicechannel;


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
        private static IMembersMapperService _membermapperservicehannel = null;
        public static IMembersMapperService MemberMapperService
        {
            get
            {
                if (_membermapperservice == null)
                {
                    _membermapperservice = new WebChannelFactory<IMembersMapperService>("webHttpBinding_IMembersMapperService");
                    _membermapperservicehannel = _membermapperservice.CreateChannel();
                    return _membermapperservicehannel;

                }
                else
                {
                    //IMembersMapperService channel = _membermapperservice.CreateChannel();
                    return _membermapperservicehannel;


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
        private static IAuthenticationService _authenticationchannel = null;
        public static IAuthenticationService AuthenticationService
        {
            get
            {
                if (_authenticationservice == null || _authenticationservice.State == CommunicationState.Closed || _authenticationchannel == null)
                {
                    _authenticationservice = new WebChannelFactory<IAuthenticationService>("webHttpBinding_IAuthenticationService");
                    _authenticationchannel = _authenticationservice.CreateChannel();
                    return _authenticationchannel;

                }
                else
                {
                    //IAuthenticationService channel = _authenticationservice.CreateChannel();
                    return _authenticationchannel;


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
        private static IApiKeyService _apikeychannel=null;
        public static IApiKeyService ApiKeyService
        {
            get
            {
                if (_apikeyservice ==null || _apikeyservice.State ==  CommunicationState.Closed || _apikeychannel == null)
                {
                    _apikeyservice = new WebChannelFactory<IApiKeyService>("webHttpBinding_IApiKeyService");
                    _apikeychannel = _apikeyservice.CreateChannel();
                    return _apikeychannel;

                }
                else
                {
                   // IApiKeyService channel = _apikeyservice.CreateChannel();
                    return _apikeychannel;


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
