using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELECTIENDA.ViewModel
{
    public class UserEditViewModel
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string? FirstName { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string? LastName { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Required , StringLength (100,MinimumLength = 3)]
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; } = "000000000000";

        public string? ImageUrl { get; set; } = "hdfhhfhh";
        public string Image { get; set; } = "jjjjjj";
        public string Role { get; set; } = "User";
    }
}
