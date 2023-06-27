using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CountryData;
using JobApplication.CustomValidations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Security.AccessControl;
using System.Resources;
using Microsoft.Extensions.Localization;
using Bogus;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace JobApplication.Models
{
    public class JobApplication
    {
        [Key]
        public int ApplicationID { get; set; }
        [Required(ErrorMessage = "Please enter your First Name")]
        [RegularExpression("([a-zA-Z]{3,30}\\s*)+", ErrorMessage = "First Name is invalid")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Please enter your Last Name")]
        [RegularExpression("([a-zA-Z]{3,30}\\s*)+", ErrorMessage = "Last Name is invalid")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$",ErrorMessage = "Email Address is invalid")]
        [Display(Name = "Email Address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm your Email Address")]
        [Compare("Email",ErrorMessage = "Email Addresses do not match")]
        [Display(Name = "Confirm Email")]

        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage ="Please enter your Date of Birth")]
        [DataType(DataType.Date)]
        [DateOfBirthValidator(ErrorMessage = "You have to be between 18 and 65 years old")]
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage ="Please enter your Address")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Address must contain only letters and numbers")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your City")]
        [RegularExpression("^(?=.{2})\\p{L}+(?:\\s\\p{L}+)*$", ErrorMessage = "City must contain only letters")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please select your Country")]
        [Display(Name = "Country")] 
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "Please enter your Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d{0,3}\s?\(?\d{2}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Provided Phone Number not valid")]
        public string Phone { get; set; }
        [HomePhoneValidator(ErrorMessage = "Home phone is invalid")]
        [Display(Name ="Home Phone")]
        public string? HomePhone { get; set; }
        [Required(ErrorMessage = "Please select your Working Preference")]
        [Display(Name = "Working Preferences")]
        public string WorkingPreferences { get; set; }
        [Required(ErrorMessage = "Please select one of the Options")]
        public string Position { get; set; }
        [Display(Name ="Cover Letter")]
        public string? CoverLetter { get; set; }

        [Display(Name = "CV")]
        public FileModel? FileUpload { get; set; }
        public int? FileUploadFileModelId { get; set; }
    }
}
