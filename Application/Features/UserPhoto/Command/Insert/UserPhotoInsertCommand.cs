using Application.Contracts;
using Application.Contracts.WebHostEnvironment;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.UserPhoto.Command.Insert
{
    public class UserPhotoInsertCommand : IRequest<ApiResult>
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public IFormFile File   { get; set; }

    }

    public class UserPhotoInsertCommandHandler : IRequestHandler<UserPhotoInsertCommand , ApiResult>
    {
        private readonly IBookStoreContext _db;
        private readonly IWebHostEnvironmentAccessor _env;

        public UserPhotoInsertCommandHandler(IBookStoreContext db , IWebHostEnvironmentAccessor env)
        {
            _db = db;
            _env = env;
        }

        public async Task<ApiResult> Handle(UserPhotoInsertCommand request , CancellationToken cancellationToken )
        {
            ApiResult result = new();

            string ext = request.File.FileName.Split('.').Last();
            string name = request.File.FileName.Split('.').First();

            string ext2 = "webp";

            var res = new Domain.Entities.UserPhoto
            {
                Name = name,
                UserId = request.UserId,
                Extenstion = ext2
            };

            _db.UserPhotos.Add(res);
            await _db.SaveChangesAsync(cancellationToken);

            string savePath = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\UserPhoto";




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
