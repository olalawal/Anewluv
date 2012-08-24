using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Text.RegularExpressions;

using Shell.MVC2.Domain.Entities.Anewluv.Validation;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{

    //defines a container for agreagating models
    [DataContract]
    [Serializable]
    public class LogonViewModel
    {
        [DataMember]
        public LogOnModel LogOnModel { get; set; }
        [DataMember]
        public LostAccountInfoModel LostAccountInfoModel { get; set; }
        [DataMember]
        public LostActivationCodeModel LostActivationCodeModel { get; set; }
    }
}
