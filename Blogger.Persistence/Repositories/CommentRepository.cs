using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId)
        {
            return await _unitOfWork.Repository<Comment>().Entities
                .Include(x => x.User)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .Where(x => x.PostId == postId).ToListAsync();
        }
    }
}
