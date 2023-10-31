using Blogger.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Domain.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
		[Required]
		[StringLength(1000)]
		public string CommentText { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
