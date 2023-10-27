using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;

namespace Blogger.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;   
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await _commentRepository.GetCommentsByPostId(postId);
        }

        public async Task<(bool IsCommentSaved, Comment comment)> SaveComment(CommentDto commentDto)
        {
            var comment = new Comment()
            {
                PostId = commentDto.PostId,
                UserId = commentDto.UserId,
                CommentText = commentDto.CommentText,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.Repository<Comment>().AddAsync(comment);
            await _unitOfWork.Save();
            return (true, comment);
        }
    }
}
