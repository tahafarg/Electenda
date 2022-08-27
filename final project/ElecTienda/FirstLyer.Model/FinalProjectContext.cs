using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FirstLyer.Model
{
    public class FinalProjectContext : IdentityDbContext<User>
    {
        public FinalProjectContext (DbContextOptions options) : base(options)
        { }
        //public DbSet<User> Users { get; set; }
        public DbSet <UserPhone> UserPhones { get; set; }
        public DbSet <Rate> Rates { get; set; }
        public DbSet <Provider> Providers { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetailes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet <Membership> Memberships { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Cards { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BrandConfiguration().Configure(modelBuilder.Entity<Brand>());
            new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new CategoryBrandConfiguration().Configure(modelBuilder.Entity<CategoryBrand>());
            new FavoriteConfiguration().Configure(modelBuilder.Entity<Favorite>());
            new ImageConfigration().Configure(modelBuilder.Entity<Image>());
            new MembershipConfiguration().Configure(modelBuilder.Entity<Membership>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new OrderDetailesConfiguration().Configure(modelBuilder.Entity<OrderDetails>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProviderConfiguration().Configure(modelBuilder.Entity<Provider>());
            new RateConfiguration().Configure(modelBuilder.Entity<Rate>());
           // new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new UserphoneConfiguration().Configure(modelBuilder.Entity<UserPhone>());
            new ShopConfiguration().Configure(modelBuilder.Entity<Shop>());
            new ServicesConfiguration().Configure(modelBuilder.Entity<Services>());

            modelBuilder.MapRelation();

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source =.;Initial Catalog=FinalProject; Integrated security = True");
        //}


    }
}
