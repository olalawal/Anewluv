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
    // [RegistrationCountryCityPostalCodeIsValid("CountryName", "City", "ZipOrPostalCode", "PostalCodeStatus")]  
    public class RegisterModel
    {
        public string EarliestDateFrom = DateTime.Now.AddYears(-18).ToString();
        public string CurrentDate = DateTime.Now.ToString();


        [DataMember]
        [Required]
        [ValidateUsername]
        [ValidateUserNameHasNoSpaces]
        [DisplayName("User Name")]
        [StringLength(15, ErrorMessage = "User Name must be 15 characters or less")]
        public string UserName { get; set; }

        [DataMember]
        [Required]
        [ValidateScreenname]
        [StringLength(10, ErrorMessage = "Screen Name must be 10 characters or less")]
        [DisplayName("Screen Name")]
        public string ScreenName { get; set; }

        [DataMember]
        [Required]
        // [EmailAddress(ErrorMessage = "Valid Email Address is required.")]
        [StringLength(200, ErrorMessage = "Email must be 200 characters or less.")]
        [DisplayName("Email address")]
        [EmailDoesNotExistAttribute]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        //[Compare("Email", ErrorMessage = "The Email Adresses do not match.")]
        [DisplayName("Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [ValidatePasswordHasNoSpaces]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters", MinimumLength = 7)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        //[Compare("Password", ErrorMessage = "The Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [DataMember]
        public string openidIdentifer { get; set; }

        [DataMember]
        public string openidProvider { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterModel), "ValidatebirthdateDate")]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public List<string> Genders { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DataMember]
        public List<string> Countries { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Country")]
        public string Country { get; set; }


        public bool PostalCodeStatus { get; set; }

        [DataMember]
        public List<string> Cities { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string City { get; set; }

        [DataMember]
        [DataType(DataType.Text)]
        public string Stateprovince { get; set; }

        [DataMember]
        public double? longitude { get; set; }

        [DataMember]
        public double? lattitude { get; set; }



        [DataMember]
        public List<string> ZipOrPostalCodes { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("Zip/PostalCode")]
        //[RequiredPostalCodeIf("PostalCodeStatus", Comparison.IsEqualTo, true)]
        public string ZipOrPostalCode { get; set; }


        // public SecurityQuestion SecurityQuestion { get; set; }
        [DataMember]
        public List<string> SecurityQuestions { get; set; }


        //[DisplayName("Security Question")]
        //public string SecurityQuestion { get; set; }


        //[DisplayName("Security Answer")]
        //public string SecurityAnswer { get; set; }

        //photo model stuff 
        public PhotoEditModel RegistrationPhotos { get; set; }

        //add temp storage for activation code too i guess
        public string ActivationCode { get; set; }

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
