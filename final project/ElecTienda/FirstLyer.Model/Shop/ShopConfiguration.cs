using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shop");
            builder.HasKey(s => s.ID);
            builder.Property(s => s.ID).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).HasMaxLength(300).IsRequired();
            builder.Property(s => s.Address).HasMaxLength(300).IsRequired();
            builder.Property(s => s.ImgSrc).HasMaxLength(300).IsRequired();
            builder.Property(s => s.ProviderId).IsRequired();
        }
    }
}
