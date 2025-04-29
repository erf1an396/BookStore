using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.Insert
{
    public class BookInsertCommand : IRequest<ApiResult>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }

        public BookLanguageEnum Language { get; set; }

        public int pages { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }


    }

    public class BookInsertCommandHandler : IRequestHandler<BookInsertCommand ,ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookInsertCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle(BookInsertCommand request , CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();

            _db.Books.Add(new Domain.Entities.Book
            {
                Title = request.Title,
                Author = request.Author,
                Publisher = request.Publisher,
                Publication_Year = request.Publication_Year,
                Isbn = request.Isbn,
                Language = request.Language,
                pages = request.pages,
                Description = request.Description,
                CategotyId = request.CategoryId,
            });

            await _db.SaveChangesAsync();   
            apiResult.Success(ApiResultStaticMessage.SavedSuccessfully);
            return apiResult;
        }


    }
}
