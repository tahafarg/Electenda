using FirstLyer.Model;
using Microsoft.AspNetCore.Identity; 
using Microsoft.Extensions.Options; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Security.Claims; 
using System.Threading.Tasks; 

namespace ElECTIENDA.MVC.Helpers
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        UserManager<User> _userManager;
        public UserClaimsFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims = await base.GenerateClaimsAsync(user);
            var Result = await _userManager.GetRolesAsync(user);
            List<string> Roles = Result.ToList();
            foreach (string role in Roles)
                claims.AddClaim(new Claim(role, role));

            return claims;

        }
    }
}
