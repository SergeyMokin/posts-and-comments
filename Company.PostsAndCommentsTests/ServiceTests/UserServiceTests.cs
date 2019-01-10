using System;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsRepositories.DbContext;
using Company.PostsAndCommentsServices.Interfaces;
using Company.PostsAndCommentsServices.Services;
using Company.PostsAndCommentsTests.Setup;
using Company.PostsAndCommentsTests.TestData;
using Xunit;

namespace Company.PostsAndCommentsTests.ServiceTests
{
    public class UserServiceTests: IDisposable
    {
        private readonly IUserService _userService;
        private readonly PacDbContext _context;

        public UserServiceTests()
        {
            _context = TestDb.GetDbContext($"UserServiceTestDb {DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds}");
            _userService = new UserService(MockInitializer.GetRepository(_context.Users),
                MockInitializer.GetImageService(),
                MockInitializer.GetEmailService(),
                MockInitializer.GetAuthorizeService());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async void Login_Valid_Should_Return_UserToken()
        {
            //Arrange
            var user = new LoginUser
            {
                Email = "fedorov@mail.ru",
                Password = "Password1234!"
            };

            //Act
            var actual = await _userService.Login(user);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<UserToken>(actual);
        }

        [Fact]
        public async void Login_Invalid_Should_Throw_Exception()
        {
            //Arrange
            var userWithInvalidEmail = new LoginUser
            {
                Email = "fedorov@mail",
                Password = "Password123!"
            };
            var userWithInvalidPassword = new LoginUser
            {
                Email = "fedorov@mail.ru",
                Password = "password"
            };
            var notRegisteredUser = new LoginUser
            {
                Email = "any@mail.ru",
                Password = "Password123!"
            };
            var registeredWithWrongPasswordUser = new LoginUser
            {
                Email = "fedorov@mail.ru",
                Password = "Password123!"
            };

            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _userService.Login(userWithInvalidEmail));
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _userService.Login(userWithInvalidPassword));
            await Assert.ThrowsAsync<PacNotFoundException>(async () => await _userService.Login(notRegisteredUser));
            await Assert.ThrowsAsync<PacInvalidOperationException>(async () => await _userService.Login(registeredWithWrongPasswordUser));
        }

        [Fact]
        public async void Register_Valid_Should_Return_UserToken()
        {
            //Arrange
            var user = new RegistrationUser
            {
                FirstName = "FName",
                LastName = "LName",
                Email = "some_mail@list.ru",
                Password = "Default123!"
            };

            //Act
            var actual = await _userService.Register(user);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<UserToken>(actual);
        }

        [Fact]
        public async void Register_Invalid_Should_Throw_Exception()
        {
            //Arrange
            var userWithInvalidEmail = new RegistrationUser
            {
                FirstName = "FName",
                LastName = "LName",
                Email = "some_mail@listru",
                Password = "Default123!"
            };
            var userWithInvalidPassword = new RegistrationUser
            {
                FirstName = "FName",
                LastName = "LName",
                Email = "some_mail@list.ru",
                Password = "default"
            };
            var registeredUser = new RegistrationUser
            {
                FirstName = "FName",
                LastName = "LName",
                Email = "fedorov@mail.ru",
                Password = "Password1234!"
            };

            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _userService.Register(userWithInvalidEmail));
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _userService.Register(userWithInvalidPassword));
            await Assert.ThrowsAsync<PacInvalidOperationException>(async () => await _userService.Register(registeredUser));
        }
    }
}
