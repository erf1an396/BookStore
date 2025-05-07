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

namespace Application.Features.BookPhoto.Query.GetById
{
    public class BookPhotoGetByIdQuery : IRequest<ApiResult<BookPhotoDto>>
    {
        public int Id { get; set; }
    }

    public class BookPhotoGetByIdQueryHandler : IRequestHandler<BookPhotoGetByIdQuery,ApiResult<BookPhotoDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public BookPhotoGetByIdQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            
            _db = db;   
            _mapper = mapper;
        }

        public async Task<ApiResult<BookPhotoDto>> Handle(BookPhotoGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<BookPhotoDto> result = new();

            var entity = await _db.BookPhotos.FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

            if ( entity == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result; 
            }

            result.Value = _mapper.Map<BookPhotoDto>(entity);
            result.Success();
            return result;
                    
        }


    }
}
