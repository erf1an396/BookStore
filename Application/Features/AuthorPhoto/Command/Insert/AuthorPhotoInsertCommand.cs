using Application.Contracts;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace Application.Features.AuthorPhoto.Command.Insert
{
    public class AuthorPhotoInsertCommand : IRequest<ApiResult>
    {
        public string Name { get; set; }



        public int AuthorId { get; set; }

        public IFormFile File { get; set; }
    }

    public class AuthorPhotoInsertCommandHandler : IRequestHandler<AuthorPhotoInsertCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;

        public AuthorPhotoInsertCommandHandler(IBookStoreContext db) 
        {
            _db = db;

        }

        public async Task<ApiResult> Handle(AuthorPhotoInsertCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            string ext = request.File.FileName.Split('.').Last();

            string ext2 = "webp";

            var res = new Domain.Entities.AuthorPhoto
            {
                Name = request.Name,
                AuthorId = request.AuthorId,
                Extenstion = ext2
            };

            _db.AuthorPhotos.Add(res);
            await _db.SaveChangesAsync(cancellationToken);

            string savePath = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\AuthorPhoto";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string fileName = $"{res.Id}.{ext2}";
            string fullPath = Path.Combine(savePath, fileName);

            using var stream = request.File.OpenReadStream();
            using var image = Image.Load(stream);

            await image.SaveAsync(fullPath, new WebpEncoder
            {
                Quality = 75
            }, cancellationToken);

            result.Success(ApiResultStaticMessage.SavedSuccessfully);
            return result;
        }
    }
}
