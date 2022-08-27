using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class ImageConfigration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");
            builder.HasKey(i => new {i.ProductID,i.Src} );
            builder.Property(i => i.Src).HasMaxLength(500);
            builder.Property(i => i.IsDeleted).HasDefaultValue(false);
        }
    }
}
