using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthorPhoto.Command.Delete
{
    public class AuthorPhotoDeleteCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }

    }

    public class AuthorPhotoDeleteCommandHandler : IRequestHandler<AuthorPhotoDeleteCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;
        public AuthorPhotoDeleteCommandHandler(IBookStoreContext db)
        {
            _db = db;

        }

        public async Task<ApiResult> Handle (AuthorPhotoDeleteCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var authorPhoto = await _db.AuthorPhotos.FirstOrDefaultAsync(e => e.Id == request.Id);

            if (authorPhoto == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            _db.AuthorPhotos.Remove(authorPhoto);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;
        }
    }
}
