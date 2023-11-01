using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostDto>> GetAll()
        {
            return await _unitOfWork.Repository<Post>().Entities
                .Include(x => x.User)
                .Where(x => x.IsPublished)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserId(int userId)
        {
            return await _unitOfWork.Repository<Post>().Entities
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            return await _unitOfWork.Repository<Post>().Entities
                .Include(x => x.Comments)
                    .ThenInclude(x => x.User)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == postId);
        }
    }
}
