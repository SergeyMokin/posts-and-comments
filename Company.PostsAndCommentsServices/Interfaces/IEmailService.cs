using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CreationModels;

namespace Company.PostsAndCommentsServices.Interfaces
{
    public interface IEmailService
    {
        Task Send(Mail mail);
    }
}
