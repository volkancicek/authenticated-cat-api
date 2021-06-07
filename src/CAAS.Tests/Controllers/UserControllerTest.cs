using AutoMapper;
using CAAS.Controllers;
using CAAS.Models;
using CAAS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CAAS.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly UserController _userController;
        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _mockTokenService = new Mock<ITokenService>();
            _userController = new UserController(_mockUserService.Object, _mockMapper.Object, _mockTokenService.Object);
        }

        [Fact]
        public void Should_register_user()
        {
            var registerInput = new RegisterModel() { Name = "test", SurName = "testsohn", Password = "testPassword", Username = "tester" };
            var result = _userController.Register(registerInput);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Should_get_user_by_id()
        {
            var userEntity = new Entities.User() { Id = 1, Username = "testUser" };
            var expected = new UserModel() { Id = 1, Username = "testUser" };
            _mockUserService.Setup(u => u.GetById(1)).Returns(userEntity);
            _mockMapper.Setup(m => m.Map<UserModel>(userEntity)).Returns(expected);

            var result = _userController.GetById(1);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actual = (result as OkObjectResult).Value as UserModel;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_authenticate_user()
        {
            var loginInput = new LoginModel() { Username = "tester", Password = "test" };
            var userEntity = new Entities.User() { Id = 1, Username = "tester", Name = "test" };
            _mockUserService.Setup(u => u.Authenticate(loginInput.Username, loginInput.Password)).Returns(userEntity);
            _mockTokenService.Setup(t => t.GetToken(1)).Returns("cool token");
            var expected = new LoginResponseModel()
            {
                Id = 1,
                Username = "tester",
                FirstName = "test",
                LastName = "",
                Token = "cool token"
            };

            var result = _userController.Authenticate(loginInput);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var actual = (result as OkObjectResult).Value as LoginResponseModel;
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Token, actual.Token);
        }
    }
}
