using System.ComponentModel.DataAnnotations;

namespace VaporInfrastructure.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress(ErrorMessage = "Некоректна адреса")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Запам'ятати")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
