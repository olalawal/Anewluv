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




    [DataContract][Serializable]
    public class LostActivationCodeModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]       
        [DisplayName("Email")]
        public string Email { get; set; }
        public bool LostActivationCode { get; set; }
    }
    
}
