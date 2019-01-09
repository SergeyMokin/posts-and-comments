using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using PostsAndCommentsInterfaces.Repositories;
using PostsAndCommentsInterfaces.Services;
using PostsAndCommentsModels;
using PostsAndCommentsModels.CreationModels;
using PostsAndCommentsModels.DatabaseModels;

namespace PostsAndCommentsTests.Setup
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
