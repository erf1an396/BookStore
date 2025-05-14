using Application.Features.Book;
using Application.Mappings;
using Application.Models;
using AutoMapper;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author
{
    public class AuthorDto : BaseDto , IMapFrom<Domain.Entities.Author> 
    {
        public void Mapping (Profile profile)
        {
            profile.CreateMap<Domain.Entities.Author, AuthorDto>()
                .ReverseMap();
        }

        public string Name { get; set; }

        public int Born_Year { get; set; }

        public BookLanguageEnum Language { get; set; }

        public string Description { get; set; }

        public int Book_Count { get; set; }

        public int Prize_Count { get; set; }

        public ICollection<BookDto> Books { get; set; }
    }
}
