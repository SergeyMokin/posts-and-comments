using System.Threading.Tasks;
using PostsAndCommentsModels.CreationModels;

namespace PostsAndCommentsInterfaces.Services
{
    public interface IEmailService
    {
        Task Send(Mail mail);
    }
}
