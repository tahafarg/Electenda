using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.ID);
            builder.Property(f=>f.ID).ValueGeneratedOnAdd();
            builder.Property(f => f.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.ProductID).IsRequired(false);
            builder.Property(c => c.ServicesId).IsRequired(false);
        }
    }
}
