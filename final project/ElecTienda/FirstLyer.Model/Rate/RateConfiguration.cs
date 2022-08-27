using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rate");
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.Review).HasMaxLength(1000);
            builder.Property(r => r.Rating).HasPrecision(2,1);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
            builder.Property(r => r.ProductID).IsRequired(false);
            builder.Property(r => r.ServicesId).IsRequired(false);
       }
    }
}
