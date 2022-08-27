
using Microsoft.Extensions.FileProviders;
using ELECTIENDA.Repository;
using FirstLyer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ElECTIENDA.MVC.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

public class Program
{
    public static int Main()
    {
        var builder = WebApplication.CreateBuilder();

        #region DI Continer

        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {

            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]))
            };
        });

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<FinalProjectContext>();

        builder.Services.AddDbContext<FinalProjectContext>(i =>
        {
            i.UseLazyLoadingProxies().UseSqlServer
            (builder.Configuration.GetConnectionString("ElecTenda"));
        });
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/SignIn";
        });

        builder.Services.AddScoped(typeof(UserAdminRepository));
        builder.Services.AddScoped(typeof(ProviderRepository));
        builder.Services.AddScoped(typeof(MembershipRepository));
        builder.Services.AddScoped(typeof(CategoryRepository));
        builder.Services.AddScoped(typeof(FinalProjectContext));
        builder.Services.AddScoped(typeof(OrderDetailsRepository));
        builder.Services.AddScoped(typeof(OrderRepository));
        builder.Services.AddScoped(typeof(UserRepository)); 
        builder.Services.AddScoped(typeof(ProductRepository));
        builder.Services.AddScoped(typeof(BrandRepository));
        builder.Services.AddScoped(typeof(ShopRepository));
        builder.Services.AddScoped(typeof(CartRepository));
        builder.Services.AddScoped(typeof(MembershipRepository));
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(OrderDetailsRepository));
        builder.Services.AddScoped(typeof(RateRepository));
        builder.Services.AddScoped(typeof(ServicesRepository));
        builder.Services.AddScoped(typeof(CategoryRepository));
        builder.Services.AddScoped(typeof(FavoriteRepository));
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(FinalProjectContext));
        builder.Services.AddScoped(typeof(RoleRepository));
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsFactory>();
        
       
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(i =>
            {
                i.AllowAnyOrigin().AllowAnyMethod()
                .AllowAnyHeader();
            });
        });


      
        #endregion


        var app = builder.Build();
        app.UseCors();

        #region MiddaleWares HTTP Requestes
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Content")
               ),
            RequestPath = "/Content"
        });

        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute("default",
            "{controller=Provider}/{action=SignUp}");
        #endregion

        app.Run();
        return 0;
    }
}