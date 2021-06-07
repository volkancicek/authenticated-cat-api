using CAAS.Controllers;
using CAAS.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CAAS.Tests.Controllers
{
    public class CatControllerTest
    {
        private readonly Mock<ICatService> _mockCatService;
        private readonly CatController _catController;
        public CatControllerTest()
        {
            _mockCatService = new Mock<ICatService>();
            _catController = new CatController(_mockCatService.Object);
        }

        [Fact]
        public void Should_get_random_cat()
        {
            var result =  _catController.Get();
            Assert.NotNull(result);
            Assert.IsType<FileContentResult>(result.Result);
        }
    }
}
