using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _postRepository.GetPostById(postId);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
        {
            return await _postRepository.GetPostsByUserId(userId);
        }

		public async Task<(bool IsPostSaved, Post post)> SavePost(PostDto postDto)
		{
            var post = new Post()
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Introduction = postDto.Introduction,
                BodyText = postDto.BodyText,
                IsPublished = postDto.IsPublished,
                UserId = postDto.UserId,
                CreatedDate = DateTime.UtcNow,
            };
            if (postDto.Id == 0)
                await _unitOfWork.Repository<Post>().AddAsync(post);
            else
                await _unitOfWork.Repository<Post>().UpdateAsync(post);
            //await _unitOfWork.Repository<Post>().AddAsync(post);
            await _unitOfWork.Save();
			return (true, post);
		}

		public async Task<(bool IsPostUpdated, string Message)> UpdatePost(PostDto postDto)
		{
            var post = await _postRepository.GetPostById(postDto.Id);
            post.Title = postDto.Title;
            post.Introduction = postDto.Introduction;
            post.BodyText = postDto.BodyText;
            post.IsPublished = postDto.IsPublished;
            post.UserId = postDto.UserId;
			
			await _unitOfWork.Repository<Post>().UpdateAsync(post);
			await _unitOfWork.Save();
			return (true, "Success");
		}
	}
}
