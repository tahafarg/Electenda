using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstLyer.Model
{
    internal class UserphoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("Userphone");
            builder.HasKey(UP => new { UP.UserID, UP.Phone });
            builder.Property(up => up.IsDeleted).HasDefaultValue(false);
        }
    }
}
