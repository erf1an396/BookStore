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

namespace Application.Features.AuthorPhoto.Query.GetById
{
    public class AuthorPhotoGetByIdQuery : IRequest<ApiResult<AuthorPhotoDto>>
    {
        public int Id { get; set; }

    }

    public class AuthorPhotoGetByIdQueryHandler : IRequestHandler<AuthorPhotoGetByIdQuery , ApiResult<AuthorPhotoDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public AuthorPhotoGetByIdQueryHandler(IBookStoreContext db  , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<AuthorPhotoDto>> Handle (AuthorPhotoGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<AuthorPhotoDto> result = new();

            var entity  = await _db.BookPhotos.FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);   

            if (entity == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            result.Value = _mapper.Map<AuthorPhotoDto>(entity);
            result.Success();
            return result;
        }
    }
}
