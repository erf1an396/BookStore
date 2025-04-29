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

namespace Application.Features.Category.Query.GetAllAsTree
{
    public class CategoryGetAllAsTreeQuery : IRequest<ApiResult<List<CategoryDto>>>
    {


    }

    public class CategoryGetAllAsTreeQueryHandler : IRequestHandler<CategoryGetAllAsTreeQuery , ApiResult<List<CategoryDto>>>
        {

        private readonly IBookStoreContext _db;
        private readonly IMapper _mapper;

        public CategoryGetAllAsTreeQueryHandler(IBookStoreContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<CategoryDto>>> Handle (CategoryGetAllAsTreeQuery request , CancellationToken cancellationToken)
        {

            ApiResult<List<CategoryDto>> result = new();
            var data = await _db.Categories.ToListAsync(cancellationToken);
            result.Value = BuildTree(_mapper.Map<List<CategoryDto>>(data));
            result.Success();
            return result;
        }

        //private List<CategoryDto> BuildTree(List<CategoryDto> categories)
        //{
        //    var lookup = categories.ToDictionary(c => c.Id);
        //    var rootCategories = new List<CategoryDto>();

        //    foreach (var category in categories)
        //    {
        //        if (category.ParentId.HasValue && lookup.ContainsKey(category.ParentId.Value))
        //        {
        //            lookup[category.ParentId.Value].CategoryChildren.Add(category);
        //        }
        //        else
        //        {
        //            rootCategories.Add(category);
        //        }
        //    }


        //    return rootCategories;
        //}


        private List<CategoryDto> BuildTree(List<CategoryDto> categories, int? parentId = null)
        {
            return categories.Where(c => c.ParentId == parentId)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    ParentId = c.ParentId,
                    Title = c.Title,
                    CategoryChildren =  BuildTree(categories , c.Id),
                   

                }).ToList();
        }

    }


   
}

