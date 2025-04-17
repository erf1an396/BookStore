using Application.Mappings;
using Application.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category
{
    public class CategoryDto : BaseDto , IMapFrom<Domain.Entities.Category>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Category, CategoryDto>()
                .ReverseMap();
        }

        public int? ParentId { get; set; }
        public string Title { get; set; }

        public CategoryDto CategoryParent { get; set; }
        public List<CategoryDto> CategoryChildren { get; set; }
       
    }
}
