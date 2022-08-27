using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.ID);
            builder.Property(c=>c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasMaxLength(500);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.ImgSrc).HasMaxLength(500);
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }
    }
}
