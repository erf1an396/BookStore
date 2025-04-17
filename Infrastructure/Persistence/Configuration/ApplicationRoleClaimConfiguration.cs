using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> b)
        {
            b.ToTable("ApplicationRoleClaim");

            b.HasOne(p => p.ApplicationRole)
                .WithMany(t => t.ApplicationRoleClaims)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
