using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            
            builder.Property(c => c.UserID).IsRequired();
            builder.Property(c => c.ProductID).IsRequired(false); 
            builder.Property(c => c.ServicesId).IsRequired(false);
            // builder.Property(c=>c.Quantity).IsRequired();
        }
    }
}
