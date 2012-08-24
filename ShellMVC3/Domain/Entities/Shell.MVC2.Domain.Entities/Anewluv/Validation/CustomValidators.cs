using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

using System.ComponentModel;

using System.Web.Security;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;


namespace Shell.MVC2.Domain.Entities.Anewluv.Validation
{

     public class CustomAnotations
     {
       public static   ValidationResult Compare(string Value1,string Value2)
       {
        bool isValid=false ;

        // Perform validation logic here and set isValid to true or false.
           if (Value1 == Value2) 
           {
               isValid = true;
           }

        if (isValid)
        {
          return ValidationResult.Success;
        }
        else
        {
          return new ValidationResult(
              "Values do not match");
        }
      }
    
     
     }

     
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
        public  class EmailAddressAttribute : DataTypeAttribute
        {

            private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);



            public EmailAddressAttribute()
                : base(DataType.EmailAddress)
            {

            }



            public override bool IsValid(object value)
            {

                string str = Convert.ToString(value, CultureInfo.CurrentCulture);

                if (string.IsNullOrEmpty(str))

                    return true;



                Match match = regex.Match(str);

                return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));

            }

        }


        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        public sealed class ActivationCodeIsValidAttribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
            private readonly object _typeId = new object();

            public ActivationCodeIsValidAttribute(string tmpprofileId, string tmpactivationcode)
                : base(_defaultErrorMessage)
            {
                tmpProfileid = tmpprofileId;
                tmpActivationcode = tmpactivationcode;
            }

            public string tmpProfileid { get; private set; }
            public string tmpActivationcode { get; private set; }

            public override object TypeId
            {
                get
                {
                    return _typeId;
                }
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                  tmpActivationcode, tmpProfileid);
            }

            public override bool IsValid(object value)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);

                //added validation here due to null value issues from the upload photos partial view
                //only validate if we actually have stuff to validate
                if (value == null)
                {
                    return true;
                }


                else
                {


                    object profileid = properties.Find(tmpProfileid, true /* ignoreCase */).GetValue(value);
                    object activationcode = properties.Find(tmpActivationcode, true /* ignoreCase */).GetValue(value);


                    //initialize access to the datating context / service
                    //var datingService = new DatingService();
                    //bool validActivationCode = datingService.CheckIfActivationCodeIsValid(profileid.ToString(), activationcode.ToString());
                    //call to the data model to verify the activation code 
                   // return validActivationCode;
                    return true;
                }



                //return true;
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
        public sealed class GalleryPhotoExistsValidAttribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "'{0}' Does Not Have Any Photos";
            private readonly object _typeId = new object();

            public GalleryPhotoExistsValidAttribute(string profileId)
                : base(_defaultErrorMessage)
            {
                Profileid = profileId;

            }

            public string Profileid { get; private set; }


            public override object TypeId
            {
                get
                {
                    return _typeId;
                }
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                    Profileid);
            }

            public override bool IsValid(object value)
            {

                var valueAsString = value as string;

                //initialize access to the datating context / service
               // var datingService = new DatingService();
               // bool validPhotoExists = datingService.CheckForGalleryPhotoByProfileID(valueAsString);
                //call to the data model to verify the activation code 

               // return validPhotoExists;
                return true;
            }
        }



        #region  Validation For Login, Register and Other stuff
        public static class AccountValidation
        {
            public static string ErrorCodeToString(MembershipCreateStatus createStatus)
            {
                // See http://go.microsoft.com/fwlink/?LinkID=177550 for
                // a full list of status codes.
                switch (createStatus)
                {
                    case MembershipCreateStatus.DuplicateUserName:
                        return "Username already exists. Please enter a different user name.";

                    case MembershipCreateStatus.DuplicateEmail:
                        return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                    case MembershipCreateStatus.InvalidPassword:
                        return "The password provided is invalid. Please enter a valid password value.";

                    case MembershipCreateStatus.InvalidEmail:
                        return "The e-mail address provided is invalid. Please check the value and try again.";

                    case MembershipCreateStatus.InvalidAnswer:
                        return "The password retrieval answer provided is invalid. Please check the value and try again.";

                    case MembershipCreateStatus.InvalidQuestion:
                        return "The password retrieval question provided is invalid. Please check the value and try again.";

                    case MembershipCreateStatus.InvalidUserName:
                        return "The user name provided is invalid. Please check the value and try again.";

                    case MembershipCreateStatus.ProviderError:
                        return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                    case MembershipCreateStatus.UserRejected:
                        return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                    default:
                        return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                }
            }
        }

      //  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        //public sealed class RegistrationCountryCityPostalCodeIsValidAttribute : ValidationAttribute
        //{
        //    private const string _defaultErrorMessage = " the zip or postal code '{2}' does not match the city : '{1}' and postalcode '{2}' ";
        //    private readonly object _typeId = new object();

        //    public RegistrationCountryCityPostalCodeIsValidAttribute(string CountryName, string City, string ZipOrPostalCode, string PostalCodeStatus)
        //        : base(_defaultErrorMessage)
        //    {
        //        _country = CountryName;
        //        _city = City;
        //        _ZipOrPostalCode = ZipOrPostalCode;
        //        // _postalCodeStatus = PostalCodeStatus;

        //    }

        //    public string _country { get; private set; }
        //    public string _city { get; private set; }
        //    public string _ZipOrPostalCode { get; private set; }
        //    public bool _postalCodeStatus { get; private set; }

        //    public override object TypeId
        //    {
        //        get
        //        {
        //            return _typeId;
        //        }
        //    }

        //    public override string FormatErrorMessage(string name)
        //    {
        //        if (_postalCodeStatus == true)
        //            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
        //              _country, _city, _ZipOrPostalCode);

        //        //if not send the error message for those with no postalcodes
        //        return String.Format(CultureInfo.CurrentUICulture, "The City {1} does not exists in the Country {0} if you feel this is in error contact us",
        //          _country, _city, _ZipOrPostalCode);
        //    }

        //    public override bool IsValid(object value)
        //    {

        //        if (value == null)
        //            return true;

        //        var postaldataservice = new PostalDataService();
        //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);



        //        var property = properties.Find("PostalCodeStatus", true);
        //        var postalcodestatus = (property.GetValue(value) != null) ? property.GetValue(value) : false;
        //        property = properties.Find("Country", true);
        //        _country = (property.GetValue(value) != null) ? property.GetValue(value).ToString() : "";
        //        property = properties.Find("City", true);
        //        _city = (property.GetValue(value) != null) ? property.GetValue(value).ToString() : "";
        //        property = properties.Find("ZipOrPostalCode", true);
        //        //  _ZipOrPostalCode  = property.GetValue(value).ToString();

        //        _ZipOrPostalCode = (property.GetValue(value) != null) ? property.GetValue(value).ToString() : "";




        //        _postalCodeStatus = (postaldataservice.GetCountry_PostalCodeStatusByCountryName(_country.ToString()) == 1);


        //        //
        //        //make sure postal code is not blank from validation context
        //        //check the city for the coma thing if it exists 

        //        //split up the city from state province
        //        string[] cityandprovince = _city.ToString().Split(',');
        //        if (cityandprovince != null)
        //        {
        //            _city = cityandprovince[0];
        //        }


        //        if (_postalCodeStatus == true)
        //        {


        //            //convert the value whihci is the model ? tthe type of model we use in this case members view mode l?
        //            // RegisterModel model = new RegisterModel();
        //            //model = (RegisterModel)(value);

        //            //split up the city from state province
        //            // string[] tempCityAndStateProvince = model.City.Split(',');

        //            // object countryName = properties.Find(tmpCountry, true /* ignoreCase */).GetValue(value);
        //            // object city = properties.Find(tmpCity, true /* ignoreCase */).GetValue(value);
        //            //object ziporpostalcode = properties.Find(tmpZipOrPostalCode, true /* ignoreCase */).GetValue(value);
        //            //initialize access to the datating context / service

        //            bool validpostalcode = postaldataservice.ValidatePostalCodeByCOuntryandCity(_country, cityandprovince[0], _ZipOrPostalCode);

        //            postaldataservice.Dispose();
        //            //call to the data model to verify the activation code 
        //            return validpostalcode;
        //        }
        //        else //no postal code
        //        {
        //            //as long as a geo code exists we are fine , store it in the view state as well
        //            string GeCode = postaldataservice.GetGeoPostalCodebyCountryNameAndCity(_country, cityandprovince[0]);
        //            postaldataservice.Dispose();
        //            if (GeCode == "")
        //                return false;

        //            //store it in the model
        //            return true;

        //        }




        //        //return true;

        //    }
        //}

        //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        //public sealed class CityAndCountryIsValidAttribute : ValidationAttribute
        //{
        //    private const string DefaultErrorMessageFormatString = "The City {0} is not valid for the Country {1}";

        //    public string CountryValue { get; private set; }
        //   // public Comparison Comparison { get; private set; }
        //    public object Value { get; private set; }

        //    public CityAndCountryIsValidAttribute(string otherProperty, object value)
        //    {
        //        if (string.IsNullOrEmpty(otherProperty))
        //        {
        //            throw new ArgumentNullException("otherProperty");
        //        }

        //        CountryValue = otherProperty;
        //       // Comparison = comparison;
        //        Value = value;

        //        ErrorMessage = DefaultErrorMessageFormatString;


        //    }

        //    public override string FormatErrorMessage(string name)
        //    {
        //        return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
        //          CountryValue  , Value.ToString());
        //    }

        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        //make sure coutnry value is not null as well
        //        var property = validationContext.ObjectInstance.GetType().GetProperty("Country");
        //        var CountryValue = property.GetValue(validationContext.ObjectInstance, null);

        //        var propert2 = validationContext.ObjectInstance.GetType().GetProperty("City");
        //        var City= property.GetValue(validationContext.ObjectInstance, null);


        //        if (value == null && CountryValue != null)
        //        {
        //            // var property = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
        //            // var property = validationContext.ObjectInstance.GetType().GetProperty("Country");
        //            //var CountryValue = property.GetValue(validationContext.ObjectInstance, null);
        //            //get property value from DB fuck it

        //            var postaldataservice = new PostalDataService().Initialize();
        //            propertyValue = (postaldataservice.GetCountry_PostalCodeStatusByCountryName(CountryValue.ToString()) == 1) ? true : false;



        //           if ())
        //           {
        //                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
        //            }
        //        }
        //        return ValidationResult(;
        //    }


        //}

    //TO DO validation should be a separate service move all these and do not use attributes
    //
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        public sealed class AccountModificationCountryCityPostalCodeIsValidAttribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = " the zip or postal code '{2}' does not match the city : '{1}' and postalcode '{2}' ";
            private readonly object _typeId = new object();

            public AccountModificationCountryCityPostalCodeIsValidAttribute(string CountryName, string City, string ZipOrPostalCode)
                : base(_defaultErrorMessage)
            {
                tmpCountry = CountryName;
                tmpCity = City;
                tmpZipOrPostalCode = ZipOrPostalCode;
            }

            public string tmpCountry { get; private set; }
            public string tmpCity { get; private set; }
            public string tmpZipOrPostalCode { get; private set; }

            public override object TypeId
            {
                get
                {
                    return _typeId;
                }
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                  tmpCountry, tmpCity, tmpZipOrPostalCode);
            }

            public override bool IsValid(object value)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);

                var postalcodestatus = properties.Find("PostalCodeStatus", true);
                //make sure coutnry value is not null as well
                // var property = validationContext.ObjectInstance.GetType().GetProperty("Country");
                // var CountryValue = property.GetValue(validationContext.ObjectInstance, null);

                //added validation here due to null value issues from the upload photos partial view
                //only validate if we actually have stuff to validate
                if (value == null)
                {
                    return true;
                }
                else
                {
                    AccountModel model = new AccountModel();
                    model = (AccountModel)(value);

                    //TO Do figure out what to do about postal validation, probbaly add a reference
                    //To the dating.server.web
                    //var postaldataservice = new PostalDataService();

                    ////split up the city from state province
                    //string[] tempCityAndStateProvince = model.City.Split(',');

                    ////convert the value whihci is the model ? tthe type of model we use in this case members view mode l?
                    //if (model.PostalCodeStatus == false && model.ZipOrPostalCode == null)
                    //    model.ZipOrPostalCode = postaldataservice.GetGeoPostalCodebyCountryNameAndCity(model.Country, tempCityAndStateProvince[0]);

                    //bool validpostalcode = postaldataservice.ValidatePostalCodeByCOuntryandCity(model.Country, tempCityAndStateProvince[0], model.ZipOrPostalCode);


                    // object countryName = properties.Find(tmpCountry, true /* ignoreCase */).GetValue(value);
                    // object city = properties.Find(tmpCity, true /* ignoreCase */).GetValue(value);
                    //object ziporpostalcode = properties.Find(tmpZipOrPostalCode, true /* ignoreCase */).GetValue(value);
                    //initialize access to the datating context / service



                    //call to the data model to verify the activation code 
                    //return validpostalcode;

                    return true;
                }



                //return true;
            }
        }

        //validate unqie emails here
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class EmailDoesNotExistAttribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "The {0} {1} is already taken";

            // Internal field to hold the username value.
            private string _email;
            public string email
            {
                get { return _email; }
                set { _email = value; }
            }


            public EmailDoesNotExistAttribute()
                : base(_defaultErrorMessage)
            {

            }


            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, this.email);
            }



            public override bool IsValid(object value)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);


                //added validation here due to null value issues from the upload photos partial view
                //only validate if we actually have stuff to validate
                if (value == null)
                {
                    return true;
                }
                else
                {
                    _email = value.ToString();

                    bool emailalreadyexists;
                    //call to the data model to verify the activation code 
                    using (AnewluvContext dbContext = new AnewluvContext())
                    {
                        emailalreadyexists = (dbContext.profiles.Any(u => u.id == value.ToString()));                          
                        //row = _repo.GetAdminLoginByUsername(valueAsString);
                    }


                    if (emailalreadyexists)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }



                //return true;
            }
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidateUsername : ValidationAttribute
        {
            private const string _defaultErrorMessage = "The {0} '{1}' is already taken";

            // Internal field to hold the username value.
            private string _username;
            public string username
            {
                get { return _username; }
                set { _username = value; }
            }
            public ValidateUsername()
                : base(_defaultErrorMessage)
            {
            }


            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, this.username);
            }

            public override bool IsValid(object value)
            {
                string valueAsString = value as string;
                if (valueAsString == null)
                    return false;

                _username = valueAsString;


                bool usernamealreadyexists;

                //TO DO move this and/or use DI
                using (AnewluvContext dbContext = new AnewluvContext())
                {
                    usernamealreadyexists  = (dbContext.profiles.Any(u => u.username  == value.ToString()));
                    //row = _repo.GetAdminLoginByUsername(valueAsString);
                }


                //TO DO fix thsi to work
                //using (DatingService dbContext = new DatingService())
                //{
                //    usernamealreadyexists = dbContext.CheckIfUserNameAlreadyExists(value.ToString());
                //    //row = _repo.GetAdminLoginByUsername(valueAsString);
                //}

                //if (usernamealreadyexists == true)
               //     return false;
              //  else
                //    return true;
                return true;
            }
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidateScreenname : ValidationAttribute
        {
            private const string _defaultErrorMessage = "{0} '{1}' is already taken";

            // Internal field to hold the username value.
            private string _screenname;
            public string screenname
            {
                get { return _screenname; }
                set { _screenname = value; }
            }
            public ValidateScreenname()
                : base(_defaultErrorMessage)
            {
            }


            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, this.screenname);
            }

            public override bool IsValid(object value)
            {
                string valueAsString = value as string;

                if (valueAsString == null)
                    return false;

                _screenname = valueAsString.ToString();


                bool screennamealreadyexists;

                //TO DO move this and/or use DI
                using (AnewluvContext dbContext = new AnewluvContext())
                {
                    screennamealreadyexists = (dbContext.profiles.Any(u => u.screenname == value.ToString()));
                    //row = _repo.GetAdminLoginByUsername(valueAsString);
                }

             
                if (screennamealreadyexists == true)
                    return false;
                else
                    return true;
            }
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
        {

            private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
            private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;
            public ValidatePasswordLengthAttribute()
                : base(_defaultErrorMessage)
            {
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _minCharacters);

            }
            public override bool IsValid(object value)
            {
                string valueAsString = value as string;

                if (valueAsString == null)
                    return false;

                return (valueAsString != null && valueAsString.Length >= _minCharacters);
            }

       }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidatePasswordHasNoSpaces : ValidationAttribute
        {

            private const string _defaultErrorMessage = "Your password cannot contain be blank or contain spaces";

            public ValidatePasswordHasNoSpaces()
                : base(_defaultErrorMessage)
            {
            }

            public override string FormatErrorMessage(string name)
            {
                return _defaultErrorMessage;

            }
            public override bool IsValid(object value)
            {
                string valueAsString = value as string;

                if (valueAsString == null)
                    return false;

                if (valueAsString.Any(z => z.ToString().Contains(" ")))
                    return false;
                return true;



            }





        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidateUserNameHasNoSpaces : ValidationAttribute
        {

            private const string _defaultErrorMessage = "Your UserName cannot contain be blank or contain spaces";

            public ValidateUserNameHasNoSpaces()
                : base(_defaultErrorMessage)
            {
            }

            public override string FormatErrorMessage(string name)
            {
                return _defaultErrorMessage;

            }
            public override bool IsValid(object value)
            {
                string valueAsString = value as string;

                if (valueAsString == null)
                    return false;

                if (valueAsString.Any(z => z.ToString().Contains(" ")))
                    return false;
                return true;



            }





        }

         // TO DO move to client side validation calling the servcies
       // [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        //public sealed class RequiredPostalCodeIfAttribute : ValidationAttribute
        //{
        //    private const string DefaultErrorMessageFormatString = "The {0} field is required.";

        //    public string OtherProperty { get; private set; }
        //    public Comparison Comparison { get; private set; }
        //    public object Value { get; private set; }

        //    public RequiredPostalCodeIfAttribute(string otherProperty, Comparison comparison, object value)
        //    {
        //        if (string.IsNullOrEmpty(otherProperty))
        //        {
        //            throw new ArgumentNullException("otherProperty");
        //        }

        //        OtherProperty = otherProperty;
        //        Comparison = comparison;
        //        Value = value;

        //        ErrorMessage = DefaultErrorMessageFormatString;
        //    }

        //    private bool IsRequired(object actualPropertyValue)
        //    {
        //        switch (Comparison)
        //        {
        //            case Comparison.IsNotEqualTo:
        //                return actualPropertyValue == null || !actualPropertyValue.Equals(Value);
        //            default:
        //                return actualPropertyValue != null && actualPropertyValue.Equals(Value);
        //        }
        //    }

        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        //make sure coutnry value is not null as well
        //        var property = validationContext.ObjectInstance.GetType().GetProperty("Country");
        //        var CountryValue = property.GetValue(validationContext.ObjectInstance, null);

        //        if (value == null && CountryValue != null)
        //        {
        //            // var property = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
        //            // var property = validationContext.ObjectInstance.GetType().GetProperty("Country");
        //            //var CountryValue = property.GetValue(validationContext.ObjectInstance, null);
        //            //get property value from DB fuck it

        //            var postaldataservice = new PostalDataService();
        //            var propertyValue = (postaldataservice.GetCountry_PostalCodeStatusByCountryName(CountryValue.ToString()) == 1) ? true : false;


        //            if (IsRequired(propertyValue))
        //            {
        //                return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
        //            }
        //        }
        //        return ValidationResult.Success;
        //    }


        //}


        #endregion
}
