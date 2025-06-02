using Application.Mappings;
using Application.Models;
using AutoMapper;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User
{
    public class UserDto : BaseDto , IMapFrom<Domain.Entities.ApplicationUser>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.ApplicationUser , UserDto>().ReverseMap();
        }


        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string UserName { get; set; }

        public string Email { get; set; }

        public string BirthDay_Date { get; set; }

        public GenderEnum Gender { get; set; }



    }
}
