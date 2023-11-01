using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostDto>> GetAll();
        Task<IEnumerable<PostDto>> GetPostsByUserId(int userId);
        Task<PostDto> GetPostById(int postId);
    }
}
