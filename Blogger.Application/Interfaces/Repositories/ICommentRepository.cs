
using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId);
    }
}
