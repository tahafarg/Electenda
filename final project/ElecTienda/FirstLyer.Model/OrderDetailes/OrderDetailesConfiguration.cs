using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class OrderDetailesConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetailes");
            builder.HasKey(od => od.ID);
            builder.Property(od => od.ProductId).IsRequired(false);
            builder.Property(od => od.ServicesId).IsRequired(false);
            //builder.Property(od => od.OrderId).IsRequired();
            builder.Property(od => od.ID).ValueGeneratedOnAdd();
            builder.Property(od=>od.SubPrice).IsRequired();
            builder.Property(od => od.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.ProductId).IsRequired(false);
            builder.Property(c => c.ServicesId).IsRequired(false);
            //builder.Property(od => od.ProductQuantity).IsRequired();
        }
    }
}
