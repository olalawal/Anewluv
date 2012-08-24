using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

using Shell.MVC2.Domain.Entities.Anewluv;



//for RIA services contrib
//using RiaServicesContrib.Mvc.Services;


using System.Runtime.Serialization;
using Shell.MVC2.Domain.Entities.Anewluv.Validation;


namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
    public class ContactUsModel
    {
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Valid Email Address is required.")] 
        [DataType(DataType.EmailAddress)]
        [StringLength(25, ErrorMessage = "Email Address cannot be longer that 25 Characters")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(25, ErrorMessage = " Name cannot be longer than 25 Characters and No Less than 1 ", MinimumLength = 1)]
        public string Name { get; set; }


        [Required]   
        [DisplayName("Subject")]
        [StringLength(50, ErrorMessage = "Subject cannot be longer than 50 Characters and No Less than 5 ", MinimumLength = 5)]
        public string Subject { get; set; }

        [Required]   
        [DisplayName("Message")]
        [StringLength(255, ErrorMessage = "Message cannot be longer than 255 characters or less than 10", MinimumLength = 10)]       
        public string Message { get; set; }

    }




}