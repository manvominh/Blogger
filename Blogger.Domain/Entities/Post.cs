using Blogger.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Domain.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Title { get; set; }
		[Required]
		[StringLength(200)]
		public string Introduction { get; set; }
        public string BodyText { get; set; }
		public string ImageUrl { get; set; }
		public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int UserId { get; set; }
        public List<Comment> Comments { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
