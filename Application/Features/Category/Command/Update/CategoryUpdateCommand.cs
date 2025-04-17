using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Update
{
    public class CategoryUpdateCommand : IRequest<ApiResult>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public CategoryUpdateCommandHandler(IBookStoreContext db)
        {
            
            _db = db;
        }

        public async Task<ApiResult> Handle(CategoryUpdateCommand request ,CancellationToken  cancellationToken)
        {
            ApiResult result = new();
            var category = await _db.Categories.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (category == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }
            category.Title = request.Title;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.UpdateSuccessfully);
            return result;
            

        }
    }
}
