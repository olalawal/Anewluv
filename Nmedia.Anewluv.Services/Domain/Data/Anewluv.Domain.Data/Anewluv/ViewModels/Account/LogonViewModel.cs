using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using System.Text.RegularExpressions;

//using Anewluv.Domain.Data.Validation;

namespace Anewluv.Domain.Data.ViewModels
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
