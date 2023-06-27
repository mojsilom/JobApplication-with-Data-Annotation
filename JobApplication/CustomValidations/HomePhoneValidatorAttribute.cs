using Microsoft.Extensions.Localization;
using JobApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JobApplication.CustomValidations
{
    //public class HomePhoneValidatorAttribute : ValidationAttribute
    //{
    //    string pattern = "^(03[0-9]|04[9]|05[0-9])\\d{6}$";

    //    //public HomePhoneValidatorAttribute()
    //    //{
    //    //    ErrorMessageResourceType = typeof(Resources);
    //    //    ErrorMessageResourceName = "HomePhoneValidatorErrorMessage";
    //    //}
    //    public override bool IsValid(object value)
    //    {
    //        if (value == null)
    //            return true;

    //        if (!(value is string valueAsString))
    //        {
    //            return false;
    //        }
    //        valueAsString = valueAsString.Replace(" ", "");
    //        if (Regex.IsMatch(valueAsString, pattern))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}
    public class HomePhoneValidatorAttribute : ValidationAttribute
    {
        string pattern = "^(03[0-9]|04[9]|05[0-9])\\d{6}$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!(value is string valueAsString))
            {
                return new ValidationResult(GetErrorMessage(validationContext, ErrorMessage));
            }
            valueAsString = valueAsString.Replace(" ", "");
            if (Regex.IsMatch(valueAsString, pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage(validationContext, ErrorMessage));
            }
        }

        private string GetErrorMessage(ValidationContext validationContext, string key)
        {
            var stringLocalizer = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<HomePhoneValidatorAttribute>));
            return stringLocalizer[key];
        }
    }
}
