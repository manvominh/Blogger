
using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class CommentDto : IMapFrom<Comment>
    {
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter Comment.")]
		[MaxLength(1000, ErrorMessage = "Title cannot be longer than 1000 characters.")]
		public string CommentText { get; set; }
    }
}
