using Application.Contracts;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Book.Command.Update
{
    public  class BookUpdateCommand : IRequest<BookUpdateCommand>
    {
    }

    public class BookUpdateCommandHandler : IRequestHandler<BookUpdateCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public BookUpdateCommandHandler(IBookStoreContext db)
        {
             _db = db;  
        }

        public async Task<ApiResult> Handle(BookUpdateCommand request , CancellationToken cancellationToken)
        {
            ApiResult apiResult = new();


        }
    }
}
