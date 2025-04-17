using Application.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class QueryableExtention
    {
        public static async Task<PaginatedList<T>> GetPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber = 0, int pageSize = 0, CancellationToken cancellationToken = default(CancellationToken))
        {

            int count = await query.CountAsync(cancellationToken);

            int skipCount = 0;
            if (pageSize > 0)
            {
                skipCount = pageNumber * pageSize;
                query = query.Skip(skipCount);
                if (pageSize != int.MaxValue) query = query.Take(pageSize);
            }

            return new PaginatedList<T>(await query.ToListAsync(cancellationToken), count, skipCount, pageSize);
        }
        public static async Task<PaginatedList<K>> GetPaginatedListWithMapAsync<T, K>(this IQueryable<T> query, IMapper mapper, int pageNumber = 0, int pageSize = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            int count = await query.CountAsync(cancellationToken);

            int skipCount = 0;
            if (pageSize > 0)
            {
                skipCount = pageNumber * pageSize;
                query = query.Skip(skipCount);
                if (pageSize != int.MaxValue)
                    query = query.Take(pageSize);
            }
            if (count == 0)
            {
                return new PaginatedList<K>([], count, skipCount, pageSize);
            }

            return new PaginatedList<K>(mapper.Map<List<K>>(await query.ToListAsync(cancellationToken)), count, skipCount, pageSize);
        }
    }
}
