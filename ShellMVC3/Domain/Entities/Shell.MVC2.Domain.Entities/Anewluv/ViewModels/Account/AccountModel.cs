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


    //Defines a model for changing basic account settings except for email 
    //Defines a model for changing basic account settings except for email 

    //[AccountModificationCountryCityPostalCodeIsValid("CountryName", "City", "ZipOrPostalCode", ErrorMessage = "Invalid Postal or Zip Code")]
    [DataContract]
    [Serializable]
    public class AccountModel
    {
        public string EarliestDateFrom = DateTime.Now.AddYears(-18).ToString();
        public string CurrentDate = DateTime.Now.ToString();
           
        [DataMember]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters", MinimumLength = 6)]
        [DisplayName("New Password")]
        public string Password { get; set; }

          [DataMember]
        [DataType(DataType.Password)]
        [DisplayName("Confirm New password")]
        // [Compare("Password", ErrorMessage = "The Passwords do not match.")]
        public string ConfirmPassword { get; set; }

          [DataMember]
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterModel), "ValidatebirthdateDate")]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }

        //public SelectList Genders { get; set; }

        //public IEnumerable<SelectListItem> Genders { get; set; }

          [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Gender")]
        public string Gender { get; set; }


        //public SelectList Countries { get; set; }

        //public List<SelectListItem> Countries { get; set; }

          [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Country")]
        public string Country { get; set; }

          [DataMember]
        public bool PostalCodeStatus { get; set; }


        //public List<SelectListItem> Cities { get; set; }

          [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string City { get; set; }


        // public List<SelectListItem> ZipOrPostalCodes { get; set; }

          [DataMember]
        [DataType(DataType.Text)]
        [DisplayName("Zip/PostalCode")]
        //[RequiredPostalCodeIf("PostalCodeStatus", Comparison.IsEqualTo, true)]
        public string ZipOrPostalCode { get; set; }


        //// public SecurityQuestion SecurityQuestion { get; set; }
        ////public SelectList SecurityQuestions { get; set; }
        //  [DataMember]
        //public List<SelectListItem> SecurityQuestions { get; set; }

          [DataMember]
       // [Required]
        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }

          [DataMember]
       // [Required]
        [DisplayName("Security Answer")]
        public string SecurityAnswer { get; set; }

        //No need to photo model stuff

        public static ValidationResult ValidatebirthdateDate(DateTime birthdateDateToValidate)
        {

            //if (birthdateDateToValidate.Date > DateTime.Today) 
            //{ 
            //   return new ValidationResult("Your must be at least 18 years old to Register"); 

            //} 
            if (birthdateDateToValidate.Date > DateTime.Today.AddYears(-18))
            {
                return new ValidationResult("You must be at least 18 years old");
            }
            return ValidationResult.Success;
        }






    }
}
