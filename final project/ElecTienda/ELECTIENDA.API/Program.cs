
using Microsoft.Extensions.FileProviders;
using ELECTIENDA.Repository;
using FirstLyer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class Program
{
    public static int Main()
    {
        var builder = WebApplication.CreateBuilder();

        #region DI Continer

        builder.Services.AddDbContext<FinalProjectContext>(i =>
        {
            i.UseLazyLoadingProxies().UseSqlServer
            (builder.Configuration.GetConnectionString("ElecTenda"));
        });
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<FinalProjectContext>();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        });

        builder.Services.AddScoped(typeof(UserAdminRepository));
        builder.Services.AddScoped(typeof(ProviderRepository));
        builder.Services.AddScoped(typeof(CategoryRepository));
        builder.Services.AddScoped(typeof(FinalProjectContext));
        builder.Services.AddScoped(typeof(OrderRepository));
        builder.Services.AddScoped(typeof(UserRepository));
        builder.Services.AddScoped(typeof(ProductRepository));
        builder.Services.AddScoped(typeof(BrandRepository));
        builder.Services.AddScoped(typeof(ShopRepository));
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped(typeof(RateRepository));
        builder.Services.AddScoped(typeof(CategoryRepository));
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(FinalProjectContext));
        builder.Services.AddScoped(typeof(RoleRepository));
        builder.Services.AddControllersWithViews();
        #endregion


        var app = builder.Build();

        #region MiddaleWares HTTP Requestes
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Content")
               ),
            RequestPath = "/Content"
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute("default",
            "{controller=Admin}/{action=Index}");
        #endregion

        app.Run();
        return 0;
    }
}