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
using System.Xml.XPath;

namespace Application.Features.Author.Query.GetAll
{
    public  class AuthorGetAllQuery : IRequest<ApiResult<List<AuthorDto>>>
    {

    }

    public class AuthorGetAllQueryHandler : IRequestHandler<AuthorGetAllQuery, ApiResult<List<AuthorDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public AuthorGetAllQueryHandler(IBookStoreContext db , IMapper mapper )
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<ApiResult<List<AuthorDto>>> Handle (AuthorGetAllQuery request , CancellationToken cancellationToken)
        {
            ApiResult<List<AuthorDto>> result = new();

            List<Domain.Entities.Author> data = await _db.Authors.ToListAsync(cancellationToken);

            result.Value = _mapper.Map<List<AuthorDto>>(data);
            result.Success();
            return result;
        }
    }
}
