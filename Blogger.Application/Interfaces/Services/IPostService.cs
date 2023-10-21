using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetPostsByUserId(int userId);
        Task<Post> GetPostById(int postId);
    }
}
