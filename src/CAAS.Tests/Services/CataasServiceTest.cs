using CAAS.Services;
using CAAS.Tests.Helpers;
using Moq;
using Moq.Protected;
using SixLabors.ImageSharp;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CAAS.Tests.Services
{
    public class CataasServiceTest
    {
        private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly ICataasService _cataasService;
        public CataasServiceTest()
        {
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _cataasService = new CataasService(_mockClientFactory.Object);
        }

        [Fact]
        public void Should_get_random_cat_image()
        {
            var expectedBytes = TestHelper.GetRandomBytes();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new ByteArrayContent(expectedBytes),
                    });
            var mockClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockClientFactory.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(mockClient);

            var result = _cataasService.GetRandomCatImage();

            Assert.NotNull(result);
            Assert.Equal(expectedBytes, result.Result);
        }
    }
}
