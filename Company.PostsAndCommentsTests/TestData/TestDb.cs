using System.Collections.Generic;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsRepositories.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Company.PostsAndCommentsTests.TestData
{
    public class TestDb
    {
        public static PacDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<PacDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var context = new PacDbContext(options);

            InitializeTestData(context);

            return context;
        }

        private static void InitializeTestData(PacDbContext context)
        {
            context.Users.AddRange(new List<User>
            {
                new User {Id = 1, Email = "fedorov@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 1},
                new User {Id = 2, Email = "petrov@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 2},
                new User {Id = 3, Email = "john@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 3},
                new User {Id = 4, Email = "john2@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 4},
                new User {Id = 5, Email = "john1@mail.ru", HashedPassword = "Password1234!".GetHashCode().ToString(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 5}
            });

            for (var i = 1; i <= 5; i++)
            {
                context.Posts.Add(new Post{ Id = i, Description = $"{i} - desc", Title = $"{i}-title", ImageId = i});
                context.Images.Add(new Image { Id = i, Link = $"{i}-text" });
                context.Comments.Add(new Comment { Id = i, PostId = i, UserId = i, Text = $"{i}-text" });
                context.Likes.Add(new Like { Id = i, PostId = i, UserId = i });
            }

            context.SaveChanges();
        }
    }
}
