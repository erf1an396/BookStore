using Application.Contracts;
using Application.Models;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.Update
{
    public class BookUpdateCommand : IRequest<ApiResult>
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }


        public BookLanguageEnum  Language  { get ; set; }

        public int Pages { get; set; }

        public string? Description  { get; set; }

        public int CategoryId { get; set; } 

        public string Category {  get; set; }   



    }

    public class BookUpdateCommandHandler : IRequestHandler<BookUpdateCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookUpdateCommandHandler(IBookStoreContext db)
        {
             _db = db;  
        }

        public async Task<ApiResult> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();

            var book = await _db.Books.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (book == null)
            {
                apiResult.Fail(ApiResultStaticMessage.NotFound);
                return apiResult;
            }
            book.Title = request.Title;
            book.Publisher = request.Publisher;
            if(request.Isbn != null)
            {
                book.Isbn = request.Isbn;
            }
            book.CategoryId = request.CategoryId;
            book.Publication_Year = request.Publication_Year;
            book.Description = request.Description;
            book.Pages = request.Pages;
            book.Language = request.Language;

            _db.Books.Update(book);

            await _db.SaveChangesAsync();
            apiResult.Success(ApiResultStaticMessage.UpdateSuccessfully);
            return apiResult;   


        }
    }
}
