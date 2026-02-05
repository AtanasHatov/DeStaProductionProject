using System.ComponentModel.DataAnnotations;

namespace DeStaProduction.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        public string? UserName { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
