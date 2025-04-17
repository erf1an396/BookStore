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
    public class CategoryConfiguration : BaseEntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey("Id");

            builder.Property(x => x.CreatedDate)
                .HasConversion(a => a.GeoDateTime, a => new PersianDateTime(a));

            builder.Property(x => x.LastModifiedDate)
                .HasConversion(a => a.Value.GeoDateTime, a => PersianDateTime.Parse(a));

            builder.HasOne(p => p.CategoryParent)
                .WithMany(t => t.CategoryChildren)
                .HasForeignKey(f => f.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

           
        }
    }
}
