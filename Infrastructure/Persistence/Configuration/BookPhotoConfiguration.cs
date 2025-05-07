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
    public class BookPhotoConfiguration : BaseEntityTypeConfiguration<BookPhoto>
    {

        public override void Configure(EntityTypeBuilder<BookPhoto> builder)
        {
            builder.ToTable("BookPhoto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate).HasConversion(a => a.GeoDateTime, a => new PersianDateTime(a));

            builder.Property(x => x.LastModifiedDate).HasConversion(a => a.Value.GeoDateTime, a => PersianDateTime.Parse(a));

            builder.HasOne(b => b.Book)
                .WithMany(c => c.BookPhtotos)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
        
    }
}
