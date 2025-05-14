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

namespace Application.Features.Author.Query.GetById
{
    public class AuthorGetByIdQuery :  IRequest<ApiResult<AuthorDto>>
    {
        public int Id { get; set; }

    }

    public class AuthorGetByIdQueryHandler : IRequestHandler<AuthorGetByIdQuery , ApiResult<AuthorDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public AuthorGetByIdQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<AuthorDto>> Handle (AuthorGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<AuthorDto> apiResult = new();

            var author = await _db.Authors.Where(b => b.Id == request.Id).Select(b => new AuthorDto
            {
                Id = b.Id,
                Name = b.Name,
                Born_Year = b.Born_Year,
                Book_Count = b.Book_Count,
                Description = b.Description,
                Prize_Count = b.Prize_Count,
                Language = b.Language,
                


            }).FirstOrDefaultAsync(cancellationToken);

            apiResult.Value = author;
            apiResult.Success();
            return apiResult;
        }
    }

}
