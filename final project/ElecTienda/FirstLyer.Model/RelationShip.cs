using Microsoft.EntityFrameworkCore;

namespace FirstLyer.Model
{
    public static class RelationShip
    {
        public static void MapRelation(this ModelBuilder builder)
        {
            ////////////////////////////////////////other (phone & image)///////////////////////////////////

            builder.Entity<UserPhone>().HasOne(up => up.User).WithMany(u => u.UserPhones);
            builder.Entity<Image>().HasOne(i => i.Product).WithMany(p => p.Images).IsRequired();
            builder.Entity<Shop>().HasOne(s => s.Provider).WithMany(p => p.Shops).IsRequired().OnDelete(DeleteBehavior.Cascade);

            /////////////////////////////////////////order relation/////////////////////////////////////////

            builder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders).IsRequired().OnDelete(DeleteBehavior.Cascade).HasForeignKey(o => o.UserID);

            //////////////////////////////////////////favorite relation///////////////////////////////////////////

            builder.Entity<Favorite>().HasOne(f => f.Product).WithMany(p => p.Favorites).HasForeignKey(f => f.ProductID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Favorite>().HasOne(f => f.Services).WithMany(s => s.Favorites).HasForeignKey(f => f.ServicesId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Favorite>().HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserID).OnDelete(DeleteBehavior.Cascade);

            //////////////////////////////////////////cart relation///////////////////////////////////////////

            builder.Entity<Cart>().HasOne(c => c.User).WithMany(u => u.Carts).HasForeignKey(c => c.UserID).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Cart>().HasOne(c => c.Services).WithMany(s => s.Carts).HasForeignKey(c => c.ServicesId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Cart>().HasOne(c => c.Product).WithMany(p => p.Carts).HasForeignKey(c => c.ProductID).OnDelete(DeleteBehavior.NoAction);

            //////////////////////////////////////////rate relation///////////////////////////////////////////

            builder.Entity<Rate>().HasOne(r => r.User).WithMany(u => u.Rates).HasForeignKey(c => c.UserID).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Rate>().HasOne(c => c.Services).WithMany(s=>s.Rates).HasForeignKey(r => r.ServicesId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Rate>().HasOne(r => r.Product).WithMany(p => p.Rates).HasForeignKey(c => c.ProductID).OnDelete(DeleteBehavior.NoAction);

            //////////////////////////////////////////provider relation////////////////////////////////

            builder.Entity<Provider>().HasOne(p => p.User).WithOne(u => u.Provider).HasForeignKey<Provider>(p => p.UserID).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Provider>().HasOne(p => p.Membership).WithMany(m => m.Providers).HasForeignKey(p => p.MembershipID).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //////////////////////////////////////////product relation//////////////////////////////////

            builder.Entity<Product>().HasOne(p => p.Provider).WithMany(pro => pro.Products).HasForeignKey(p => p.ProviderID).OnDelete(DeleteBehavior.NoAction).IsRequired();
            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID).IsRequired().OnDelete(DeleteBehavior.Cascade);

            /////////////////////////////////////////////Services relation ////////////////////////////////////
            
            builder.Entity<Services>().HasOne(s => s.Category).WithMany(c => c.Services).HasForeignKey(s => s.CategoryID).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Services>().HasOne(s => s.Provider).WithMany(c => c.Services).HasForeignKey(s => s.ProviderID).OnDelete(DeleteBehavior.Cascade).IsRequired();

            ////////////////////////////////////////////category & brand relation////////////////////////////////

            builder.Entity<CategoryBrand>().HasOne(cb => cb.Brand).WithMany(b => b.CategoryBrands).HasForeignKey(cb => cb.BrandId);
            builder.Entity<CategoryBrand>().HasOne(cb => cb.Category).WithMany(c => c.CategoryBrands).HasForeignKey(cb => cb.CategoryId);

            ///////////////////////////////////////////order & product relation//////////////////////////////////

            builder.Entity<OrderDetails>().HasOne(od => od.Product).WithMany(p => p.OrderDetailes).HasForeignKey(od => od.ProductId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrderDetails>().HasOne(od => od.Services).WithMany(s => s.OrderDetailes).HasForeignKey(od => od.ServicesId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrderDetails>().HasOne(od => od.Order).WithMany(o => o.OrderDetailes).HasForeignKey(od => od.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}