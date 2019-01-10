using System;
using System.IO;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Company.PostsAndCommentsServices.Services
{
    public class ImageService: IImageService
    {
        private const string DefaultImgName = "default.png";
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepository<Image> _imageRepository;
        public ImageService(IRepository<Image> imageRepository, IHttpContextAccessor accessor)
        {
            _imageRepository = imageRepository;
            _accessor = accessor;
        }

        public async Task<Image> Get(int id)
        {
            return await _imageRepository.Get(id);
        }

        public async Task<Image> Add(string imageData, string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageData) || string.IsNullOrWhiteSpace(imageName))
            {
                return await _imageRepository.Add(new Image
                {
                    Link = new Uri(
                            $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}{_accessor.HttpContext.Request.PathBase}/{DefaultImgName}")
                        .AbsoluteUri
                });
            }

            var name = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds + imageName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", name);

            var data = Convert.FromBase64String(imageData);

            await File.WriteAllBytesAsync(path, data);

            return await _imageRepository.Add(new Image
            {
                Link = new Uri($"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}{_accessor.HttpContext.Request.PathBase}/{name}").AbsoluteUri
            });
        }

        public async Task<Image> Delete(int id)
        {
            return await _imageRepository.Delete(id);
        }
    }
}
