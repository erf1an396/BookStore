using Application.Contracts;
using Application.Models;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Application.Features.Author.Command.Update
{
    public class AuthorUpdateCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Born_Year { get; set; }

        public int Book_Count { get; set; }

        public int Prize_Count { get; set; }

        public BookLanguageEnum Language { get; set; }

        public string Description { get; set; }

       
    }

    public class AuthorUpdateCommandHandler : IRequestHandler<AuthorUpdateCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public AuthorUpdateCommandHandler(IBookStoreContext db)
        {
            _db = db;            
        }

        public async Task<ApiResult> Handle (AuthorUpdateCommand request , CancellationToken cancellationToken )
        {
            ApiResult result = new();

            var author = await _db.Authors.SingleOrDefaultAsync(x => x.Id == request.Id);

            if ( author == null )
            {

                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            author.Name = request.Name;
            author.Born_Year = request.Born_Year;
            author.Book_Count = request.Book_Count;
            author.Language = request.Language;
            author.Description = request.Description;  
            author.Prize_Count = request.Prize_Count;

            _db.Authors.Update(author);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.UpdateSuccessfully);
            return result;  

        }
    }
}
