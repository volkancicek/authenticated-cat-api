using CAAS.Services;
using CAAS.Tests.Helpers;
using Moq;
using SixLabors.ImageSharp;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CAAS.Tests.Services
{
    public class CatServiceTest
    {
        private readonly Mock<ICataasService> _mockCataasService;
        private readonly Mock<IImageService> _mockImageService;
        private readonly ICatService _catService;

        public CatServiceTest()
        {
            _mockCataasService = new Mock<ICataasService>();
            _mockImageService = new Mock<IImageService>();
            _catService = new CatService(_mockCataasService.Object, _mockImageService.Object);
        }

        [Fact]
        public void Should_get_random_cat()
        {
            var expectedBytes = TestHelper.GetRandomBytes();
            var mockImage = new Mock<Image>(null, null, null, null);
            _mockCataasService.Setup(c => c.GetRandomCatImage()).Returns(Task.FromResult(expectedBytes));
            _mockImageService.Setup(i => i.UpsideDownImage(expectedBytes)).Returns(mockImage.Object);
            _mockImageService.Setup(i => i.ConvertImageToByteArray(mockImage.Object)).Returns(expectedBytes);

            var result = _catService.GetRandomCatImage();

            Assert.NotNull(result);
            Assert.IsType<byte[]>(result.Result);
            Assert.Equal(expectedBytes, result.Result);
        }
    }
}
