using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLyer.Model
{
    //public enum Roles
    //{
    //    User, Provider, Admin
    //}
    public class User : IdentityUser
    {
        
        public bool IsDeleted { get; set; }
        public IsAccepted IsAccepted { get; set; } = IsAccepted.Bending;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImgSrc { get; set; }
        
        
        
        /// <summary>
        //  relation between User and other table
        /// </summary>
        public virtual Provider? Provider { get; set; }
        public virtual List<Favorite>? Favorites { get; set; }
        public virtual List<Rate>? Rates { get; set; }
        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<UserPhone>? UserPhones { get; set; }




    }
}
