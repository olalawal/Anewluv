using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anewluv.Domain.Data.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class ChangePasswordModel : IValidatableObject
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [DisplayName("Confirm new password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// test of IvalidateObject
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OldPassword == null)
            {
                yield return new ValidationResult("The old password is mandatory.");
            }
            if (NewPassword == null)
            {
                yield return new ValidationResult("The new password is mandatory");
            }
        }
    }

}
