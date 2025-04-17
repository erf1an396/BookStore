using Application.Contracts;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Command.Insert
{
    public class CategoryInsertCommand : IRequest<ApiResult>
    {
        public int? ParentId { get; set; }

        public string Title { get; set; }

    }

    public class CategoryInsertCommandHandler : IRequestHandler<CategoryInsertCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public CategoryInsertCommandHandler(IBookStoreContext db)
        {

            _db = db;

            
        }

        public async Task<ApiResult> Handle(CategoryInsertCommand request, CancellationToken cancellationToken)
        {
            ApiResult result = new();

            _db.Categories.Add(new Domain.Entities.Category()
            {

                Title = request.Title,
                ParentId = request.ParentId,

            });
            await _db.SaveChangesAsync();
            result.Success(ApiResultStaticMessage.SavedSuccessfully);
            return result;
             
        }
    }
}
