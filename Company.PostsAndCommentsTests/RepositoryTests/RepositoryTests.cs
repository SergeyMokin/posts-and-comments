using System;
using System.Linq;
using Company.PostsAndCommentsModels.CustomExceptions;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsRepositories.DbContext;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsRepositories.Repositories;
using Company.PostsAndCommentsTests.TestData;
using Xunit;

namespace Company.PostsAndCommentsTests.RepositoryTests
{
    public class RepositoryTests: IDisposable
    {
        private readonly PacDbContext _context;
        private readonly IRepository<User> _userRepository;

        public RepositoryTests()
        {
            _context = TestDb.GetDbContext($"RepositoryTestDb {DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds}");
            _userRepository = new Repository<User>(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(DisplayName = "Get() should return 5 objects.")]
        public void Repository_Get_Should_Return_5_Objects()
        {
            //Arrange
            const int expectedCount = 5;

            //Act
            var actualCount = _userRepository.Get().Count();

            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact(DisplayName = "Get(2) should return the second object.")]
        public async void Repository_Get_1_Should_Return_Second_Object()
        {
            //Arrange
            const int expectedId = 2;

            //Act
            var actualId = (await _userRepository.Get(2)).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact(DisplayName = "Add(model) should return valid model.")]
        public async void Repository_Add_Model_Should_Return_Valid_Model()
        {
            //Arrange
            const int expectedId = 6;

            //Act
            var actualId = (await _userRepository.Add(new User { Id = 6 })).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact(DisplayName = "Add(null) should return ArgumentNullException.")]
        public async void Repository_Add_Null_Should_Return_ArgumentNullException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userRepository.Add(null));
        }

        [Fact(DisplayName = "Edit(model) should return valid model.")]
        public async void Repository_Edit_Model_Should_Return_Valid_Model()
        {
            //Arrange
            const int expectedId = 5;

            //Act
            var actualId = (await _userRepository.Edit(new User { Id = 5 })).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact(DisplayName = "Edit(null) should return NullReferenceException.")]
        public async void Repository_Edit_Null_Should_Return_NullReferenceException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _userRepository.Edit(null));
        }

        [Fact(DisplayName = "Edit(7) should return PacNotFoundException.")]
        public async void Repository_Edit_7_Should_Return_PacNotFoundException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacNotFoundException>(async () => await _userRepository.Edit(new User { Id = 7 }));
        }

        [Fact(DisplayName = "Delete(3) should return valid model.")]
        public async void Repository_Delete_Model_Should_Return_Valid_Model()
        {
            //Arrange
            const int expectedId = 3;

            //Act
            var actualId = (await _userRepository.Delete(3)).Id;

            //Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact(DisplayName = "Delete(7) should return PacNotFoundException.")]
        public async void Repository_Delete_7_Should_Return_PacNotFoundException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<PacNotFoundException>(async () => await _userRepository.Delete(7));
        }
    }
}
