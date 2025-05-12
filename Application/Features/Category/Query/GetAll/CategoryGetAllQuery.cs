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

namespace Application.Features.Category.Query.GetAll
{
    public class CategoryGetAllQuery : IRequest<ApiResult<List<CategoryDto>>>
    {

    }

    public class CategoryGetAllQueryHandler : IRequestHandler<CategoryGetAllQuery, ApiResult<List<CategoryDto>>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public CategoryGetAllQueryHandler(IBookStoreContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<CategoryDto>>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<CategoryDto>> result = new();

            var data = await _db.Categories.Where(x => x.ParentId == null).ToListAsync(cancellationToken);
            result.Value = _mapper.Map<List<CategoryDto>>(data);
            result.Success();
            return result;

        }
}

}
