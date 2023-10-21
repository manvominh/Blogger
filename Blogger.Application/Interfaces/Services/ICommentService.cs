using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
    }
}
