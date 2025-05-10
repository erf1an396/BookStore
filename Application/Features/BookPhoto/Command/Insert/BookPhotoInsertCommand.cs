using Application.Contracts;
using Application.Contracts.WebHostEnvironment;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace Application.Features.BookPhoto.Command.Insert
{
    public class BookPhotoInsertCommand : IRequest<ApiResult>
    {
        public string Name { get; set; }

       

        public int BookId { get; set; }

        public IFormFile File { get; set; }


    }

    public class BookPhotoInsertCommandHandler : IRequestHandler<BookPhotoInsertCommand,ApiResult>
    {
        private readonly IBookStoreContext _db;
        private readonly IWebHostEnvironmentAccessor _env;

        public BookPhotoInsertCommandHandler(IBookStoreContext db , IWebHostEnvironmentAccessor env)
        {
            _db = db;
            _env = env;
        }

        public async Task<ApiResult> Handle(BookPhotoInsertCommand request , CancellationToken cancellationToken)
        {
            ApiResult result = new();

            string ext = request.File.FileName.Split('.').Last();

            string ext2 = "webp";

            var res = new Domain.Entities.BookPhoto
            {
                Name = request.Name,
                BookId = request.BookId,
                Extenstion = ext2
            };
             _db.BookPhotos.Add(res);
            await  _db.SaveChangesAsync(cancellationToken);

            string savePath = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\BookPhoto";


            //var fileName = $"{res.Id}.{ext2}";
            //var fullPath = Path.Combine(savePath, fileName);


            if(!Directory.Exists(savePath))
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


            //string savePath = Path.Combine(_env.WebRootPath, "img", "BookPhoto");

            //if(!Directory.Exists(savePath))
            //{
            //    Directory.CreateDirectory(savePath);
            //}

            //var fileName = $"{res.Id}.{ext}";
            //var fullPath = Path.Combine(savePath, fileName);

            //using var stream = request.File.OpenReadStream();
            //using var image = Image.Load(stream);


            //await image.SaveAsync(fullPath, new WebpEncoder
            //{
            //    Quality = 75 
            //}, cancellationToken);

            // result.Success(ApiResultStaticMessage.SavedSuccessfully);
            //return result;


        }
    }
}
