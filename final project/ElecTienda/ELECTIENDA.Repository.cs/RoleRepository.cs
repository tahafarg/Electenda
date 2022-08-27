using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FirstLyer.Model;
using ELECTIENDA.ViewModel;

namespace ELECTIENDA.Repository
{
    public class RoleRepository : GeneralRepository<IdentityRole>
    {
        RoleManager<IdentityRole> roleManager;
        public RoleRepository(FinalProjectContext _context , RoleManager<IdentityRole> _manager ):base(_context) 
        {
            roleManager = _manager;

        }

        public async Task<IdentityResult> Add(RoleAddViewModel model)
        {
           return await roleManager.CreateAsync(model.ToModel());
        }
    }
}
