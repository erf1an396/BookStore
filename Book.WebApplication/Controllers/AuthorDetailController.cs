using Application.Features.Author.Query.GetById;
using Application.Features.AuthorPhoto;
using Application.Features.AuthorPhoto.Query.GetAll;
using Application.Features.Book.Query.GetByAuthorId;
using Application.Features.Book;
using Application.Features.Book.Query.GetById;
using Application.Features.Category;
using Application.Features.Category.Query.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Category.Query.GetById;
using Application.Features.BookPhoto.Query.GetById;

namespace Book.WebApplication.Controllers
{
    public class AuthorDetailController : Controller
    {
        private readonly ILogger<AuthorDetailController> _logger;
        private readonly IMediator _mediator;

        public AuthorDetailController(ILogger<AuthorDetailController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }



        public async Task<IActionResult> Model([FromRoute] int id)
        {
            var author = (await _mediator.Send(new AuthorGetByIdQuery { Id = id })).Value;
            var authorPhoto = new List<AuthorPhotoDto>(); 

            var photos = await _mediator.Send(new AuthorPhotoGetAllQuery { AuthorId = author.Id });
            authorPhoto.AddRange(photos.Value);


            var category = await _mediator.Send(new CategoryGetAllQuery());
            List<CategoryDto> categories = category.Value;

            



            var book = await _mediator.Send(new BookGetByAuthorIdQuery { AuthorId = id} );
            List<BookDto> books = book.Value;





            ViewBag.AuthorPhoto = authorPhoto;
            ViewBag.Author = author;
            ViewBag.Categories = categories;
            ViewBag.Books = books;

            return View();
        }
    }
}
