using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Query.GetById
{
    public class BookGetByIdQuery : IRequest<ApiResult<BookDto>>
    {
        public int Id { get; set; }
    }

    public class BookGetByIdQueryHandler : IRequestHandler<BookGetByIdQuery , ApiResult<BookDto>>
    {
        private readonly IBookStoreContext _db;

        public BookGetByIdQueryHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult<BookDto>> Handle(BookGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<BookDto> result = new();

            var book = await _db.Books.Where(b => b.Id == request.Id).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Publication_Year = b.Publication_Year,
                Publisher = b.Publisher,
                Isbn = b.Isbn,
                Pages = b.Pages,
                Description = b.Description,
                Language = b.Language,
                CategoryId = b.CategoryId,

            }).FirstOrDefaultAsync(cancellationToken);

            result.Value = book;
            result.Success();
            return result;
        }
    }

}
