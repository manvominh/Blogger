
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
        [Required]
        public string CommentText { get; set; }
        //public User User { get; set; }
        //public Post Post { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
