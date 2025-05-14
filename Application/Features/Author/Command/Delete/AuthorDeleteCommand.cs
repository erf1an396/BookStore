using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Command.Delete
{
    public class AuthorDeleteCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }

    }
    public class AuthorDeleteCommandHandler : IRequestHandler<AuthorDeleteCommand, ApiResult>
    {
        private readonly IBookStoreContext _db;

        public AuthorDeleteCommandHandler(IBookStoreContext db)

        {
            _db = db;
        }

        public async Task<ApiResult> Handle(AuthorDeleteCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var Author = await _db.Authors.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (Author == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            _db.Authors.Remove(Author);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;
        }
    }
}
