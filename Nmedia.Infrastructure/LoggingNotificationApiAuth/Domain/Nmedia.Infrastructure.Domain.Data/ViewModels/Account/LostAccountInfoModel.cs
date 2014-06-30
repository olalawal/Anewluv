using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;


using System.Web.Security;
using System.Globalization;

using System.Text.RegularExpressions;


using Anewluv.Domain.Data.Validation ;
using System;

namespace Nmedia.Infrastructure.Domain.Data.ViewModels
{




    [Serializable]
    [DataContract]
    public class LostAccountInfoModel
    {
         [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]      
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Valid Email Address is required.")] 
        public string Email { get; set; }

        //[DataMember]
        //  public List<SelectListItem> SecurityQuestions { get; set; } 

         [DataMember]
        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }

         [DataMember]
        [DisplayName("Security Answer")]
        public string SecurityAnswer { get; set; }

         [DataMember]
        public bool LostPassword { get; set; }


        
    }
    
}
