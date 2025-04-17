using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Delete
{
    public class CategoryDeleteCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }
    }

    public class CategorDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand, ApiResult>
    {
        private readonly IBookStoreContext _db;

        public CategorDeleteCommandHandler(IBookStoreContext db)
        {
            _db = db;
        }

        public async Task<ApiResult> Handle(CategoryDeleteCommand request , CancellationToken cancellationToken)
        {

            ApiResult result = new();

            var category = await _db.Categories.SingleOrDefaultAsync(c => c.Id == request.Id );
            if (category == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;

        }
    }
}
