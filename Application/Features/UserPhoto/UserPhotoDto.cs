using Application.Mappings;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserPhoto
{
    public class UserPhotoDto : BaseDto , IMapFrom<Domain.Entities.UserPhoto>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.UserPhoto, UserPhotoDto>().ReverseMap();


        }

        public string Name { get; set; }

        public string Extenstion  { get; set; }

        public Guid UserId  { get; set; }

        
    }
}
