using System;
using System.Linq;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsRepositories.DbContext;
using Company.PostsAndCommentsServices.Interfaces;
using Company.PostsAndCommentsServices.Services;
using Company.PostsAndCommentsTests.Setup;
using Company.PostsAndCommentsTests.TestData;
using Xunit;

namespace Company.PostsAndCommentsTests.ServiceTests
{
    public class PostServiceTests: IDisposable
    {
        private readonly IPostService _postService;
        private readonly PacDbContext _context;

        public PostServiceTests()
        {
            _context = TestDb.GetDbContext($"PostServiceTestDb {DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds}");
            _postService = new PostService(MockInitializer.GetRepository(_context.Posts),
                MockInitializer.GetRepository(_context.Likes),
                MockInitializer.GetRepository(_context.Users),
                MockInitializer.GetImageService(),
                MockInitializer.GetAuthorizeService(),
                MockInitializer.GetAccessor());
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void Get_Should_Return_5_Objects()
        {
            //Arrange
            const int expectedCount = 5;

            //Act
            var actualCount = _postService.Get().Count();

            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async void Get_1_Should_Return_First_Object()
        {
            //Arrange
            const int expectedId = 1;

            //Act
            var actualId = (await _postService.Get(1)).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async void Create_Valid_Should_Return_Object()
        {
            //Arrange
            const string expectedTitle = "6 - text";

            //Act
            var actualTitle = (await _postService.Create(new Post {Title = expectedTitle, Description = "random"})).Title;

            //Assert
            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public async void Create_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _postService.Create(null));
        }

        [Fact]
        public async void Edit_Valid_Should_Return_Object()
        {
            //Arrange
            const int expectedId = 1;

            //Act
            var actualId = (await _postService.Edit(new Post {Id = expectedId, Title = "title", Description = "random"})).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async void Edit_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _postService.Edit(null));
        }

        [Fact]
        public async void Delete_1_Should_Return_First_Object()
        {
            //Arrange
            const int expectedId = 1;

            //Act
            var actualId = (await _postService.Delete(1)).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }
    }
}
