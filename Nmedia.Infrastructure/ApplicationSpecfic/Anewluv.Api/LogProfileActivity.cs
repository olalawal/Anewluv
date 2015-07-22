using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Utils;
using Nmedia.Infrastructure;

namespace Anewluv.Api
{
    public class AnewLuvLogging
    {
        

        public static ActivityModel CreateActivity(int? profileid,Guid? apikey, int activitytypeid, OperationContext context)
        {
            var actvitymodel = new ActivityModel();
            try
            {
                //log the activity since we have a profileID that is valid 
                //TO DO find a way to get the geo data and the activity type from the URL probbaly dictorronay or class to do that.


                var activitybase = new profileactivity
                {  
                    
                    profile_id = profileid, 
                    creationdate = DateTime.Now,
                    routeurl = context != null ? context.IncomingMessageHeaders.To.AbsolutePath : null,
                    sessionid = context != null ? context.SessionId : null,
                    ipaddress = context != null ? WCFContextParser.GetRequestIP(context) : null,
                    useragent = context != null ? WCFContextParser.GetUserAgent(context) : null,
                    //TO do have an index of enums that parses the acity type by url
                    activitytype_id = activitytypeid,
                    apikey = apikey ==null && context != null ? WCFContextParser.GetAPIKey(context) : apikey
                };

                //handle the geo data stuff

                //TO DO get the IP address and use to get geo data going forward.
                //var activitygeodata = new profileactivitygeodata
                //{
                //    city = ProfileModel.city,
                //    countryname = ProfileModel.Countryname,
                //    lattitude = ProfileModel.lattitude.GetValueOrDefault(),
                //    longitude = ProfileModel.longitude.GetValueOrDefault(),
                //    regionname = ProfileModel.stateprovince,
                //    creationdate = activitybase.creationdate,

                //};


                actvitymodel.activitybase = activitybase;
                //actvitymodel.activitygeodata = activitygeodata;

             

                //return true;
            }
            catch (Exception ex)
            {
                //do nothing in this case since we dont want to affect authentication based on activity logging. 
                //errors should be logged downstream as well so just log.
                // new Logging(applicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null, true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                //do nothing for now
            }

            return actvitymodel;

            //return false;
        }

        public static void LogProfileActivities(List<ActivityModel> activities)
        {
            try
            {




                AsyncCalls.addprofileactivities(activities).DoNotAwait();

                //return true;
            }
            catch (Exception ex)
            {
                //do nothing in this case since we dont want to affect authentication based on activity logging. 
                //errors should be logged downstream as well so just log.
                // new Logging(applicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null, true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                //do nothing for now
            }

            //return false;
        }
    }
}
