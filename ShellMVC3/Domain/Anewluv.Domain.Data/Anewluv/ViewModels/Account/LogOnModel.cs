using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;


using System.Web.Security;
using System.Globalization;

using System.Text.RegularExpressions;


using Shell.MVC2.Domain.Entities.Anewluv.Validation;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    

    [DataContract]
    [Serializable]
    public class LogOnModel
    {
        [DataMember ]
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

         [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

         [DataMember]
        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        //New feilds added for openID
         [DataMember]
        public string OpenIDidentifer { get; set; }
         [DataMember]
        public string OpenIDprovidername { get; set; }
         [DataMember]
        public bool OpenIdAuthenticated { get; set; }
         [DataMember]
        public string ScreenName { get; set; }
        public int ProfileID { get; set; }
    }
}
