using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Application.Features.UserPhoto.Command.Delete
{
    public class UserPhotoDeleteCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }

    }

    public class UserPhotoDeleteCommandHandler : IRequestHandler<UserPhotoDeleteCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public UserPhotoDeleteCommandHandler(IBookStoreContext db)
        {
            _db = db;

        }

        public async  Task<ApiResult> Handle(UserPhotoDeleteCommand request, CancellationToken cancellationToken)
        {
            ApiResult result = new();


            var userPhoto = await _db.UserPhotos.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (userPhoto != null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            _db.UserPhotos.Remove(userPhoto);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;

        }
    }
}
