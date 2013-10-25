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
    class api
    {
    
     public static System.Exception LastException = null;

	public static string ApplicationPath = "";

	public static bool Debug = false;
    private static WebChannelFactory<IPhotoService> _photoservice;

    public static  WebChannelFactory<IPhotoService> PhotoService
    {
		get {
            if (_photoservice == null)            
            {
                _photoservice = new WebChannelFactory<IPhotoService>("webHttpBinding_PhotoService");
              
			}
            return _photoservice;
		}
	}

    private static WebChannelFactory<IGeoService> _geoservice;

    public static WebChannelFactory<IGeoService> GeoService
    {
        get
        {
            if (_geoservice == null)
            {
                _geoservice = new WebChannelFactory<IGeoService>("geoservice");

            }
            return _geoservice;
        }
    }



    }
}
