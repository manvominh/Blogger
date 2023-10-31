﻿using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IGenericRepository<Post> _postRepository;

        public PostRepository(IGenericRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postRepository.Entities
                .Include(x => x.User)
                .Where(x => x.IsPublished)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
        {
            return await _postRepository.Entities
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _postRepository.Entities
                .Include(x => x.Comments)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == postId);
        }
    }
}
