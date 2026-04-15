using System.ComponentModel.DataAnnotations;

namespace VaporInfrastructure.ViewModel
{
    public class ChangeNicknameViewModel
    {
        [Required(ErrorMessage = "Введіть новий нікнейм")]
        [Display(Name = "Новий нікнейм")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Нікнейм має бути від 3 до 50 символів")]
        public string NewNickname { get; set; } = null!;
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введіть старий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Введіть новий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Підтвердіть новий пароль")]
        [Compare("NewPassword", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити новий пароль")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}