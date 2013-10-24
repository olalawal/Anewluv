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


using Anewluv.Domain.Data.Validation;

namespace Anewluv.Domain.Data.ViewModels
{




    [DataContract][Serializable]
    public class LostActivationCodeModel
    {
         [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]       
        [DisplayName("Email")]
        public string Email { get; set; }
         [DataMember]
        public bool LostActivationCode { get; set; }
    }
    
}
