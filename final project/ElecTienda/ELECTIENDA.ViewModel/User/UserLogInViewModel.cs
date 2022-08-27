using System.ComponentModel.DataAnnotations;

namespace ELECTIENDA.ViewModel
{
    public class UserLogInViewModel
    {

        [Required, StringLength(50, MinimumLength = 3), EmailAddress]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password), StringLength(50, MinimumLength = 3)]
        public string? Password { get; set; }
        //[Required]
        //[Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = true;
    }
}
