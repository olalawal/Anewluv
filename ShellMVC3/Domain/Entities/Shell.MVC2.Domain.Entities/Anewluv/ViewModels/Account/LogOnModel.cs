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
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        //New feilds added for openID
        public string OpenIDidentifer { get; set; }
        public string OpenIDprovidername { get; set; }
        public bool OpenIdAuthenticated { get; set; }
        public string ScreenName { get; set; }
        public string ProfileID { get; set; }
    }
}
