using Application.Contracts;
using Application.Features.Book.Query.GetAll;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookPhoto.Query.GetAll
{
    public class BookPhotoGetAllQuery : IRequest<ApiResult<List<BookPhotoDto>>>
    {
    }

    public class BookGetAllQueryHandler : IRequestHandler<BookPhotoGetAllQuery , ApiResult<List<BookPhotoDto>>> 
    {
        private readonly IBookStoreContext _db;

        private readonly IMapper _mapper;

        public BookGetAllQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<BookPhotoDto>>> Handle (BookPhotoGetAllQuery request , CancellationToken cancellationToken)
        {
            ApiResult<List<BookPhotoDto>> result = new();

            var data = await _db.BookPhotos.ToListAsync(cancellationToken);

            result.Value = _mapper.Map<List<BookPhotoDto>>(data);
            result.Success();
            return result;
        }

    }
}
