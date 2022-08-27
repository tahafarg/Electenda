using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Membership");
            builder.HasKey(m => m.ID);
            builder.Property(m=>m.ID).ValueGeneratedOnAdd();
            builder.Property(m=>m.Type).IsRequired();
            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
        }
    }
}
