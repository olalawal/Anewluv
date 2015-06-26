using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Data.Anewluv.ViewModels
{
   

    
    [DataContract]
    public class ActivityModel
    {
        //basic settings  
        [DataMember]
        public profileactivity activitybase { get; set; }
        [DataMember]
        public profileactivitygeodata activitygeodata { get; set; }

        public ActivityModel()
        {
            activitybase = new profileactivity();
            activitygeodata = new profileactivitygeodata();
        }

    }
}
