using System.Security.Claims;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsServices.Interfaces;
using Company.PostsAndCommentsServices.Services;
using Xunit;

namespace Company.PostsAndCommentsTests.ServiceTests
{
    public class AuthorizeServiceTests
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthorizeServiceTests()
        {
            _authorizeService = new AuthorizeService();
        }

        [Fact]
        public void GetUserId_Valid_Should_Return_UserId()
        {
            //Arrange
            const int expectedUserId = 15;
            var claimPrincipals = new ClaimsPrincipal(new[]
                {new ClaimsIdentity(new[] {new Claim("Id", $"{expectedUserId}")})});

            //Act
            var actualUserId = _authorizeService.GetUserId(claimPrincipals);

            //Assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public void GetUserId_Invalid_Should_Throw_PacUnauthorizedAccessException()
        {
            //Arrange
            var claimPrincipals = new ClaimsPrincipal();

            //Act & Assert
            Assert.Throws<PacUnauthorizedAccessException>(() => _authorizeService.GetUserId(claimPrincipals));
        }

        [Fact]
        public void HashPassword_Valid_Should_Return_Hashed_Password()
        {
            //Arrange
            const string passwordToHash = "RandomPassword1234!";

            //Act
            var actualHashedPassword = _authorizeService.HashPassword(passwordToHash);

            //Assert
            Assert.NotEqual(passwordToHash, actualHashedPassword);
        }

        [Fact]
        public void HashPassword_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            Assert.Throws<PacInvalidModelException>(() => _authorizeService.HashPassword(null));
        }

        [Fact]
        public void VerifyHashedPassword_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            Assert.Throws<PacInvalidModelException>(() => _authorizeService.VerifyHashedPassword("random", null));
            Assert.Throws<PacInvalidModelException>(() => _authorizeService.VerifyHashedPassword(null, "random"));
            Assert.Throws<PacInvalidModelException>(() => _authorizeService.VerifyHashedPassword(null, null));
        }

        [Fact]
        public void GetToken_Valid_Should_Return_UserToken()
        {
            //Arrange
            var user = new User
            {
                Id = 1, Email = "fedorov@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(),
                Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 1
            };

            //Act
            var actual = _authorizeService.GetToken(user);

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<UserToken>(actual);
        }

        [Fact]
        public void GetToken_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            Assert.Throws<PacInvalidModelException>(() => _authorizeService.GetToken(null));
        }
    }
}
