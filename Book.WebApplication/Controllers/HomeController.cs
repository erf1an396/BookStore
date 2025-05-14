using Application.Features.Book;
using Application.Features.Book.Query.GetAll;
using Application.Features.BookPhoto;
using Application.Features.BookPhoto.Query.GetAll;
using Application.Features.Category;
using Application.Features.Category.Query.GetById;
using Book.WebApplication.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var books = (await _mediator.Send(new BookGetAllQuery())).Value;


            // book photo  & category
            var bookList = new List<BookPhotoDto>();
            

            foreach(var book in books)
            {
                var photos = await _mediator.Send(new BookPhotoGetAllQuery { BookId = book.Id});
                
                bookList.AddRange(photos.Value);
                

            }

            // book photo & category




            ViewBag.Books = books;
            ViewBag.BookPhotos = bookList;
            
            
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
