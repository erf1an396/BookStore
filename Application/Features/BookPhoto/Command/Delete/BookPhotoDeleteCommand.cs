using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookPhoto.Command.Delete
{
    public class BookPhotoDeleteCommand : IRequest<ApiResult>
    {

        public int Id { get; set; }

    }

    public class BookPhotoDeleteCommandHandler : IRequestHandler<BookPhotoDeleteCommand,ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookPhotoDeleteCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle(BookPhotoDeleteCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var bookPhoto = await _db.BookPhotos.FirstOrDefaultAsync(c => c.Id == request.Id);

            if(bookPhoto == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }
            _db.BookPhotos.Remove(bookPhoto);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;
        }
    }
}
