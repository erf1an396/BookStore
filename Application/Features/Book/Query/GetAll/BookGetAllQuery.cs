using Application.Contracts;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Query.GetAll
{
    public  class BookGetAllQuery : IRequest<ApiResult<List<BookDto>>>
    {
    }

    public class BookGetAllQueryHandler : IRequestHandler<BookGetAllQuery, ApiResult<List<BookDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public BookGetAllQueryHandler(IBookStoreContext db , IMapper mapper )
        {
               _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<BookDto>>> Handle (BookGetAllQuery request , CancellationToken cancellationToken)
        {
            ApiResult<List<BookDto>> result = new();

            var data = await _db.Books.ToListAsync(cancellationToken);
            result.Value = _mapper.Map<List<BookDto>>( data );
            result.Success();
            return result;  

        }
    }
}
