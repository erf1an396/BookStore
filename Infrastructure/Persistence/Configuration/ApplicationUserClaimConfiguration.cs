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
    public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> b)
        {
            b.ToTable("ApplicationUserClaim");

            b.HasOne(p => p.ApplicationUser)
             .WithMany(t => t.ApplicationUserClaims)
             .HasForeignKey(f => f.UserId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
