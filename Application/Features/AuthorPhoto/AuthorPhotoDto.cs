using Application.Features.Author;
using Application.Features.Book;
using Application.Mappings;
using Application.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthorPhoto
{
    public class AuthorPhotoDto  : BaseDto , IMapFrom<Domain.Entities.AuthorPhoto>
    {
        public void Mapping (Profile profile)
        {
            profile.CreateMap<Domain.Entities.AuthorPhoto , AuthorPhotoDto>()
                .ReverseMap();
        }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public string Extenstion { get; set; }



        public AuthorDto Author { get; set; }
    }
}
