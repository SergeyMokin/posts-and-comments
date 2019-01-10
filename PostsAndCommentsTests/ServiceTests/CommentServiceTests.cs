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
    public class CommentServiceTests: IDisposable
    {
        private readonly ICommentService _commentService;
        private readonly PacDbContext _context;

        public CommentServiceTests()
        {
            _context = TestDb.GetDbContext($"CommentServiceTestDb {DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds}");
            _commentService = new CommentService(MockInitializer.GetRepository(_context.Comments), MockInitializer.GetAccessor());
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void Get_1_should_return_1_object()
        {
            const int expected = 1;
            Assert.Equal(expected, _commentService.Get(1).Count());
        }

        [Fact]
        public async void Get_1_1_should_return_first_object()
        {
            const int expected = 1;
            Assert.Equal(expected, (await _commentService.Get(1, 1)).Id);
        }

        [Fact]
        public async void Get_1_2_should_return_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Get(1, 2));
        }

        [Fact]
        public async void Add_valid_should_return_object()
        {
            const string expected = "6 - text";
            Assert.Equal(expected, (await _commentService.Add(new Comment { PostId = 1, Text = expected })).Text);
        }

        [Fact]
        public async void Add_invalid_should_throw_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _commentService.Add(null));
        }

        [Fact]
        public async void Edit_valid_should_return_object()
        {
            const string expected = "6 - text";
            Assert.Equal(expected, (await _commentService.Edit(1, new Comment { PostId = 1, Text = expected })).Text);
        }

        [Fact]
        public async void Edit_invalid_should_throw_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _commentService.Edit(1, null));
        }

        [Fact]
        public async void Delete_1_should_return_first_object()
        {
            const int expected = 1;
            Assert.Equal(expected, (await _commentService.Delete(1)).Id);
        }
    }
}
