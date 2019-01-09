using System;
using System.Linq;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsRepositories.DBContext;
using PostsAndCommentsRepositories.Repositories;
using PostsAndCommentsTests.TestData;
using Xunit;

namespace PostsAndCommentsTests.RepositoryTests
{
    public class RepositoryTests: IDisposable
    {
        private readonly PacDbContext _context;
        private readonly IRepository<User> _userRepository;

        public RepositoryTests()
        {
            _context = TestDb.GetDbContext();
            _userRepository = new Repository<User>(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(DisplayName = "Get() should return 5 objects.")]
        public void Repository_Get_should_return_5_objects()
        {
            var result = _userRepository.Get();

            Assert.Equal(5, result.Count());
        }

        [Fact(DisplayName = "Get(2) should return the second object.")]
        public async void Repository_Get_1_should_return_first_object()
        {
            var result = await _userRepository.Get(2);

            Assert.Equal(2, result.Id);
        }

        [Fact(DisplayName = "Add(model) should return valid model.")]
        public async void Repository_Add_model_should_return_valid_model()
        {
            var model = await _userRepository.Add(new User { Id = 6 });

            Assert.Equal(6, model.Id);
        }

        [Fact(DisplayName = "Add(null) should return argument null exception.")]
        public async void Repository_Add_null_should_return_argument_null_exception()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userRepository.Add(null));
        }

        [Fact(DisplayName = "Edit(model) should return valid model.")]
        public async void Repository_Edit_model_should_return_valid_model()
        {
            var model = await _userRepository.Edit(new User { Id = 5 });

            Assert.Equal(5, model.Id);
        }

        [Fact(DisplayName = "Edit(null) should return null reference exception.")]
        public async void Repository_Edit_null_should_return_null_reference_exception()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _userRepository.Edit(null));
        }

        [Fact(DisplayName = "Edit(7) should return ArgumentNullException.")]
        public async void Repository_Edit_7_should_return_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userRepository.Edit(new User { Id = 7 }));
        }

        [Fact(DisplayName = "Delete(3) should return valid model.")]
        public async void Repository_Delete_model_should_return_valid_model()
        {
            var model = await _userRepository.Delete(3);

            Assert.Equal(3, model.Id);
        }

        [Fact(DisplayName = "Delete(7) should return ArgumentNullException.")]
        public async void Repository_Delete_7_should_return_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userRepository.Delete(7));
        }
    }
}
