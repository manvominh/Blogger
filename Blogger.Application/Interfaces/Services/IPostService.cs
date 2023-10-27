using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IEnumerable<Post>> GetPostsByUserId(int userId);
        Task<Post> GetPostById(int postId);
        Task<(bool IsPostSaved, Post post)> SavePost(PostDto post);
		Task<(bool IsPostUpdated, string Message)> UpdatePost(PostDto post);
	}
}
