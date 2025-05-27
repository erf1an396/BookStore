using Application.Features.Author.Query.GetAll;
using Application.Features.AuthorPhoto;
using Application.Features.AuthorPhoto.Query.GetAll;
using Application.Features.Category.Query.GetAll;
using Application.Features.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Book.Query.GetByAuthorId;
using Application.Features.Book;

namespace Book.WebApplication.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMediator _mediator;

        public AuthorsController(ILogger<AuthorsController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        public async Task<IActionResult> All()
        {
            var authors = (await _mediator.Send(new AuthorGetAllQuery())).Value;


            var authorlist = new List<AuthorPhotoDto>();

            foreach ( var author in authors)
            {
                var photos = await _mediator.Send(new AuthorPhotoGetAllQuery { AuthorId = author.Id });

                authorlist.AddRange(photos.Value);
            }

            var category = await _mediator.Send(new CategoryGetAllQuery());
            List<CategoryDto> categories = category.Value;

            


            ViewBag.AuthorPhotos = authorlist;
            ViewBag.Authors = authors;
            ViewBag.Categories = categories;
            


            return View();
        }
    }
}
