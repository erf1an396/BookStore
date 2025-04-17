using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> b)
        {
            b.ToTable("ApplicationUserToken");

            b.HasOne(p => p.ApplicationUser)
             .WithMany(t => t.ApplicationUserTokens)
             .HasForeignKey(f => f.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
