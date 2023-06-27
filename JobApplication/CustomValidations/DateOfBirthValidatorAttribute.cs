using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using JobApplication.Models;
using System.ComponentModel.DataAnnotations;

//namespace JobApplication.CustomValidations
//{
//    public class DateOfBirthValidatorAttribute : ValidationAttribute
//    {
//        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
//        {
//            var applicant = (JobApplication)validationContext.ObjectInstance;

//            if(applicant.DateOfBirth==null)
//                return new ValidationResult("Date of Birth is required.");

//            var age = DateTime.Today.Year - applicant.DateOfBirth.Value.Year;

//            return (age >= 18)
//                ? ValidationResult.Success
//                : new ValidationResult(ErrorMessage);
//        }
//    }

//}

namespace JobApplication.CustomValidations
{
    public class DateOfBirthValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var applicant = (Models.JobApplication)validationContext.ObjectInstance;

                if (applicant.DateOfBirth == null)
                return new ValidationResult(GetErrorMessage(validationContext,ErrorMessage));

                var age = DateTime.Today.Year - applicant.DateOfBirth.Value.Year;

            return (age >= 18 && age<=65)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage(validationContext, ErrorMessage));
        }

        private string GetErrorMessage(ValidationContext validationContext, string key)
        {
            var stringLocalizer = (IStringLocalizer)validationContext.GetService(typeof(IStringLocalizer<DateOfBirthValidatorAttribute>));
            return stringLocalizer[key];
        }
    }
}
