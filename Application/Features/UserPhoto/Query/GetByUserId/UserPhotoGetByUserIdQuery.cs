using Application.Contracts;
using Application.Features.BookPhoto;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserPhoto.Query.GetByUserId
{
    public class UserPhotoGetByUserIdQuery : IRequest<ApiResult<UserPhotoDto>>
    {
        public Guid UserId { get; set; }
    }
    public class UserPhotoGetByUserIdQueryHandler : IRequestHandler<UserPhotoGetByUserIdQuery, ApiResult<UserPhotoDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public UserPhotoGetByUserIdQueryHandler(IBookStoreContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<UserPhotoDto>> Handle(UserPhotoGetByUserIdQuery request, CancellationToken cancellationToken)
        {
            ApiResult<UserPhotoDto> result = new();

            var entity = await _db.UserPhotos.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (entity == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            result.Value = _mapper.Map<UserPhotoDto>(entity);
            result.Success();
            return result;
        }
    }
}
