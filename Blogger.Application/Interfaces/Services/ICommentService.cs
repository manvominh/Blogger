using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<(bool IsCommentSaved, Comment comment)> SaveComment(CommentDto comment);
        Task<bool> DeleteComment(int commentId);
    }
}
