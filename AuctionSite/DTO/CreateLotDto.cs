﻿using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.DTO
{
    public class CreateLotDto : IValidatableObject
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public IFormFile ImagePreview { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validations = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
                validations.Add(new ValidationResult("Введіть ім'я")); 
            
            if (string.IsNullOrWhiteSpace(ShortDescription))
                validations.Add(new ValidationResult("Введіть короткий опис"));

            if (string.IsNullOrWhiteSpace(CategoryName))
                validations.Add(new ValidationResult("Введіть категорію"));

            if (ImagePreview == null)
                validations.Add(new ValidationResult("Лот повинен мати картинку"));

            string[] lenghtDesc = ShortDescription.Split(" ");
            if (lenghtDesc.Length < 3 || lenghtDesc.Length > 30)
                validations.Add(new ValidationResult("Опис повинени бути більше за три слова і менше ніж 30"));

            return validations;       
        }
    }
}
