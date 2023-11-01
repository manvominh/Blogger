using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId);
        Task<(bool IsCommentSaved, CommentDto comment)> SaveComment(CommentDto comment);
        Task<bool> DeleteComment(int commentId);
    }
}
