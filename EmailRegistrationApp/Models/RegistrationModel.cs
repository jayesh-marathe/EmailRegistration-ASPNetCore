using System.ComponentModel.DataAnnotations;

namespace EmailRegistrationApp.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Temporary Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Temporary Password")]
        [CustomPasswordValidation]
        public string TemporaryPassword { get; set; }
    }

    public class CustomPasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
                return new ValidationResult("Password is required");

            var errors = new List<string>();

            if (password.Length < 10)
                errors.Add("Minimum length is 10 characters");
            if (!password.Any(char.IsUpper))
                errors.Add("At least 1 uppercase letter");
            if (!password.Any(char.IsLower))
                errors.Add("At least 1 lowercase letter");
            if (!password.Any(char.IsDigit))
                errors.Add("At least 1 numeric digit");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                errors.Add("At least 1 special character");

            if (errors.Any())
                return new ValidationResult(string.Join(", ", errors));

            return ValidationResult.Success;
        }
    }
}
