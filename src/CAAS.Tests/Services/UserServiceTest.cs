using CAAS.Data;
using CAAS.Entities;
using CAAS.Services;
using Moq;
using System;
using Xunit;

namespace CAAS.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IPasswordService> _mockPasswordService;
        private IUserService _userService;

        public UserServiceTest()
        {
            _mockPasswordService = new Mock<IPasswordService>();
            var _dataContext = new DataContext(null);
            _userService = new UserService(_dataContext, _mockPasswordService.Object);
        }


        [Fact]
        public void Should_create_user()
        {
            var user = new User() { Id = 1, Name = "testName", SurName = "testSurname", Username = "FirstUser", PasswordHash = null, PasswordSalt = null };

            var result = _userService.Create(user, "password");

            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public void Should_not_create_user_with_empty_password()
        {
            var user = new User() { Id = 2, Name = "testName", SurName = "testSurname", Username = "SecondUser", PasswordHash = null, PasswordSalt = null };

            Assert.Throws<Exception>(() => _userService.Create(user, ""));
        }

        [Fact]
        public void Should_not_create_user_with_existing_user_name()
        {
            var user = new User() { Id = 10, Name = "testName", SurName = "testSurname", Username = "FirstUser", PasswordHash = null, PasswordSalt = null };

            Assert.Throws<Exception>(() => _userService.Create(user, ""));
        }

        [Fact]
        public void Should_authenticate_user()
        {
            var expectedUser = new User() { Id = 3, Name = "testName", SurName = "testSurname", Username = "ThirdUser", PasswordHash = null, PasswordSalt = null };
            var password = "password";
            _mockPasswordService.Setup(p => p.VerifyPasswordHash(password, null, null)).Returns(true);
            _userService.Create(expectedUser, password);

            var result = _userService.Authenticate("ThirdUser", password);

            Assert.NotNull(result);
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public void Should_get_user_by_id()
        {
            var expectedUser = new User() { Id = 4, Name = "testName", SurName = "testSurname", Username = "FourthUser", PasswordHash = null, PasswordSalt = null };
            var user = _userService.Create(expectedUser, "password");

            var result = _userService.GetById(user.Id);
            Assert.NotNull(result);
            Assert.Equal(result, expectedUser);
        }
    }
}
