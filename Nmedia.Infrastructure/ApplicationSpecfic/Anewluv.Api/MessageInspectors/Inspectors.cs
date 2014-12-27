using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Api
{
   public static class Inspectors
    {

       public static ProfileModel getprofileidfrombody (ref Message request)
        {

           // MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
           // Message requestCopy = buffer.CreateMessage();
            
           ////if we have a body look for the profileid
           // if (!requestCopy.IsEmpty)
           // {   var dd = new Utilities();
           //      dd.MessageToString(ref requestCopy);
           //     //get the profile id and map and other values as needed to the model if it exists otherwise no nothing
           //     if (dd != "" && dd.Contains("profileid"))
           //     {
           //         return JsonExtentionsMethods.Deserialize<ProfileModel>(dd);
           //     }

           //     // msg.Close();  //kill this since we need it no more.
           // }

            return new ProfileModel();

        

        }
}

}
