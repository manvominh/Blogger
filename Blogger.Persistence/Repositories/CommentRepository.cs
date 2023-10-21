using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        public CommentRepository(IGenericRepository<Comment> commentRepository)
        {
            this._commentRepository = commentRepository;
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await _commentRepository.Entities
                .Include(x => x.User)
                .Where(x => x.PostId == postId).ToListAsync();
        }
    }
}
