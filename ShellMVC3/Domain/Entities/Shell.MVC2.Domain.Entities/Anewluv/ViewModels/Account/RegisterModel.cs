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
    public class registermodel
    {
        public string EarliestDateFrom = DateTime.Now.AddYears(-18).ToString();
        public string CurrentDate = DateTime.Now.ToString();


        [DataMember]
        [Required]
        [ValidateUsername]
        [ValidateUserNameHasNoSpaces]
        [DisplayName("User Name")]
        [StringLength(15, ErrorMessage = "User Name must be 15 characters or less")]
        public string username { get; set; }

        [DataMember]
        [Required]
        [ValidateScreenname]
        [StringLength(10, ErrorMessage = "Screen Name must be 10 characters or less")]
        [DisplayName("Screen Name")]
        public string screenname { get; set; }

        [DataMember]
        [Required]
        // [EmailAddress(ErrorMessage = "Valid Email Address is required.")]
        [StringLength(200, ErrorMessage = "Email must be 200 characters or less.")]
        [DisplayName("Email address")]
        [EmailDoesNotExistAttribute]
        public string emailaddress { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]
        //[Compare("Email", ErrorMessage = "The Email Adresses do not match.")]
        [DisplayName("Confirm Email")]
        public string confirmemailaddress { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [ValidatePasswordHasNoSpaces]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters", MinimumLength = 7)]
        [DisplayName("Password")]
        public string password { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        //[Compare("Password", ErrorMessage = "The Passwords do not match.")]
        public string confirmpassword { get; set; }

        [DataMember]
        public string openididentifer { get; set; }

        [DataMember]
        public string openidprovider { get; set; }

        [IgnoreDataMember]
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(registermodel), "ValidatebirthdateDate")]
        [DisplayName("Date of Birth")]
        public DateTime birthdate { get; set; }

        [DataMember(Name = "birthdate")]
        public string FormattedDate
        {
            get { return string.Format("{0:yyyy-MM-ddTHH:mm:ss.fffZ}", birthdate); }
            set {;}
        }

        [DataMember]
        public List<string> genders { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [DataMember]
        public List<string> countries { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Country")]
        public string country { get; set; }

           [DataMember]
        public bool postalcodestatus { get; set; }

        [DataMember]
        public List<string> cities { get; set; }

           [DataMember]
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string city { get; set; }

        [DataMember]
        [DataType(DataType.Text)]
        public string stateprovince { get; set; }

        [DataMember]
        public double? longitude { get; set; }

        [DataMember]
        public double? lattitude { get; set; }



        [DataMember]
        public List<string> ziporpostalcodes { get; set; }

           [DataMember]
        [DataType(DataType.Text)]
        [DisplayName("Zip/PostalCode")]
        //[RequiredPostalCodeIf("PostalCodeStatus", Comparison.IsEqualTo, true)]
        public string ziporpostalcode { get; set; }


        // public SecurityQuestion SecurityQuestion { get; set; }
        [DataMember]
        public List<string> securityquestions { get; set; }


        //[DisplayName("Security Question")]
        //public string SecurityQuestion { get; set; }


        //[DisplayName("Security Answer")]
        //public string SecurityAnswer { get; set; }

        //photo model stuff 
           [DataMember]
        public photoeditmodel registrationphotos { get; set; }

        //add temp storage for activation code too i guess
           [DataMember]
        public string activationcode { get; set; }


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
