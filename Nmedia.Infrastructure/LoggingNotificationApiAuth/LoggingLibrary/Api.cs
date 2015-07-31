using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Nmedia.Services.Contracts;

namespace LoggingLibrary
{
    public static class Api
    {

        public static System.Exception LastException = null;

        public static string ApplicationPath = "";

        public static bool Debug = false;
        //   p
        private static WebChannelFactory<ILoggingService> _loggingservice;
        //do do maybe pass auth header and API key to enure its same user
        private static ILoggingService _loggingservicechannel = null;
        public static ILoggingService loggingService
        {
            get
            {
                if (_loggingservice == null & _loggingservicechannel == null)
                {
                    _loggingservice = new WebChannelFactory<ILoggingService>("webHttpBinding_ILoggingService");
                    _loggingservicechannel = _loggingservice.CreateChannel();
                    return _loggingservicechannel;
                }
                else
                {
                    //just create a new chanel
                    // IloggingService channel = _loggingservice.CreateChannel();
                    return _loggingservicechannel;

                }
                // return _loggingservice;
            }
        }
        public static bool DisposeloggingService()
        {

            if (_loggingservice != null)
            {
                _loggingservice.Close();
                //IloggingService channel = _geoservice.CreateChannel();
                // return channel;
                return true;
            }
            return false;
            //return _geoservice;

        }



        private static WebChannelFactory<INotificationService> _notificationservice;
        //do do maybe pass auth header and API key to enure its same user
        private static INotificationService _notificationservicechannel = null;
        public static INotificationService notificationService
        {
            get
            {
                if (_notificationservice == null & _notificationservicechannel == null)
                {
                    _notificationservice = new WebChannelFactory<INotificationService>("webHttpBinding_INotificationService");
                    _notificationservicechannel = _notificationservice.CreateChannel();
                    return _notificationservicechannel;
                }
                else
                {
                    //just create a new chanel
                    // InotificationService channel = _notificationservice.CreateChannel();
                    return _notificationservicechannel;

                }
                // return _notificationservice;
            }
        }
        public static bool DisposenotificationService()
        {

            if (_notificationservice != null)
            {
                _notificationservice.Close();
                //InotificationService channel = _geoservice.CreateChannel();
                // return channel;
                return true;
            }
            return false;
            //return _geoservice;

        }

    }
}
