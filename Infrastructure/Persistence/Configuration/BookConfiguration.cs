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
    public  class BookConfiguration : BaseEntityTypeConfiguration<Book>
    { 
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.CreatedDate)
                .HasConversion(a => a.GeoDateTime, a => new PersianDateTime(a));

            builder.Property(x => x.LastModifiedDate)
                .HasConversion(a => a.Value.GeoDateTime, a => PersianDateTime.Parse(a));

            builder.HasOne(b => b.Category)
                .WithMany(c => c.books)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
