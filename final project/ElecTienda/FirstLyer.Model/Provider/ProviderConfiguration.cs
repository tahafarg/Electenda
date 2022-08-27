using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Provider");
            builder.HasKey(p => p.ProviderID);
            builder.Property(p => p.ProviderID).ValueGeneratedOnAdd();
            builder.Property(p => p.Balance).HasDefaultValue(0.00);
           // builder.Property(p => p.ShopName).HasMaxLength(500).IsRequired();
           // builder.Property(p => p.ShopAddress).HasMaxLength(500).IsRequired();
           // builder.Property(p => p.ShopImg).HasMaxLength(500).IsRequired();
            builder.Property(p => p.LicenseImg).HasMaxLength(500).IsRequired();
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.MembershipID).IsRequired();
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
