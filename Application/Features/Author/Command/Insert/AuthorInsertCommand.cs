using Application.Contracts;
using Application.Models;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Command.Insert
{
    public  class AuthorInsertCommand : IRequest<ApiResult>
    {
        public string Name { get; set; }

        public int Born_Year { get; set; }

        public int Book_Count { get; set; }

        public int Prize_Count { get; set; }

        public BookLanguageEnum Language { get; set; }

        public string Description { get; set; }
    }

    public class AuthorInsertCommandHandler : IRequestHandler<AuthorInsertCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public AuthorInsertCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle (AuthorInsertCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            _db.Authors.Add(new Domain.Entities.Author()
            {
                Name = request.Name,
                Born_Year = request.Born_Year,
                Book_Count = request.Book_Count,
                Prize_Count = request.Prize_Count,
                Language = request.Language,
                Description = request.Description,


            });
            
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.SavedSuccessfully);
            return result;
        }
    }
}
