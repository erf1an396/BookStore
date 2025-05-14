using Application.Features.Book.Query.GetAll;
using Application.Features.BookPhoto.Query.GetAll;
using Application.Features.BookPhoto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Category.Query.GetAll;
using Application.Features.Book.Query.GetById;
using Application.Features.Category;
using Application.Features.Category.Query.GetById;

namespace Book.WebApplication.Controllers
{
    public class BookDetailController : Controller
    {

        private readonly ILogger<BookDetailController> _logger;
        private readonly IMediator _mediator;

        public BookDetailController(ILogger<BookDetailController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            
        }

        public async Task<IActionResult> Model([FromRoute] int id)
        
        {
            

            //var category = (await _mediator.Send(new CategoryGetAllQuery())).Value;

          

            var book = (await _mediator.Send(new BookGetByIdQuery { Id = id })).Value;
            var bookPhoto = new List<BookPhotoDto>();

            var photos = await _mediator.Send(new BookPhotoGetAllQuery { BookId = book.Id });
            bookPhoto.AddRange(photos.Value);

            var categoryList = new CategoryDto();
            var category = await _mediator.Send(new CategoryGetByIdQuery { Id = book.CategoryId });
            categoryList = category.Value;



            //ViewBag.Category = category;
            ViewBag.Book = book;    
            ViewBag.BookPhoto = bookPhoto;
            ViewBag.Category = categoryList;

            
            
            return View();
        }
    }
}
