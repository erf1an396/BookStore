using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OryPersianDateTime;

namespace Infrastructure.Persistence.Configuration.Base
{
    public class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");

            builder.Property(x => x.CreatedDate)
                .HasConversion(a => a.GeoDateTime, a => new OryPersianDateTime.PersianDateTime(a));

            builder.Property(x => x.LastModifiedDate)
                .HasConversion(a => a.Value.GeoDateTime , a=> PersianDateTime.Parse(a));


        }
    }
}
