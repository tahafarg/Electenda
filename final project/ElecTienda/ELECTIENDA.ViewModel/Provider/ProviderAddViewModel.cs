using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ELECTIENDA.ViewModel
{
   public class ProviderAddViewModel
    {

        [Required, StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
       public int MembershipID { get; set; }

        public string? LicenseImageUrl { get; set; }
        public IFormFile? LicenseImg { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string Role { get; set; } = "Provider";

    }
}
