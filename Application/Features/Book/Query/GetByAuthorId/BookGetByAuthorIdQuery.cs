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

namespace Application.Features.Book.Query.GetByAuthorId
{
    public  class BookGetByAuthorIdQuery : IRequest<ApiResult<List<BookDto>>>
    {
        public int AuthorId { get; set; }
    }

    public class BookGetByAuthorIdQueryHandler : IRequestHandler< BookGetByAuthorIdQuery ,ApiResult<List<BookDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public BookGetByAuthorIdQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            
        }

        public async Task<ApiResult<List<BookDto>>> Handle(BookGetByAuthorIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<List<BookDto>> result = new();

            var book = await _db.Books.Where(x => x.AuthorId == request.AuthorId).Include(x => x.Category).Include(x => x.BookPhtotos)
                .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Publication_Year = b.Publication_Year,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Title,
                Price = b.Price,
                Pages = b.Pages,
                PhotoUrls = b.BookPhtotos.Select(p => $"/img/BookPhoto/{p.Id}.webp").ToList(),
                Publisher = b.Publisher


            }).ToListAsync(cancellationToken);


            result.Value = book;
            result.Success();
            return result;
           
        }
    }




}
