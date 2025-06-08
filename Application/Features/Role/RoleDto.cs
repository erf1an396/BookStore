using Application.Mappings;
using Application.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role
{
    public class RoleDto :IMapFrom<Domain.Entities.ApplicationRole>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.ApplicationRole ,RoleDto>().ReverseMap();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
