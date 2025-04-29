using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.Delete
{
    public class BookDeleteCommand : IRequest<ApiResult>
    {
        public int Id  { get; set; }
    }

    public class BookDeleteCommandHandler : IRequestHandler<BookDeleteCommand,ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookDeleteCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle(BookDeleteCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var book = await _db.Books.SingleOrDefaultAsync(c =>c.Id == request.Id);
            if (book == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);   
                return result;
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;
        }
    }
}
