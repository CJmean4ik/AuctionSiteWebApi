using System.ComponentModel.DataAnnotations;

namespace AuctionSite.API
{
    public class UserAuthorizePostDto
    {
        [Required(ErrorMessage = "Почта не вказана!")]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль повинен бути вказаний!")]
        public string EnteredPassword { get; set; } = string.Empty;
    }
}