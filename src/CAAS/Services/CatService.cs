using System;
using System.Threading.Tasks;


namespace CAAS.Services
{
    public interface ICatService
    {
        Task<Byte[]> GetRandomCatImage();
    }
    public class CatService : ICatService
    {
        private readonly ICataasService _cataasService;
        private readonly IImageService _imageService;
        public CatService(ICataasService cataasService, IImageService imageService)
        {
            _cataasService = cataasService;
            _imageService = imageService;
        }
        public async Task<Byte[]> GetRandomCatImage()
        {
            var imageBytes = await _cataasService.GetRandomCatImage();
            var flippedImage = _imageService.UpsideDownImage(imageBytes);
            return _imageService.ConvertImageToByteArray(flippedImage);
        }
    }
}
