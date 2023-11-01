using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;

namespace Blogger.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
		private readonly IUnitOfWork _unitOfWork;
		public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PostDto>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            return await _postRepository.GetPostById(postId);
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserId(int userId)
        {
            return await _postRepository.GetPostsByUserId(userId);
        }

		public async Task<(bool IsPostSaved, PostDto post)> SavePost(PostDto postDto)
		{
            var post = new Post()
            {
                Title = postDto.Title,
                Introduction = postDto.Introduction,
                BodyText = postDto.BodyText,
			    ImageUrl = postDto.ImageUrl,
			    IsPublished = postDto.IsPublished,
                UserId = postDto.UserId,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.Repository<Post>().AddAsync(post);
            await _unitOfWork.Save();
            postDto.Id = post.Id;
			return (true, postDto);
		}

		public async Task<(bool IsPostUpdated, string Message)> UpdatePost(PostDto postDto)
		{
            var currentPostDto = await _postRepository.GetPostById(postDto.Id);
            var post = new Post()
            {
                Id = currentPostDto.Id,
                Title = currentPostDto.Title,
                Introduction = currentPostDto.Introduction,
                BodyText = currentPostDto.BodyText,
                ImageUrl = currentPostDto.ImageUrl,
                IsPublished = currentPostDto.IsPublished,
                UserId = currentPostDto.UserId,
            };
			await _unitOfWork.Repository<Post>().UpdateAsync(post);
			await _unitOfWork.Save();
			return (true, "Success");
		}
	}
}
