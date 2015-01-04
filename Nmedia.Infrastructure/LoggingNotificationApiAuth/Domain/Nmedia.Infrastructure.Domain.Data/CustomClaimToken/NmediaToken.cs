using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nmedia.Infrastructure.Domain.Data.CustomClaimToken
{
    /// <summary>
    /// Virtual token stored by the client that holds custom user data along with the valid api key that is reused for calls
    /// after validation.  Should be expired after no activity on the application after 30 miniute to an hour.  At that point
    /// user should be red-recteted to login and need another token on the client since previous API key should be inactive
    /// </summary>
    /// 
    [DataContract]
    public class NmediaToken
    {
        [DataMember]
        public int id { get; set; }  //standerd id of the application that identifes the user
        [DataMember]
        public Guid Apikey { get; set; }
        [DataMember]
        public DateTime? timestamp { get; set; }     

    }
}
