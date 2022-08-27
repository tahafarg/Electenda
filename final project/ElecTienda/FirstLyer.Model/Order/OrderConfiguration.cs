using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.ID);
            builder.Property(o => o.ID).ValueGeneratedOnAdd();
            builder.Property(o => o.UserID).IsRequired();
           // builder.Property(o => o.OrderNum).IsRequired();
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.IsDeleted).HasDefaultValue(false);
            builder.Property(o => o.ShippingDate).IsRequired();
            builder.Property(o => o.TotalPrice).IsRequired();
            builder.Property(o => o.Statue).HasDefaultValue(statues.Pending);

        }
    }
}
