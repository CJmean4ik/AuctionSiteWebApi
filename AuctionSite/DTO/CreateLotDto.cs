using AuctionSite.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API.DTO
{
    public class CreateLotDto : IValidatableObject
    {
        public string? Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; } = string.Empty;
        public string? CategoryName { get; set; } = string.Empty;
        public int? DurationSale { get; set; }
        public string? FullDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public IFormFile? ImagePreview { get; set; }
        public IFormFile? FullImage { get; set; }
        public LotStatus LotStatus { get; set; } = LotStatus.Active;

        public string? ImagePreviewName { get; set; }
        public string? FullImageName { get; set; }
             

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

            if (DurationSale == null)
                validations.Add(new ValidationResult("Тривалість продажу повинна бути більше нуля"));

            if (string.IsNullOrWhiteSpace(FullDescription))
                validations.Add(new ValidationResult("Введіть повний опис"));

            if (FullImage == null)
                validations.Add(new ValidationResult("Лот повинен мати повну картинку"));

            return validations;
        }
    }
}
