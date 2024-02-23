using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AuctionSite.API.DTO
{
    public class UserSignUpPostDto : IValidatableObject
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validations = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(FirstName))
                validations.Add(new ValidationResult("Введіть ім'я"));

            if (string.IsNullOrWhiteSpace(SecondName))
                validations.Add(new ValidationResult("Введіть прізвище"));

            if (string.IsNullOrWhiteSpace(Email))
                validations.Add(new ValidationResult("Введіть пошту"));

            if (Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                validations.Add(new ValidationResult("Введіть коректну пошту"));
            
            if (Password.Length < 3)
                validations.Add(new ValidationResult("Паролько повинен бути більше ніж 3 символи"));

            return validations;
        }
    }
}
