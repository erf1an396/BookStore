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
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> b)
        {
            b.ToTable("ApplicationUserRole");

            b.HasOne(p => p.ApplicationUser)
             .WithMany(t => t.ApplicationUserRoles)
             .HasForeignKey(f => f.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(p => p.ApplicationRole)
             .WithMany(t => t.ApplicationUserRoles)
             .HasForeignKey(f => f.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
