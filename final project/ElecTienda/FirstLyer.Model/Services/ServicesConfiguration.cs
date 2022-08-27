using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(s => s.ID);
            builder.Property(s => s.ID).ValueGeneratedOnAdd();
            builder.Property(s => s.ProviderID).IsRequired();
            builder.Property(s=>s.CategoryID).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(500).IsRequired();
            builder.Property(s => s.Description).HasMaxLength(1000).IsRequired();
            builder.Property(s => s.Price).IsRequired();
            builder.Property(s => s.IsActive).HasDefaultValue(false);
            builder.Property(s => s.StartDate).IsRequired();
            builder.Property(s => s.EndDate).IsRequired();
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.Src).HasMaxLength(500);
        }
    }
}
