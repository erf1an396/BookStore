using Domain.Entities;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OryPersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public  class UserPhotoConfiguration : BaseEntityTypeConfiguration<UserPhoto>

    {
        public override void Configure(EntityTypeBuilder<UserPhoto> builder)
        {
            builder.ToTable("UserPhoto");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasConversion(a => a.GeoDateTime, a => new PersianDateTime(a));

            builder.Property(x => x.LastModifiedDate).HasConversion(a => a.Value.GeoDateTime, a => PersianDateTime.Parse(a));

            builder.HasOne(b => b.ApplicationUser)
                .WithMany(c => c.UserPhoto)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
