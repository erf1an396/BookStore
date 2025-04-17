using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Contracts;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage;
using OryPersianDateTime;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class BookStoreContext : KeyApiAuthorizationDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IBookStoreContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            entry.Entity.CreatedDate = PersianDateTime.Now;
                        }
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = PersianDateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            entry.Entity.CreatedDate = PersianDateTime.Now;
                        }
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = PersianDateTime.Now;
                        break;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var timeSpanConverter = new ValueConverter<TimeSpan, long>(v => v.Ticks, v => TimeSpan.FromTicks(v));

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(TimeSpan) || property.ClrType == typeof(TimeSpan?))
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion(timeSpanConverter);
                }
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }


    public class KeyApiAuthorizationDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> :
      IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
          where TUser : IdentityUser<TKey>
          where TRole : IdentityRole<TKey>
          where TKey : IEquatable<TKey>
          where TUserClaim : IdentityUserClaim<TKey>
          where TUserRole : IdentityUserRole<TKey>
          where TUserLogin : IdentityUserLogin<TKey>
          where TRoleClaim : IdentityRoleClaim<TKey>
          where TUserToken : IdentityUserToken<TKey>
    {
        public KeyApiAuthorizationDbContext(
            DbContextOptions options)
            : base(options)
        {
        }

    }
}
