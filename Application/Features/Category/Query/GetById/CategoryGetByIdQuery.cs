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

namespace Application.Features.Category.Query.GetById
{
    public class CategoryGetByIdQuery : IRequest<ApiResult<CategoryDto>>
    {
        public int Id { get; set; }

    }

    public class CategoryGetByIdQueryHandler : IRequestHandler<CategoryGetByIdQuery , ApiResult<CategoryDto>>
    {
        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public CategoryGetByIdQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<CategoryDto>> Handle (CategoryGetByIdQuery request , CancellationToken cancellationToken)
        {
            ApiResult<CategoryDto> result = new();

            var Category = await _db.Categories.Where(x => x.Id == request.Id).Select(x => new CategoryDto
            {
                Id = request.Id,
                Title = x.Title,
                ParentId = x.ParentId,
                CategoryChildren = x.CategoryChildren.Select(child => new CategoryDto
                {
                    Id = child.Id,
                    Title = child.Title,
                    ParentId = child.ParentId
                }).ToList(),



            }).FirstOrDefaultAsync(cancellationToken);

            result.Value = Category;
            result.Success();
            return result;





        }
    }
}
