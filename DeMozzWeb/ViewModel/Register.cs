using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.ViewModel
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confiration password did not match!")]
        public string ConfirmPassword { get; set; }
    }
}
