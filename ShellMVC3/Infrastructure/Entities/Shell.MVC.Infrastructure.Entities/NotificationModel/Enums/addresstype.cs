using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum addresstypeenum : int
    {
        [Description("Developer")][EnumMember]
        Developer = 1,
        [Description("System Admin")][EnumMember]
        SystemAdmin = 2,
        [Description("Project Lead")][EnumMember]
        ProjectLead = 3,
        [Description("QA")][EnumMember]
        QualityAsurance = 4,
        [Description("Support Admin")][EnumMember]
        SiteSupportAdmin = 5,
        [Description("Member")][EnumMember]
        SiteUser = 6,
     
    }


  
}
