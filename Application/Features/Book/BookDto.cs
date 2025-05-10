using Application.Features.BookPhoto;
using Application.Features.Category;
using Application.Mappings;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Application.Features.Book
{
    public class BookDto : BaseDto , IMapFrom<Domain.Entities.Book>
    {

        public void Mapping (Profile profile)
        {
            profile.CreateMap<Domain.Entities.Book, BookDto>()
                .ReverseMap();
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Description { get; set; }

        public string Publisher { get; set; }

        public int Publication_Year { get; set; }

        public string? Isbn { get; set; }

        public BookLanguageEnum Language { get; set; }

        public int Pages { get; set; }

        public CategoryDto Category { get; set; }

        public int CategoryId { get; set; }

        public ICollection<BookPhotoDto> BookPhtotos { get; set; }



    }
}
