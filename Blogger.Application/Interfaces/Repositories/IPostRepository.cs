using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetPostsByUserId(int userId);
        Task<Post> GetPostById(int postId);
    }
}
