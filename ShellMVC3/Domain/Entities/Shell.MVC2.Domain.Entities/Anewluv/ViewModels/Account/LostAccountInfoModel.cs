using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;


using System.Web.Security;
using System.Globalization;

using System.Text.RegularExpressions;


using Shell.MVC2.Domain.Entities.Anewluv.Validation ;
using System;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{




    [Serializable]
    [DataContract]
    public class LostAccountInfoModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]      
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Valid Email Address is required.")] 
        public string Email { get; set; }

        //[DataMember]
        //  public List<SelectListItem> SecurityQuestions { get; set; } 


        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }


        [DisplayName("Security Answer")]
        public string SecurityAnswer { get; set; }

        public bool LostPassword { get; set; }


        
    }
    
}
