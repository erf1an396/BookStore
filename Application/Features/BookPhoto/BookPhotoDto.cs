using Application.Features.Book;
using Application.Mappings;
using Application.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookPhoto
{
    public class BookPhotoDto : BaseDto , IMapFrom<Domain.Entities.BookPhoto>
    {

        public void Mapping (Profile profile)
        {
            profile.CreateMap<Domain.Entities.BookPhoto , BookDto>().ReverseMap() ;    
        }

        public string Name { get; set; } 

        public int BookId { get; set; }

        public string Extention { get; set; }

        public BookDto Book { get; set; }

        

    }
}
