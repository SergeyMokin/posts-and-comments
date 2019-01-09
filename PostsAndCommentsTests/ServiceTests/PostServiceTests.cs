using System;
using System.Linq;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsRepositories.DBContext;
using PostsAndCommentsServices.Services;
using PostsAndCommentsTests.Setup;
using PostsAndCommentsTests.TestData;
using Xunit;

namespace PostsAndCommentsTests.ServiceTests
{
    public class PostServiceTests: IDisposable
    {
        private readonly IPostService _postService;
        private readonly PacDbContext _context;

        public PostServiceTests()
        {
            _context = TestDb.GetDbContext();
            _postService = new PostService(MockInitializer.GetRepository(_context.Posts),
                MockInitializer.GetRepository(_context.Likes),
                MockInitializer.GetRepository(_context.Users),
                MockInitializer.GetImageService(),
                MockInitializer.GetAccessor());
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void Get_should_return_5_objects()
        {
            const int expected = 5;
            Assert.Equal(expected, _postService.Get().Count());
        }

        [Fact]
        public async void Get_1_should_return_first_object()
        {
            const int expected = 1;
            Assert.Equal(expected, (await _postService.Get(1)).Id);
        }

        [Fact]
        public async void Create_valid_should_return_object()
        {
            const string expected = "6 - text";
            Assert.Equal(expected, (await _postService.Create(new Post { Title = expected, Description = "random" })).Title);
        }

        [Fact]
        public async void Create_invalid_should_throw_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _postService.Create(null));
        }

        [Fact]
        public async void Edit_valid_should_return_object()
        {
            const int expected = 1;
            Assert.Equal(expected, (await _postService.Edit(new Post { Id = expected, Title = "title", Description = "random" })).Id);
        }

        [Fact]
        public async void Edit_invalid_should_throw_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _postService.Edit(null));
        }

        [Fact]
        public async void Delete_1_should_return_first_object()
        {
            const int expected = 1;
            Assert.Equal(expected, (await _postService.Delete(1)).Id);
        }
    }
}
