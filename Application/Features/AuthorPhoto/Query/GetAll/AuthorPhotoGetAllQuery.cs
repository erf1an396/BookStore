using Application.Contracts;
using Application.Features.BookPhoto.Query.GetAll;
using Application.Features.BookPhoto;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorPhoto.Query.GetAll
{
    public class AuthorPhotoGetAllQuery : IRequest<ApiResult<List<AuthorPhotoDto>>>
    {
        public int AuthorId {  get; set; }
    }

    public class AuthorPhotoGetAllQueryHandler : IRequestHandler<AuthorPhotoGetAllQuery  ,  ApiResult<List<AuthorPhotoDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public AuthorPhotoGetAllQueryHandler(IBookStoreContext db , IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AuthorPhotoDto>>> Handle(AuthorPhotoGetAllQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<AuthorPhotoDto>> result = new();

            List<Domain.Entities.AuthorPhoto> data = await _db.AuthorPhotos.Where(x => x.AuthorId == request.AuthorId).ToListAsync();

            result.Value = _mapper.Map<List<AuthorPhotoDto>>(data);
            result.Success();
            return result;
        }
    }
}
