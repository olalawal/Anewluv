using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{


 

    /// <summary>
    /// Enums that will be converted to lookup table indexs for Message Templates (templates should all be razor with an underlying model?)
    /// </summary>
    /// 
    [DataContract]
    public enum newsenum : int
    {
        [EnumMember]
        [Description("We have updated alot on the site please log in to see the new changes !")]
        BasicNewsMessage = 1,
       


    }



}
