using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using Microsoft.AspNetCore.Identity;

namespace ELECTIENDA.ViewModel
{
    public static class RoleExtension
    {
        public static IdentityRole ToModel(this RoleAddViewModel model)
        {
            return new IdentityRole
            {
                Id = model.ID.ToString(),
                Name = model.Name
            };
        }
    }
}
