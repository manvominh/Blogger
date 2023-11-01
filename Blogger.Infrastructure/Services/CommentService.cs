using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _unitOfWork.Repository<Comment>().Entities.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null) { return false; }
            await _unitOfWork.Repository<Comment>().DeleteAsync(comment);
            await _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostId(int postId)
        {
            return await _commentRepository.GetCommentsByPostId(postId);
        }

        public async Task<(bool IsCommentSaved, CommentDto comment)> SaveComment(CommentDto commentDto)
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
            commentDto.Id = comment.Id;
            return (true, commentDto);
        }
    }
}
