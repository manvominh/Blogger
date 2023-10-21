using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
    }
}
