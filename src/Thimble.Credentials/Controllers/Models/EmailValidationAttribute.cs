using System.ComponentModel.DataAnnotations;

namespace Thimble.Credentials.Controllers.Models
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = (string) value;
            if (new EmailAddressAttribute().IsValid(email))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Email Address is Invalid");
        }
    }
}