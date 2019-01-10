using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsModels.DatabaseModels;
using Company.PostsAndCommentsModels.ViewModels;
using Company.PostsAndCommentsRepositories.Interfaces;
using Company.PostsAndCommentsServices.Extensions;
using Company.PostsAndCommentsServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Company.PostsAndCommentsTests.Setup
{
    public class MockInitializer
    {
        public static IRepository<T> GetRepository<T>(IQueryable<T> testData)
            where T : class, IModel<T>, new()
        {
            var rep = new Mock<IRepository<T>>();
            rep.Setup(x => x.Get()).Returns(testData);
            rep.Setup(x => x.Get(It.IsAny<int>())).Returns<int>(x => Task.FromResult(new T { Id = x }));
            rep.Setup(x => x.Add(It.IsAny<T>())).Returns<T>(Task.FromResult);
            rep.Setup(x => x.Edit(It.IsAny<T>())).Returns<T>(Task.FromResult);
            rep.Setup(x => x.Delete(It.IsAny<int>())).Returns<int>(x => Task.FromResult(new T { Id = x }));
            return rep.Object;
        }

        public static IImageService GetImageService()
        {
            var service = new Mock<IImageService>();
            service.Setup(x => x.Get(It.IsAny<int>())).Returns<int>(x => Task.FromResult(new Image { Id = x }));
            service.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>((x, y) => Task.FromResult(new Image { Id = 0, Link = y }));
            service.Setup(x => x.Delete(It.IsAny<int>())).Returns<int>(x => Task.FromResult(new Image { Id = x }));
            return service.Object;
        }

        public static IEmailService GetEmailService()
        {
            var service = new Mock<IEmailService>();
            service.Setup(x => x.Send(It.IsAny<Mail>())).Returns(Task.CompletedTask);
            return service.Object;
        }

        public static IAuthorizeService GetAuthorizeService()
        {
            var service = new Mock<IAuthorizeService>();
            service.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(1);
            service.Setup(x => x.GetToken(It.IsAny<User>(), It.IsAny<IEmailService>())).Returns<User, IEmailService>((x, y) => 
                new UserToken{ User = x.ToView(), DateExpires = DateTime.Now.AddDays(31), Token = "bearer token"});
            service.Setup(x => x.HashPassword(It.IsAny<string>())).Returns<string>(x => x.GetHashCode().ToString());
            service.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) => x.Equals(y.GetHashCode().ToString()));
            return service.Object;
        }

        public static IHttpContextAccessor GetAccessor()
        {
            var claimsPrincipal = new Mock<ClaimsPrincipal>();
            var httpContext = new Mock<HttpContext>();
            var accessor = new Mock<IHttpContextAccessor>();
            
            claimsPrincipal.Setup(x => x.Claims).Returns(new[] { new Claim("Id", "1") });
            httpContext.Setup(x => x.User).Returns(claimsPrincipal.Object);
            accessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

            return accessor.Object;
        }
    }
}
