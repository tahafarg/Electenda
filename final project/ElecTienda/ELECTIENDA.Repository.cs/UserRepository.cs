using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstLyer.Model;
using Microsoft.AspNetCore.Identity;

using ELECTIENDA.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace ELECTIENDA.Repository
{
    public class UserRepository : GeneralRepository<User>
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        IConfiguration _Configuration;

        public UserRepository(FinalProjectContext finalProjectContext, UserManager<User> _userManager, SignInManager<User> _signInManager, IConfiguration configuration) : base(finalProjectContext)
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
            _Configuration = configuration;
        }

        public async Task <IdentityResult> SignUp (UserEditViewModel model)
        {
            User user = model.ToModel();
            var result = await UserManager.CreateAsync(user, model.Password);
            result = await UserManager.AddToRoleAsync(user,model.Role);
            return result;
        }

        public async Task<string> SignIn(UserLogInViewModel model)
        {
            var result = await SignInManager.PasswordSignInAsync(model.Email,
                   model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                User User = await UserManager.FindByEmailAsync(model.Email);
                List<Claim> claims = new List<Claim>();
                IList<string> roles = await UserManager.GetRolesAsync(User);
                roles.ToList().ForEach(i => claims.Add(new Claim(ClaimTypes.Role, i)));
                JwtSecurityToken token
                    = new JwtSecurityToken
                    (
                      signingCredentials: new SigningCredentials
                        (
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Configuration["JWT:Key"]))
                            , SecurityAlgorithms.HmacSha256
                        ),
                      expires: DateTime.Now.AddDays(20),
                      claims: claims
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }

        public async Task SignOut()
            => await SignInManager.SignOutAsync();

        public int Detailes(string id)
        {
            User user = base.GetList().Where(u => u.Id == id).FirstOrDefault();
            var Data = user.Provider.ProviderID;
            return Data;
        }
        public string Getuserid(string email )
        {
           return UserManager.FindByEmailAsync(email).Result.Id;
        }
       
    }
}
