using Application.Contracts;
using Application.Models;
using Domain.Enums;
using MediatR;

namespace Application.Features.Book.Command.Insert
{
    public class BookInsertCommand : IRequest<ApiResult>
    {
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }

        public BookLanguageEnum Language { get; set; }

        public int Pages { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public int Price { get; set; }


    }


    public class BookInsertCommandHandler : IRequestHandler<BookInsertCommand, ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookInsertCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle(BookInsertCommand request, CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();

            _db.Books.Add(new Domain.Entities.Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                Publisher = request.Publisher,
                Publication_Year = request.Publication_Year,
                Isbn = request.Isbn,
                Language = request.Language,
                Pages = request.Pages,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = request.Price,
                
            });

            await _db.SaveChangesAsync();
            apiResult.Success(ApiResultStaticMessage.SavedSuccessfully);
            return apiResult;
        }


    }
}
