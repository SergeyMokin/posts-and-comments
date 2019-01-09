﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PostsAndCommentsModels.DatabaseModels;
using PostsAndCommentsRepositories.DBContext;
using PostsAndCommentsServices.Extensions;

namespace PostsAndCommentsTests.TestData
{
    public class TestDb
    {
        private static readonly object Lock = new object();
        private static int _counter;
        public static PacDbContext GetDbContext()
        {
            lock (Lock)
            {
                var options = new DbContextOptionsBuilder<PacDbContext>()
                    .UseInMemoryDatabase($"TestDB - {++_counter}")
                    .Options;

                var context = new PacDbContext(options);

                InitializeTestData(context);

                return context;
            }
        }

        private static void InitializeTestData(PacDbContext context)
        {
            context.Users.AddRange(new List<User>
            {
                new User {Id = 1, Email = "fedorov@mail.ru", HashedPassword = "Qwer1234!".HashPassword(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 1},
                new User {Id = 2, Email = "petrov@mail.ru", HashedPassword = "Qwer1234!".HashPassword(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 2},
                new User {Id = 3, Email = "john@mail.ru", HashedPassword = "Qwer1234!".HashPassword(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 3},
                new User {Id = 4, Email = "john2@mail.ru", HashedPassword = "Qwer1234!".HashPassword(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 4},
                new User {Id = 5, Email = "john1@mail.ru", HashedPassword = "Qwer1234!".HashPassword(), Role = "Admin", FirstName = "FName", LastName = "LName", ImageId = 5}
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