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

namespace Application.Features.UserPhoto.Query.GetById
{
    public class UserPhotoGetByIdQuery : IRequest<ApiResult<UserPhotoDto>>
    {
        public int Id { get; set; }
    }

    public class UserPhotoGetByIdQueryHandler : IRequestHandler<UserPhotoGetByIdQuery , ApiResult<UserPhotoDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public UserPhotoGetByIdQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<UserPhotoDto>> Handle(UserPhotoGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<UserPhotoDto> result = new();

            var entity = await _db.UserPhotos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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
