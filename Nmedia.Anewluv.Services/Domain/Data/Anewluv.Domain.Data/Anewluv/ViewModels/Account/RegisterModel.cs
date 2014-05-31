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


//using Anewluv.Domain.Data.Validation;

namespace Anewluv.Domain.Data.ViewModels
{



    [DataContract] 
    public class registermodel
    {
       // public string EarliestDateFrom = DateTime.Now.AddYears(-18).ToString();
       // public string CurrentDate = DateTime.Now.ToString();


        [DataMember]      
        public string username { get; set; }
        [DataMember]     
        public string screenname { get; set; }
        [DataMember]    
        public string emailaddress { get; set; }
        [DataMember]   
        public string confirmemailaddress { get; set; }
        [DataMember]      
        public string password { get; set; }
        [DataMember]     
        public string confirmpassword { get; set; }
        [DataMember]
        public string openididentifer { get; set; }
        [DataMember]
        public string openidprovider { get; set; }
        [IgnoreDataMember]     
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
        public string gender { get; set; }
        [DataMember]
        public List<string> countries { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public bool postalcodestatus { get; set; }
        [DataMember]
        public List<string> cities { get; set; }

        [DataMember]      
        public string city { get; set; }

        [DataMember]       
        public string stateprovince { get; set; }
        [DataMember]
        public double? longitude { get; set; }
        [DataMember]
        public double? lattitude { get; set; }
        [DataMember]
        public List<string> ziporpostalcodes { get; set; }
        [DataMember]      
        public string ziporpostalcode { get; set; }
       
        [DataMember]
        public photoeditmodel registrationphotos { get; set; }

        //add temp storage for activation code too i guess     ]
        [DataMember]
        public string activationcode { get; set; }

        //public static ValidationResult ValidatebirthdateDate(DateTime birthdateDateToValidate)
        //{

        //    //if (birthdateDateToValidate.Date > DateTime.Today) 
        //    //{ 
        //    //   return new ValidationResult("Your must be at least 18 years old to Register"); 

        //    //} 
        //    if (birthdateDateToValidate.Date > DateTime.Today.AddYears(-18))
        //    {
        //        return new ValidationResult("You must be at least 18 years old");
        //    }
        //    return ValidationResult.Success;
        //}





    }
}
