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

namespace Application.Features.UserPhoto.Query.GetAll
{
    public class UserPhotoGetAllQuery : IRequest<ApiResult<List<UserPhotoDto>>>
    {
        public Guid UserId { get; set; }
    }

    public class UserPhotoGetAllQueryHandler : IRequestHandler<UserPhotoGetAllQuery, ApiResult<List<UserPhotoDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;


        public UserPhotoGetAllQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<ApiResult<List<UserPhotoDto>>> Handle(UserPhotoGetAllQuery request, CancellationToken cancellationToken)
        {

            ApiResult<List<UserPhotoDto>> result = new();

            List<Domain.Entities.UserPhoto> data = await _db.UserPhotos.Where(x => x.UserId == request.UserId).ToListAsync();


            result.Value = _mapper.Map<List<UserPhotoDto>>(data);
            result.Success();
            return result;

            
        }
    }
}
