using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    internal class CategoryBrandConfiguration : IEntityTypeConfiguration<CategoryBrand>
    {
        public void Configure(EntityTypeBuilder<CategoryBrand> builder)
        {
            builder.ToTable("CategoryBrand");
            builder.HasKey(cb=>new {cb.BrandId,cb.CategoryId });
            builder.Property(cb => cb.BrandId).IsRequired();
            builder.Property(cb => cb.CategoryId).IsRequired();
            builder.Property(cb => cb.IsDeleted).HasDefaultValue(false);
        }
    }
}
