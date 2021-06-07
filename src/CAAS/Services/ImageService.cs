using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace CAAS.Services
{

    public interface IImageService
    {
        Image UpsideDownImage(Byte[] imageBytes);
        Byte[] ConvertImageToByteArray(Image image);
    }
    public class ImageService : IImageService
    {
        public Image UpsideDownImage(Byte[] imageBytes)
        {
            Image image = Image.Load(imageBytes);
            Size original = image.Size();
            image.Mutate(x => x.Rotate(180));
            return image;
        }

        public Byte[] ConvertImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.SaveAsJpeg(ms);
                return ms.ToArray();
            }
        }
    }
}
