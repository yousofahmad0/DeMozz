using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.ViewModel
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
