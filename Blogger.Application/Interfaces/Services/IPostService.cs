using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAll();
        Task<IEnumerable<PostDto>> GetPostsByUserId(int userId);
        Task<PostDto> GetPostById(int postId);
        Task<(bool IsPostSaved, PostDto post)> SavePost(PostDto post);
		Task<(bool IsPostUpdated, string Message)> UpdatePost(PostDto post);
	}
}
