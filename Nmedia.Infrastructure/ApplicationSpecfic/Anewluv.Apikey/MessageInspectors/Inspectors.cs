using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Apikey
{
   public static class Inspectors
    {

       public static ProfileModel getprofileidmodelfrombody(ref Message internalCopy)
        {
            //TO DO check if the call body has a profileid in the request body to make sure the user can access the revevant data being called for.
            //if so return use a diffeernt validation call that returns the profileID so we can match against the passed on.
            // Message msg = OperationContext.Current.RequestContext.RequestMessage.CreateBufferedCopy(Int32.MaxValue).CreateMessage();
            //if we have a body look for the profileid
            if (!internalCopy.IsEmpty)
            {
                var dd = Utilities.MessageToString(internalCopy);
                //get the profile id and map and other values as needed to the model if it exists otherwise no nothing
                if (dd != "" && dd.Contains("profileid"))
                {
                   var ProfileModel = JsonExtentionsMethods.Deserialize<ProfileModel>(dd);
                   return ProfileModel;
                }
                // msg.Close();  //kill this since we need it no more.
            }
           //Return empty model
            return new ProfileModel();        

        }



}

}
