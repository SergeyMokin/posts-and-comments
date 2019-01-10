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
    public class CommentServiceTests: IDisposable
    {
        private readonly ICommentService _commentService;
        private readonly PacDbContext _context;

        public CommentServiceTests()
        {
            _context = TestDb.GetDbContext($"CommentServiceTestDb {DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds}");
            _commentService = new CommentService(MockInitializer.GetRepository(_context.Comments), MockInitializer.GetAuthorizeService(), MockInitializer.GetAccessor());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void Get_1_Should_Return_One_Object()
        {
            //Arrange
            const int expectedCount = 1;

            //Act
            var actualCount = _commentService.Get(1).Count();

            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async void Get_1_1_Should_Return_First_Object()
        {
            //Arrange
            const int expectedId = 1;

            //Act
            var actualId = (await _commentService.Get(1, 1)).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async void Get_1_2_Should_Return_PacNotFoundException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacNotFoundException>(async () => await _commentService.Get(1, 2));
        }

        [Fact]
        public async void Add_Valid_Should_Return_Object()
        {
            //Arrange
            const string expectedText = "6 - text";

            //Act
            var actualText = (await _commentService.Add(new Comment {PostId = 1, Text = expectedText})).Text;

            //Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public async void Add_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _commentService.Add(null));
        }

        [Fact]
        public async void Edit_Valid_Should_Return_Object()
        {
            //Arrange
            const string expectedText = "6 - text";

            //Act
            var actualText = (await _commentService.Edit(1, new Comment {PostId = 1, Text = expectedText})).Text;

            //Assert
            Assert.Equal(expectedText, actualText);
        }

        [Fact]
        public async void Edit_Invalid_Should_Throw_PacInvalidModelException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacInvalidModelException>(async () => await _commentService.Edit(1, null));
        }

        [Fact]
        public async void Delete_1_Should_Return_First_Object()
        {
            //Arrange
            const int expectedId = 1;

            //Act
            var actualId = (await _commentService.Delete(1)).Id;
            Assert.Equal(expectedId, actualId);
        }
    }
}
